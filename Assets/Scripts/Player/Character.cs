using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string CharacterName;
    public string Name_Kor;
    public int MaxHp;


    public int cost;
    public int previewNum;
    public bool isOwn;

    public string[] ability;

    public float GoldPercent;
    public float healPercent;
    public float scorePercent;
    public float decreaseHpPercent;
    public float treasureZoneDecreaseSpeed;
    public float decreaseCrashDamage;

}
