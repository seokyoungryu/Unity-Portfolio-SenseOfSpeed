using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItemCreate : MonoBehaviour
{
    public GameObject hpItem;
    public GameObject ItemPos_Parent;
    public HpItemInfo[] infos;

    public int count = 0;

    private  void OnEnable()
    {
        if (count >= 1)
        {
            if(GameManager.Instance.isHardRound == false)
                ItemCreate();
            else if(GameManager.Instance.isHardRound)
            {
                int rand = Random.Range(1, 5);
                if (rand != 0)
                    HardActiveItem(rand);
            }
        }

        count += 1;
    }

    public void ItemCreate()
    {
        infos = ItemPos_Parent.GetComponentsInChildren<HpItemInfo>();
        

        if (GameManager.Instance.noHpItemCount >= 1)
        {
            ActiveItem();
        }
        else
        {
            int rand = Random.Range(0, 10);

            if (rand > 5) //积己
            {
                ActiveItem();
            }
            else
            {
                GameManager.Instance.noHpItemCount += 1;
            }
        }
    }

    public void ActiveItem()
    {
        int rand = Random.Range(0,infos.Length);
        infos[rand].hpItem_go.SetActive(true);
        infos[rand].isActive = true;
        GameManager.Instance.noHpItemCount = 0;

    }


    //吝汗 厘家 力芭 
    public void HardActiveItem(int count)
    {
        infos = ItemPos_Parent.GetComponentsInChildren<HpItemInfo>();

        List<int> randomNumber = new List<int>();

        for (int a = 0; a < infos.Length; a++)
        {
            randomNumber.Add(a);
        }

        int[] rands = new int[count];

        for (int i = 0; i < count; i++)
        {
            int temp = Random.Range(0, randomNumber.Count);
            rands[i] = randomNumber[temp];
            infos[rands[i]].hpItem_go.SetActive(true);
            infos[rands[i]].isActive = true;

            randomNumber.RemoveAt(temp);
        }

      
       
    }

}
