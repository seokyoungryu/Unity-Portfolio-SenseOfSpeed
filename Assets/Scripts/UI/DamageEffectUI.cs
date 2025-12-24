using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffectUI : MonoBehaviour
{
    public Image[] damageEffect_Imgs;

    Color colorOrigin;

    private void Start()
    {
        colorOrigin = damageEffect_Imgs[0].color;
    }
    public void DamageUI()
    {
        StopAllCoroutines();
        ResetValue();
        StartCoroutine(DamageEffectFadeOut_Co());

    }
  
    private IEnumerator DamageEffectFadeOut_Co()
    {
        for (int i = 0; i < damageEffect_Imgs.Length; i++)
        {
            damageEffect_Imgs[i].gameObject.SetActive(true);
        }

        Color[] imgColors = new Color[damageEffect_Imgs.Length];
      
        for (int i = 0; i < imgColors.Length; i++)
        {
            imgColors[i] = damageEffect_Imgs[i].color;
        }

        for (int i = 0; i < 20; i++)
        {
            imgColors[0].a -= 0.05f;
            imgColors[1].a -= 0.05f;
            imgColors[2].a -= 0.05f;
            imgColors[3].a -= 0.05f;

            damageEffect_Imgs[0].color = imgColors[0];
            damageEffect_Imgs[1].color = imgColors[1];
            damageEffect_Imgs[2].color = imgColors[2];
            damageEffect_Imgs[3].color = imgColors[3];

            yield return new WaitForSeconds(0.1f);
        }
    }


    private void ResetValue()
    {
        for (int i = 0; i < damageEffect_Imgs.Length; i++)
        {
            damageEffect_Imgs[i].color = colorOrigin;
            damageEffect_Imgs[i].gameObject.SetActive(false);
        }
    }
}
