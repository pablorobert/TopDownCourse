using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipePanel : MonoBehaviour
{
    public List<RecipeUI> recipeButtons = new List<RecipeUI>();
    public List<Recipe> recipes = new List<Recipe>();

    public List<Item> recipeItems = new List<Item>();

    private void Start()
    {
        foreach (RecipeUI obj in recipeButtons)
        {
            obj.Reset();
        }
    }

    public void BuildRecipe()
    {
        recipeItems.Clear();
        for (int i = 0; i < recipeButtons.Count; i++)
        {
            recipeItems.Add(recipeButtons[i].item);
        }
        Cook();
    }

    private void Cook()
    {
        bool cooked = false;
        Item outcome = null;
        foreach (Recipe recipe in recipes)
        {
            //Debug.Log("Checando");
            if (recipe.Check(recipeItems))
            {
                cooked = true;
                outcome = recipe.outcome;
                //Debug.Log("You got " + recipe.outcome.itemName);
            }
        }
        if (cooked)
        {
            Debug.Log("You got " + outcome.itemName);
        }
        else
        {
            Debug.Log("Recipe not recognized");
        }
    }
}