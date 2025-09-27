using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItemInfo : MonoBehaviour
{
   
    private float healValue;

    public GameObject hpItem_go;

    public bool isActive = false;


    private void Start()
    {
        healValue = GameManager.Instance.healValue * ( 1 + GameManager.Instance.healPercent);
    }
    void Update()
    {
        //transform.eulerAngles += hpRot_value * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(isActive)
            {
                GameManager.Instance.currentHp += healValue;
                if (GameManager.Instance.currentHp >= GameManager.Instance.MaxHp)
                    GameManager.Instance.currentHp = GameManager.Instance.MaxHp;

                SoundManager.Instance.PlayEffectSound("HPItem_Eat");
                isActive = false;
                hpItem_go.SetActive(false);
            }
           
        }
    }

    private void OnDisable()
    {
        isActive = false;
        hpItem_go.SetActive(false);
    }
}
