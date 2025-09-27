using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("Score UI")]
    public Text highScore_text;
    public Text currentScore_text;
    private float tempCountScore = 0;

    [Header("Money UI")]
    public Text getMoney_text;
    public Text OwnMoney_text;
    public int tempGetMoney;
    public int tempOwnMoney;

    [Header("Butten")]
    public Button reStart_Btn;
    public Button GoMain_Btn;
    public GameObject move_Panel;

    [Header("Fade In")]
    public Image fadeImg;
    public int fadeCount;
    public float fadevalue;

    private Animator gameOverAnim;

    private bool isGameOverCheck = false;

    private void OnEnable()
    {
        tempCountScore = 0;
        tempGetMoney = 0;
        tempOwnMoney = 0;
        isGameOverCheck = false;
        GameManager.Instance.ResetValue();
    }

    private void Start()
    {
        gameOverAnim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (GameManager.Instance.isGameOver && !isGameOverCheck)
        {
            StartCoroutine(GameOverPanel_Co());
            isGameOverCheck = true;
        }
    }



    public IEnumerator GameOverPanel_Co()
    {
        InitValue();
        StartCoroutine(fadeIn_Co());

        yield return new WaitForSeconds(1f);

        gameOverAnim.SetTrigger("GameOver");

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ResultScoreIncrease_Co());

        yield return new WaitForSeconds(1f);
        
        StartCoroutine(ResultMoneyIncrease_Co());

        yield return new WaitForSeconds(1f);

        reStart_Btn.gameObject.SetActive(true);
        GoMain_Btn.gameObject.SetActive(true);

        GameManager.Instance.json.SaveData();

    }

    IEnumerator fadeIn_Co()
    {
        Color tempColor = fadeImg.color;
        for (int i = 0; i < fadeCount; i++)
        {
            tempColor.a += fadevalue;
            fadeImg.color = tempColor;
            yield return new WaitForSeconds(0.1f);

        }

    }

    private void InitValue()
    {
        highScore_text.text = GameManager.Instance.highScore.ToString();
        getMoney_text.text = GameManager.Instance.getMoney.ToString();
        OwnMoney_text.text = GameManager.Instance.ownMoney.ToString();
        
        tempGetMoney = GameManager.Instance.getMoney;
        tempOwnMoney = GameManager.Instance.ownMoney;
        
        reStart_Btn.gameObject.SetActive(false);
        GoMain_Btn.gameObject.SetActive(false);
        move_Panel.SetActive(false);
        SoundManager.Instance.StopBGMSound();
        SoundManager.Instance.AllStopEffectSound();
        SoundManager.Instance.PlayEffectSound("GameOver");


    }
    IEnumerator ResultScoreIncrease_Co()
    {
        bool isScoreIncrease = true;
        int increseValue = CheckIncreaseValue(GameManager.Instance.score);
        SoundManager.Instance.PlayLoopSound("GameOver_Counting");

        while (isScoreIncrease)
        {
            if (GameManager.Instance.score > tempCountScore)
            {
                tempCountScore += increseValue;
                currentScore_text.text = tempCountScore.ToString("0");
                if (GameManager.Instance.score <= tempCountScore)
                {
                    tempCountScore = GameManager.Instance.score;
                    currentScore_text.text = tempCountScore.ToString("0");
                    isScoreIncrease = false;
                }
            }

            yield return new WaitForSeconds(0.001f);
        }

        SoundManager.Instance.StopLoopSound("GameOver_Counting");

    }


    IEnumerator ResultMoneyIncrease_Co()
    {
        GameManager.Instance.ownMoney += GameManager.Instance.getMoney;
        
        bool isMoneyIncrease = true;
        int increseValue = CheckIncreaseValue(tempGetMoney);
        while (isMoneyIncrease)
        {
            if (tempGetMoney > 0)
            {
                tempGetMoney -= increseValue;
                getMoney_text.text = tempGetMoney.ToString();
            }
            if (tempOwnMoney <= GameManager.Instance.ownMoney && GameManager.Instance.getMoney != 0)
            {
                tempOwnMoney += increseValue;
                OwnMoney_text.text = tempOwnMoney.ToString();
            }

            if (tempGetMoney <= 0 && tempOwnMoney >= GameManager.Instance.ownMoney)
            {
                tempGetMoney = 0;
                tempOwnMoney = GameManager.Instance.ownMoney;
                getMoney_text.text = tempGetMoney.ToString();
                OwnMoney_text.text = tempOwnMoney.ToString();
                Debug.Log("½ÇÇàµÊ");

                isMoneyIncrease = false;
            }

            yield return new WaitForSeconds(0.001f);
        }

        
    }

    private int CheckIncreaseValue(int number)
    {
        if (number <= 500)
            return 2;
        else if (number <= 1000)
            return 3;
        else if (number <= 4000)
            return 4;
        else if (number > 4000)
            return 6;

        return 1;
    }


    public void Restart_Button()
    {
        SceneManager.LoadScene(GameManager.Instance.GAME);
    }

    public void GoMain_Button()
    {
        SceneManager.LoadScene(GameManager.Instance.SHOP);
        SoundManager.Instance.ChangePlayBGMSound("Title_Song");

    }
}
