using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameManager gm = FindObjectOfType<GameManager>();
                if (gm != null)
                    instance = gm;
            }
            return instance;
        }
    }
    [Header("Game State")]
    public bool[] characterIsOwn;
    public string currentCharacterName;
    public float decreaseHpPerSecondValue;
    public float healValue;
    public int highScore;
    public int score;
    public int getMoney;
    public int ownMoney;
    public int noHpItemCount;
    public float crashValue;

    [Header("Character Ability")]
    public float MaxHp;
    public float currentHp;
    public float GoldPercent;
    public float healPercent;
    public float scorePercent;
    public float decreaseHpPercent;
    public float treasureZoneDecreaseSpeed;
    public float decreaseCrashDamage;


    [Header("Bool Value")]
    public bool isHitCheckBtn = false;
    public bool isPassBox = false;
    public bool isHit = false;
    public bool isHpDecreaseStop = false;

    public bool isHardRound = false;
    public bool isTreasureBoxZone = false;
    public bool isTreasureLimitLine = false;
    public bool isTreasureCamRotZone = false;
    public bool isCrash = false;

    public bool isGameStart = false;
    public bool isGameOver = false;

    [Header("Scene")]
    public int TITLE = 0;
    public int SHOP = 1;
    public int GAME = 2;


    [Header("Json")]
    public JsonSave json;

  
    private void Start()
    {
        if (instance != null)
        {
            if (instance != this)
                Destroy(this.gameObject);

        }

        DontDestroyOnLoad(this.gameObject);
       json.LoadData();
    }

    public void ResetValue()
    {
        score = 0;

        getMoney = 0;
        isHitCheckBtn = false;
        isPassBox = false;
        isHit = false;
        isHpDecreaseStop = false;
        isHardRound = false;

        isTreasureBoxZone = false;
        isTreasureLimitLine = false;
        isTreasureCamRotZone = false;

        isGameStart = false;
        isGameOver = false;
        isCrash = false;
    }

}
