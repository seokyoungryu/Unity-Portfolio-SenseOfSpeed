using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    public Slider hp_slider;



    private void Update()
    {
        if(GameManager.Instance.isGameStart && !GameManager.Instance.isGameOver)
        {
            if (!GameManager.Instance.isHpDecreaseStop)
            {
                GameManager.Instance.currentHp 
                    -= (GameManager.Instance.decreaseHpPerSecondValue * (1 - GameManager.Instance.decreaseHpPercent)) * Time.deltaTime;

            }
            hp_slider.value = GameManager.Instance.currentHp / GameManager.Instance.MaxHp;
            if (GameManager.Instance.currentHp <= 0)
                PlayerMoveController.Instance.Die("treasureZone");
        }
       
    }
}
