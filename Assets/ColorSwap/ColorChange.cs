using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(SpriteRenderer))]
public class ColorChange : MonoBehaviour
{
    private Renderer theRenderer;
    private SpriteRenderer spriteRenderer;
    private bool[] changed = {
        false, false, false, false, false
    };

    private Color[] lastColorFrom = {
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
    };
    public Color[] colorFrom = {
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
    };
    public Color[] colorTo = {
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
    };

    MaterialPropertyBlock mpb;

    static readonly int ColorTo1Prop = Shader.PropertyToID("_ColorTo1");
    static readonly int ColorFrom1Prop = Shader.PropertyToID("_ColorFrom1");
    static readonly int ColorTo2Prop = Shader.PropertyToID("_ColorTo2");
    static readonly int ColorFrom2Prop = Shader.PropertyToID("_ColorFrom2");
    static readonly int ColorTo3Prop = Shader.PropertyToID("_ColorTo3");
    static readonly int ColorFrom3Prop = Shader.PropertyToID("_ColorFrom3");
    static readonly int ColorTo4Prop = Shader.PropertyToID("_ColorTo4");
    static readonly int ColorFrom4Prop = Shader.PropertyToID("_ColorFrom4");
    static readonly int ColorTo5Prop = Shader.PropertyToID("_ColorTo5");
    static readonly int ColorFrom5Prop = Shader.PropertyToID("_ColorFrom5");

    MaterialPropertyBlock Mpb
    {
        get
        {
            if (mpb == null)
            {
                mpb = new MaterialPropertyBlock();
            }
            return mpb;
        }
    }

    public void Reset()
    {

        for (int i = 0; i < 5; i++)
        {
            colorTo[i] = colorFrom[i] = Color.white;
            //changed[i] = false;
        }
    }

    private void GetReference()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        if (theRenderer == null)
            theRenderer = GetComponent<Renderer>();
    }

    public void Awake()
    {
        GetReference();

        ApplyColor();
        /*Texture2D colourPalette = new Texture2D(256, 10, TextureFormat.ARGB32, false);

        for (int x = 0; x < 256; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                colourPalette.SetPixel(x, y, colourArray[x]);
            }
        }
        colourPalette.filterMode = FilterMode.Point;
        colourPalette.wrapMode = TextureWrapMode.Clamp;
        colourPalette.Apply();
        GetComponent<Renderer>().material.SetTexture("_ColorRamp", colourPalette);*/

        //Material material = GetComponent<Renderer>().sharedMaterial;


        /*mpb.SetColor(ColorTo1Prop, colorTo[0]);
        GetComponent<Renderer>().SetPropertyBlock(mpb);*/
    }


    /*void ApplyColor()
    {
        Shader shader = Shader.Find("Shader Graphs/ColorSwap");
        Material mat = new Material(shader)
        {
            hideFlags = HideFlags.HideAndDontSave
        };
        print(mat.name);
        for (int i = 0; i < 5; i++)
        {
            mat.SetColor("ColorFrom" + (i + 1).ToString(), colorTo[i]);
            mat.SetColor("ColorTo" + (i + 1).ToString(), colorFrom[i]);
        }

        GetComponent<Renderer>().material = mat;
    }*/

#if UNITY_EDITOR
    void CheckChange()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            return;
        }
        for (int i = 0; i < 5; i++)
        {
            //if (lastColorFrom[i] != colorFrom[i])
            if (!changed[i])
            {
                changed[i] = true;
                //lastColorFrom[i] = colorFrom[i];
                colorTo[i] = colorFrom[i];
            }
        }

    }
#endif

    void ApplyColor()
    {
        //Shader shader = Shader.Find("ColorSwap/ColorSwap");
        Shader shader = Shader.Find("ColorChange/ColorChange");
        Material mat = new Material(shader)
        {
            hideFlags = HideFlags.HideAndDontSave
        };
        theRenderer.material = mat;

        //Mpb.Clear();
        Mpb.SetTexture("_MainTex", spriteRenderer.sprite.texture);
        if (colorFrom[0] != colorTo[0])
        {
            Mpb.SetColor(ColorFrom1Prop, colorFrom[0]);
            Mpb.SetColor(ColorTo1Prop, colorTo[0]);
        }
        if (colorFrom[1] != colorTo[1])
        {
            Mpb.SetColor(ColorFrom2Prop, colorFrom[1]);
            Mpb.SetColor(ColorTo2Prop, colorTo[1]);
        }
        if (colorFrom[2] != colorTo[2])
        {
            Mpb.SetColor(ColorFrom3Prop, colorFrom[2]);
            Mpb.SetColor(ColorTo3Prop, colorTo[2]);
        }
        if (colorFrom[3] != colorTo[3])
        {
            Mpb.SetColor(ColorFrom4Prop, colorFrom[3]);
            Mpb.SetColor(ColorTo4Prop, colorTo[3]);
        }
        if (colorFrom[4] != colorTo[4])
        {
            Mpb.SetColor(ColorFrom5Prop, colorFrom[4]);
            Mpb.SetColor(ColorTo5Prop, colorTo[4]);
        }


        theRenderer.SetPropertyBlock(Mpb);
    }
    void OnValidate()
    {
        GetReference();
#if UNITY_EDITOR
        //CheckChange();
#endif
        ApplyColor();
    }
}
