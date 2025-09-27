using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MoneyUI : MonoBehaviour
{
    public TMP_Text money_Text;

    public int tempStartMoney;
    public int getMoney;


    public void Update()
    {
        money_Text.text = tempStartMoney.ToString();

        if (Input.GetKeyDown(KeyCode.C))
            GetMoney(500);

    }

    public void RandomGetMoney(int min, int max)
    {
        int randomNumber = Random.Range(min, max);
        int applyGoldPercent = (int)(randomNumber * (1 + GameManager.Instance.GoldPercent));
        GetMoney(applyGoldPercent);

    }

    public void GetMoney(int money)
    {
        getMoney = tempStartMoney + money;

        StartCoroutine(CountMoney_Co());

    }


    IEnumerator CountMoney_Co()
    {
        GameManager.Instance.getMoney = getMoney;
        SoundManager.Instance.PlayLoopSound("Coin_Counting");
        while (tempStartMoney != getMoney && tempStartMoney < getMoney)
        {
            tempStartMoney += 1;
            money_Text.text = tempStartMoney.ToString();
            yield return null;

        }
        Debug.Log("¸ØÃã!");

        SoundManager.Instance.StopLoopSound("Coin_Counting");
    }


}
