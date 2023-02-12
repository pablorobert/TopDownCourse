using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using PR.Editor;
using Object = UnityEngine.Object;

namespace PR
{
	/// <summary>
	/// Conditionally Show/Hide field in inspector, based on some other field or property value
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field)]
	public class HideIfAttribute : PropertyAttribute
	{
		public bool IsSet => Data != null && Data.IsSet;
		public readonly ConditionalData Data;

		/// <param name="fieldToCheck">String name of field to check value</param>
		/// <param name="inverse">Inverse check result</param>
		/// <param name="compareValues">On which values field will be shown in inspector</param>
		public HideIfAttribute(string fieldToCheck, bool inverse = false, params object[] compareValues)
			=> Data = new ConditionalData(fieldToCheck, inverse, compareValues);


		public HideIfAttribute(string[] fieldToCheck, bool[] inverse = null, params object[] compare)
			=> Data = new ConditionalData(fieldToCheck, inverse, compare);

		public HideIfAttribute(params string[] fieldToCheck) => Data = new ConditionalData(fieldToCheck);
		public HideIfAttribute(bool useMethod, string method, bool inverse = false)
			=> Data = new ConditionalData(useMethod, method, inverse);
	}

	/// <summary>
	/// This pool is used to prevent warning message spamming.
	/// If something was logged once it wont be logged again
	/// </summary>
	public static class WarningsPool
	{
		public static bool Log(string message, Object target = null)
		{
			if (Pool.Contains(message)) return false;

			if (target != null) Debug.Log(message, target);
			else Debug.Log(message);

			Pool.Add(message);
			return true;
		}

		public static bool LogWarning(string message, Object target = null)
		{
			if (Pool.Contains(message)) return false;

			if (target != null) Debug.LogWarning(message, target);
			else Debug.LogWarning(message);

			Pool.Add(message);
			return true;
		}

#if UNITY_EDITOR
		public static bool LogWarning(Object owner, string message, Object target = null)
			=> LogWarning($"<color=brown>{owner.name}</color> caused: " + message, target);

		public static bool LogWarning(UnityEditor.SerializedProperty property, string message, Object target = null)
			=> LogWarning($"Property <color=brown>{property.name}</color> " +
						  $"in Object <color=brown>{property.serializedObject.targetObject.name}</color> caused: " + message, target);

		public static bool LogCollectionsNotSupportedWarning(UnityEditor.SerializedProperty property, string nameOfType)
			=> LogWarning(property, $"Array fields are not supported by <color=brown>[{nameOfType}]</color>. " +
									"Consider to use <color=blue>CollectionWrapper</color>", property.serializedObject.targetObject);
#endif


		public static bool LogError(string message, Object target = null)
		{
			if (Pool.Contains(message)) return false;

			if (target != null) Debug.LogError(message, target);
			else Debug.LogError(message);

			Pool.Add(message);
			return true;
		}

		private static readonly System.Collections.Generic.HashSet<string> Pool = new HashSet<string>();
	}

	public class MyCommonConstants
	{
		public static readonly System.Random SystemRandom = new System.Random();
	}
}

