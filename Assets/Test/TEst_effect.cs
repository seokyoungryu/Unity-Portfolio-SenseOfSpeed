using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEst_effect : MonoBehaviour
{
    public float dureation = 0f;
    public float blinkIntensit = 0f;
    public float blinkTimer = 0f;
    public SkinnedMeshRenderer[] renderers;
    public Color color;

    private void Start()
    {
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        blinkTimer = dureation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
            blinkTimer = dureation;


        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / dureation);
        float inten = lerp * blinkIntensit;
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sharedMaterials[i].color = color * inten;
        }


    }
}
