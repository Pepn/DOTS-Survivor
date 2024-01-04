using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ToonShaderStyleSetter : MonoBehaviour
{
    [Header("Shader Properties")]
    public Color color = Color.white;
    public Texture mainTexture;

    [Range(0.0f, 0.5f)]
    public float toonRampSmoothness = 0.5f;

    public Color toonRampTint = Color.white;

    [Range(0.0f, 1.0f)]
    public float toonRampOffset = 0.1f;

    [Range(0.0f, 10.0f)]
    public float rimPower = 1.0f;

    [Range(0.0f, 5.0f)]
    public float brightnessRim = 0.5f;

    [Required] public Material monsterMaterial;
    [Required] public Material playerMaterial;
    [Required] public Material planeMaterial;
    [Required] public Material xpMaterial;
    [Required] public Material playerBulletMaterial;

    private void Awake()
    {
        // Set shader variables
        SetShaderVariables(monsterMaterial);
        SetShaderVariables(playerMaterial);
        SetShaderVariables(planeMaterial);
    }

    [Button]
    private void SetAllMaterialsRandom()
    {
        SetRandomValues(monsterMaterial);
        SetRandomValues(playerMaterial);
        SetRandomValues(planeMaterial);
        SetRandomValues(xpMaterial);
        SetRandomValues(playerBulletMaterial);
    }

    [Button]
    public void SetRandomValuesMonster()
    {
        SetRandomValues(monsterMaterial);
    }

    private void SetRandomValues(Material mat)
    {
        toonRampSmoothness = Random.Range(0.0f, 0.5f);
        toonRampOffset = Random.Range(0.0f, 1.0f);
        rimPower = Random.Range(0.0f, 10.0f);
        brightnessRim = Random.Range(0.0f, 5.0f);

        // Set random color values
        color = new Color(Random.value, Random.value, Random.value);
        toonRampTint = new Color(Random.value, Random.value, Random.value);

        // Call the function to set shader variables
        SetShaderVariables(mat);
    }

    private void SetShaderVariables(Material material)
    {
        //using default shader
        if (material.shader.name.Contains("Universal"))
        {
            Debug.Log(material.shader.name);
            material.SetColor("_BaseColor", color);
            return;
        }

        material.SetColor("_Color", color);
        //material.SetTexture("_MainTex", mainTexture);
        material.SetFloat("_ToonRampSmoothness", toonRampSmoothness);
        material.SetColor("_ToonRampTint", toonRampTint);
        material.SetFloat("_ToonRampOffset", toonRampOffset);
        material.SetFloat("_RimPower", rimPower);
        material.SetFloat("_BrightnessRim", brightnessRim);
    }
}