#if UNITY_EDITOR
namespace PR.Editor
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;

	[CustomPropertyDrawer(typeof(HideIfAttribute))]
	public class HideIfAttributeDrawer : PropertyDrawer
	{
		private bool _toShow = true;
		private bool _initialized;
		private PropertyDrawer _customPropertyDrawer;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (!(attribute is HideIfAttribute conditional)) return EditorGUI.GetPropertyHeight(property);

			CachePropertyDrawer(property);
			_toShow = ConditionalUtility.IsPropertyConditionMatch(property, conditional.Data);
			if (!_toShow) return -2;

			if (_customPropertyDrawer != null) return _customPropertyDrawer.GetPropertyHeight(property, label);
			return EditorGUI.GetPropertyHeight(property);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (!_toShow) return;

			if (!CustomDrawerUsed()) EditorGUI.PropertyField(position, property, label, true);


			bool CustomDrawerUsed()
			{
				if (_customPropertyDrawer == null) return false;

				try
				{
					_customPropertyDrawer.OnGUI(position, property, label);
					return true;
				}
				catch (System.Exception e)
				{
					WarningsPool.LogWarning(property,
						"Unable to use CustomDrawer of type " + _customPropertyDrawer.GetType() + ": " + e,
						property.serializedObject.targetObject);

					return false;
				}
			}
		}

		/// <summary>
		/// Try to find and cache any PropertyDrawer or PropertyAttribute on the field
		/// </summary>
		private void CachePropertyDrawer(SerializedProperty property)
		{
			if (_initialized) return;
			_initialized = true;
			if (fieldInfo == null) return;

			var customDrawer = CustomDrawerUtility.GetPropertyDrawerForProperty(property, fieldInfo, attribute);
			if (customDrawer == null) customDrawer = TryCreateAttributeDrawer();

			_customPropertyDrawer = customDrawer;


			// Try to get drawer for any other Attribute on the field
			PropertyDrawer TryCreateAttributeDrawer()
			{
				var secondAttribute = TryGetSecondAttribute();
				if (secondAttribute == null) return null;

				var attributeType = secondAttribute.GetType();
				var customDrawerType = CustomDrawerUtility.GetPropertyDrawerTypeForFieldType(attributeType);
				if (customDrawerType == null) return null;

				return CustomDrawerUtility.InstantiatePropertyDrawer(customDrawerType, fieldInfo, secondAttribute);


				//Get second attribute if any
				System.Attribute TryGetSecondAttribute()
				{
					return (PropertyAttribute)fieldInfo.GetCustomAttributes(typeof(PropertyAttribute), false)
						.FirstOrDefault(a => !(a is HideIfAttribute));
				}
			}
		}
	}

	public class ConditionalData
	{
		public bool IsSet => _fieldToCheck.NotNullOrEmpty() || _fieldsToCheckMultiple.NotNullOrEmpty() || _predicateMethod.NotNullOrEmpty();

		private readonly string _fieldToCheck;
		private readonly bool _inverse;
		private readonly string[] _compareValues;

		private readonly string[] _fieldsToCheckMultiple;
		private readonly bool[] _inverseMultiple;
		private readonly string[] _compareValuesMultiple;

		private readonly string _predicateMethod;

		public ConditionalData(string fieldToCheck, bool inverse = false, params object[] compareValues)
			=> (_fieldToCheck, _inverse, _compareValues) =
				(fieldToCheck, inverse, compareValues.Select(c => c.ToString().ToUpper()).ToArray());

		public ConditionalData(string[] fieldToCheck, bool[] inverse = null, params object[] compare) =>
			(_fieldsToCheckMultiple, _inverseMultiple, _compareValuesMultiple) =
			(fieldToCheck, inverse, compare.Select(c => c.ToString().ToUpper()).ToArray());

		public ConditionalData(params string[] fieldToCheck) => _fieldsToCheckMultiple = fieldToCheck;

		// ReSharper disable once UnusedParameter.Local
		public ConditionalData(bool useMethod, string methodName, bool inverse = false)
			=> (_predicateMethod, _inverse) = (methodName, inverse);


#if UNITY_EDITOR
		/// <summary>
		/// Iterate over Field Conditions
		/// </summary>
		public IEnumerator<(string Field, bool Inverse, string[] CompareAgainst)> GetEnumerator()
		{
			if (_fieldToCheck.NotNullOrEmpty()) yield return (_fieldToCheck, _inverse, _compareValues);
			if (_fieldsToCheckMultiple.NotNullOrEmpty())
			{
				for (var i = 0; i < _fieldsToCheckMultiple.Length; i++)
				{
					var field = _fieldsToCheckMultiple[i];
					bool withInverseValue = _inverseMultiple != null && _inverseMultiple.Length - 1 >= i;
					bool withCompareValue = _compareValuesMultiple != null && _compareValuesMultiple.Length - 1 >= i;
					var inverse = withInverseValue && _inverseMultiple[i];
					var compare = withCompareValue ? new[] { _compareValuesMultiple[i] } : null;

					yield return (field, inverse, compare);
				}
			}
		}

		/// <summary>
		/// Call and check Method Condition, if any
		/// </summary>
		public bool IsMethodConditionMatch(object owner)
		{
			if (_predicateMethod.IsNullOrEmpty()) return true;

			var predicateMethod = GetMethodCondition(owner);
			if (predicateMethod == null) return true;

			bool match = (bool)predicateMethod.Invoke(owner, null);
			if (_inverse) match = !match;
			return match;
		}


		private MethodInfo GetMethodCondition(object owner)
		{
			if (_predicateMethod.IsNullOrEmpty()) return null;
			if (_initializedMethodInfo) return _cachedMethodInfo;
			_initializedMethodInfo = true;

			var ownerType = owner.GetType();
			var bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			var method = ownerType.GetMethods(bindings).SingleOrDefault(m => m.Name == _predicateMethod);

			if (method == null || method.ReturnType != typeof(bool))
			{
				ConditionalUtility.LogMethodNotFound((UnityEngine.Object)owner, _predicateMethod);
				_cachedMethodInfo = null;
			}
			else _cachedMethodInfo = method;

			return _cachedMethodInfo;
		}

		private MethodInfo _cachedMethodInfo;
		private bool _initializedMethodInfo;
#endif
	}

	public static class CustomDrawerUtility
	{
		/// <summary>
		/// Key is Associated with drawer type (the T in [CustomPropertyDrawer(typeof(T))])
		/// Value is PropertyDrawer Type
		/// </summary>
		private static readonly Dictionary<Type, Type> PropertyDrawersInAssembly = new Dictionary<Type, Type>();
		private static readonly Dictionary<int, PropertyDrawer> PropertyDrawersCache = new Dictionary<int, PropertyDrawer>();
		private static readonly string IgnoreScope = typeof(int).Module.ScopeName;

		/// <summary>
		/// Create PropertyDrawer for specified property if any PropertyDrawerType for such property is found.
		/// FieldInfo and Attribute will be inserted in created drawer.
		/// </summary>
		public static PropertyDrawer GetPropertyDrawerForProperty(SerializedProperty property, FieldInfo fieldInfo, System.Attribute attribute)
		{
			var propertyId = property.GetUniquePropertyId();
			if (PropertyDrawersCache.TryGetValue(propertyId, out var drawer)) return drawer;

			var targetType = fieldInfo.FieldType;
			var drawerType = GetPropertyDrawerTypeForFieldType(targetType);
			if (drawerType != null)
			{
				drawer = InstantiatePropertyDrawer(drawerType, fieldInfo, attribute);

				if (drawer == null)
					WarningsPool.LogWarning(property,
						$"Unable to instantiate CustomDrawer of type {drawerType} for {fieldInfo.FieldType}",
						property.serializedObject.targetObject);
			}

			PropertyDrawersCache[propertyId] = drawer;
			return drawer;
		}

		public static PropertyDrawer InstantiatePropertyDrawer(Type drawerType, FieldInfo fieldInfo, Attribute insertAttribute)
		{
			try
			{
				var drawerInstance = (PropertyDrawer)Activator.CreateInstance(drawerType);

				// Reassign the attribute and fieldInfo fields in the drawer so it can access the argument values
				var fieldInfoField = drawerType.GetField("m_FieldInfo", BindingFlags.Instance | BindingFlags.NonPublic);
				if (fieldInfoField != null) fieldInfoField.SetValue(drawerInstance, fieldInfo);
				var attributeField = drawerType.GetField("m_Attribute", BindingFlags.Instance | BindingFlags.NonPublic);
				if (attributeField != null) attributeField.SetValue(drawerInstance, insertAttribute);

				return drawerInstance;
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// Try to get PropertyDrawer for a target Type, or any Base Type for a target Type
		/// </summary>
		public static Type GetPropertyDrawerTypeForFieldType(Type drawerTarget)
		{
			// Ignore .net types from mscorlib.dll
			if (drawerTarget.Module.ScopeName.Equals(IgnoreScope)) return null;
			CacheDrawersInAssembly();

			// Of all property drawers in the assembly we need to find one that affects target type
			// or one of the base types of target type
			var checkType = drawerTarget;
			while (checkType != null)
			{
				if (PropertyDrawersInAssembly.TryGetValue(drawerTarget, out var drawer)) return drawer;
				checkType = checkType.BaseType;
			}

			return null;
		}

		private static Type[] GetTypesSafe(Assembly assembly)
		{
			try
			{
				return assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				return e.Types;
			}
		}

		private static void CacheDrawersInAssembly()
		{
			if (PropertyDrawersInAssembly.NotNullOrEmpty()) return;

			var propertyDrawerType = typeof(PropertyDrawer);
			var allDrawerTypesInDomain = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(GetTypesSafe)
				.Where(t => t != null && propertyDrawerType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

			foreach (var drawerType in allDrawerTypesInDomain)
			{
				var propertyDrawerAttribute = CustomAttributeData.GetCustomAttributes(drawerType).FirstOrDefault();
				if (propertyDrawerAttribute == null) continue;
				var drawerTargetType = propertyDrawerAttribute.ConstructorArguments.FirstOrDefault().Value as Type;
				if (drawerTargetType == null) continue;

				if (PropertyDrawersInAssembly.ContainsKey(drawerTargetType)) continue;
				PropertyDrawersInAssembly.Add(drawerTargetType, drawerType);
			}
		}
	}

	public static class ConditionalUtility
	{
		public static bool IsConditionMatch(UnityEngine.Object owner, ConditionalData condition)
		{
			if (!condition.IsSet) return true;

			var so = new SerializedObject(owner);
			foreach (var fieldCondition in condition)
			{
				if (fieldCondition.Field.IsNullOrEmpty()) continue;

				var property = so.FindProperty(fieldCondition.Field);
				if (property == null) LogFieldNotFound(so.targetObject, fieldCondition.Field);

				bool passed = IsConditionMatch(property, fieldCondition.Inverse, fieldCondition.CompareAgainst);
				if (!passed) return false;
			}

			return condition.IsMethodConditionMatch(owner);
		}

		public static bool IsPropertyConditionMatch(SerializedProperty property, ConditionalData condition)
		{
			if (!condition.IsSet) return true;

			foreach (var fieldCondition in condition)
			{
				var relativeProperty = FindRelativeProperty(property, fieldCondition.Field);
				if (relativeProperty == null) LogFieldNotFound(property, fieldCondition.Field);

				bool passed = IsConditionMatch(relativeProperty, fieldCondition.Inverse, fieldCondition.CompareAgainst);
				if (!passed) return false;
			}

			return condition.IsMethodConditionMatch(property.GetParent());
		}

		private static void LogFieldNotFound(SerializedProperty property, string field) => WarningsPool.LogWarning(property,
			$"Conditional Attribute is trying to check field {field.Colored(Colors.brown)} which is not present",
			property.serializedObject.targetObject);
		private static void LogFieldNotFound(UnityEngine.Object owner, string field) => WarningsPool.LogWarning(owner,
			$"Conditional Attribute is trying to check field {field.Colored(Colors.brown)} which is not present",
			owner);
		public static void LogMethodNotFound(UnityEngine.Object owner, string method) => WarningsPool.LogWarning(owner,
			$"Conditional Attribute is trying to invoke method {method.Colored(Colors.brown)} " +
			"which is missing or not with a bool return type",
			owner);

		private static bool IsConditionMatch(SerializedProperty property, bool inverse, string[] compareAgainst)
		{
			if (property == null) return true;

			string asString = property.AsStringValue().ToUpper();

			if (compareAgainst != null && compareAgainst.Length > 0)
			{
				var matchAny = CompareAgainstValues(asString, compareAgainst, IsFlagsEnum());
				if (inverse) matchAny = !matchAny;
				return matchAny;
			}

			bool someValueAssigned = asString != "FALSE" && asString != "0" && asString != "NULL";
			if (someValueAssigned) return !inverse;

			return inverse;


			bool IsFlagsEnum()
			{
				if (property.propertyType != SerializedPropertyType.Enum) return false;
				var value = property.GetValue();
				if (value == null) return false;
				return value.GetType().GetCustomAttribute<FlagsAttribute>() != null;
			}
		}


		/// <summary>
		/// True if the property value matches any of the values in '_compareValues'
		/// </summary>
		private static bool CompareAgainstValues(string propertyValueAsString, string[] compareAgainst, bool handleFlags)
		{
			if (!handleFlags) return ValueMatches(propertyValueAsString);

			if (propertyValueAsString == "-1") //Handle Everything
				return true;
			if (propertyValueAsString == "0") //Handle Nothing
				return false;

			var separateFlags = propertyValueAsString.Split(',');
			foreach (var flag in separateFlags)
			{
				if (ValueMatches(flag.Trim())) return true;
			}

			return false;


			bool ValueMatches(string value)
			{
				foreach (var compare in compareAgainst)
					if (value == compare)
						return true;
				return false;
			}
		}

		/// <summary>
		/// Get the other Property which is stored alongside with specified Property, by name
		/// </summary>
		private static SerializedProperty FindRelativeProperty(SerializedProperty property, string propertyName)
		{
			if (property.depth == 0) return property.serializedObject.FindProperty(propertyName);

			var path = property.propertyPath.Replace(".Array.data[", "[");
			var elements = path.Split('.');

			var nestedProperty = NestedPropertyOrigin(property, elements);

			// if nested property is null = we hit an array property
			if (nestedProperty == null)
			{
				var cleanPath = path.Substring(0, path.IndexOf('['));
				var arrayProp = property.serializedObject.FindProperty(cleanPath);
				WarningsPool.LogCollectionsNotSupportedWarning(arrayProp, nameof(HideIfAttribute));

				return null;
			}

			return nestedProperty.FindPropertyRelative(propertyName);
		}

		// For [Serialized] types with [Conditional] fields
		private static SerializedProperty NestedPropertyOrigin(SerializedProperty property, string[] elements)
		{
			SerializedProperty parent = null;

			for (int i = 0; i < elements.Length - 1; i++)
			{
				var element = elements[i];
				int index = -1;
				if (element.Contains("["))
				{
					index = Convert.ToInt32(element.Substring(element.IndexOf("[", StringComparison.Ordinal))
						.Replace("[", "").Replace("]", ""));
					element = element.Substring(0, element.IndexOf("[", StringComparison.Ordinal));
				}

				parent = i == 0
					? property.serializedObject.FindProperty(element)
					: parent != null
						? parent.FindPropertyRelative(element)
						: null;

				if (index >= 0 && parent != null) parent = parent.GetArrayElementAtIndex(index);
			}

			return parent;
		}
	}
}
#endif