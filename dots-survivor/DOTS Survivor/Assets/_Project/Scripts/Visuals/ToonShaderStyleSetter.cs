using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class StyleContainer
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
}

public class ToonShaderStyleSetter : MonoBehaviour
{
    [Required] public Material monsterMaterial;
    [Required] public Material playerMaterial;
    [Required] public Material planeMaterial;
    [Required] public Material xpMaterial;
    [Required] public Material playerBulletMaterial;

    public StyleContainer monsterStyle, playerStyle, planeStyle, xpStyle, playerBulletStyle;
    private void Awake()
    {
        // Set shader variables
        //SetShaderVariables(monsterMaterial);
        //SetShaderVariables(playerMaterial);
        //SetShaderVariables(planeMaterial);
    }

    [Button]
    private void SetAllMaterialsRandom()
    {
        SetRandomValues(monsterMaterial, ref monsterStyle);
        SetRandomValues(playerMaterial, ref playerStyle);
        SetRandomValues(planeMaterial, ref planeStyle);
        SetRandomValues(xpMaterial, ref xpStyle);
        SetRandomValues(playerBulletMaterial, ref playerBulletStyle);
    }

    [Button]
    public void SetRandomValuesMonster()
    {
        SetRandomValues(monsterMaterial, ref monsterStyle);
    }

    [Button]
    public void FlowToNextRandomColor()
    {
        StartCoroutine(FlowToNextColorGeneration(monsterMaterial, monsterStyle, 2.0f));
        StartCoroutine(FlowToNextColorGeneration(playerMaterial, playerStyle, 2.0f));
        StartCoroutine(FlowToNextColorGeneration(planeMaterial, planeStyle, 2.0f));
        StartCoroutine(FlowToNextColorGeneration(xpMaterial, xpStyle, 2.0f));
        StartCoroutine(FlowToNextColorGeneration(playerBulletMaterial, playerBulletStyle, 2.0f));
    }

    private IEnumerator FlowToNextColorGeneration(Material mat, StyleContainer currentStyle, float duration)
    {
        StyleContainer targetStyle = new StyleContainer();
        SetRandomValues(null, ref targetStyle);

        StyleContainer lerpingStyle = currentStyle;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float percentageComplete = elapsedTime / duration;

            lerpingStyle.color = Color.Lerp(currentStyle.color, targetStyle.color, percentageComplete);
            lerpingStyle.toonRampSmoothness = Mathf.Lerp(currentStyle.toonRampSmoothness, targetStyle.toonRampSmoothness, percentageComplete);
            lerpingStyle.toonRampOffset = Mathf.Lerp(currentStyle.toonRampOffset, targetStyle.toonRampOffset, percentageComplete);
            lerpingStyle.rimPower = Mathf.Lerp(currentStyle.rimPower, targetStyle.rimPower, percentageComplete);
            lerpingStyle.brightnessRim = Mathf.Lerp(currentStyle.brightnessRim, targetStyle.brightnessRim, percentageComplete);
            lerpingStyle.toonRampTint = Color.Lerp(currentStyle.color, targetStyle.color, percentageComplete);

            SetShaderVariables(mat, lerpingStyle);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void SetRandomValues(Material mat, ref StyleContainer style)
    {
        style.toonRampSmoothness = Random.Range(0.0f, 0.5f);
        style.toonRampOffset = Random.Range(0.0f, 1.0f);
        style.rimPower = Random.Range(0.0f, 10.0f);
        style.brightnessRim = Random.Range(0.0f, 5.0f);

        // Set random color values
        style.color = new Color(Random.value, Random.value, Random.value);
        style.toonRampTint = new Color(Random.value, Random.value, Random.value);

        // Call the function to set shader variables
        if(mat != null)
        {
            SetShaderVariables(mat, style);
        }
    }

    private void SetShaderVariables(Material material, StyleContainer style)
    {
        //using default shader
        if (material.shader.name.Contains("Universal"))
        {
            //Debug.Log(material.shader.name);
            material.SetColor("_BaseColor", style.color);
            return;
        }

        material.SetColor("_Color", style.color);
        //material.SetTexture("_MainTex", mainTexture);
        material.SetFloat("_ToonRampSmoothness", style.toonRampSmoothness);
        material.SetColor("_ToonRampTint", style.toonRampTint);
        material.SetFloat("_ToonRampOffset", style.toonRampOffset);
        material.SetFloat("_RimPower", style.rimPower);
        material.SetFloat("_BrightnessRim", style.brightnessRim);
    }
}
