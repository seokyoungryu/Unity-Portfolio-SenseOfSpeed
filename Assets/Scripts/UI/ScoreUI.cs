using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ScoreUI : MonoBehaviour
{
    public float tempScoreCount;
    public float perScore;
    public float timeLimit;
    public float currentTime;

    public bool isScoreCheck;
    public bool isScoreOverHighscore = false;
    public bool isLeftRightClick = false;
    public Text scoreName_Text;
    public TMP_Text score_Text;
    public TMP_Text gameOverPanel_NewHighScore_Text;
    public TMP_Text highScore_Text;

    private void Start()
    {
        isScoreOverHighscore = false;
        score_Text.text = "0";
        highScore_Text.text = GameManager.Instance.highScore.ToString();
    }

    void Update()
    {
        ChangeGameValue();

        if (isScoreCheck && !GameManager.Instance.isGameOver)
        {
            tempScoreCount += (perScore * ( 1 + GameManager.Instance.scorePercent)) * Time.deltaTime;
            GameManager.Instance.score = (int)tempScoreCount;
            score_Text.text = GameManager.Instance.score.ToString("0");

            if (GameManager.Instance.score > GameManager.Instance.highScore)
            {
                GameManager.Instance.highScore = GameManager.Instance.score;
                score_Text.color = Color.red;
                scoreName_Text.color = Color.red;
                isScoreOverHighscore = true;
                gameOverPanel_NewHighScore_Text.gameObject.SetActive(true);
            }

            if (isLeftRightClick)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= timeLimit)
                {
                    isScoreCheck = false;
                }
            }
        }

    }

    private void ChangeGameValue()
    {
        if(!GameManager.Instance.isTreasureBoxZone && !GameManager.Instance.isCrash)
        {
            if (GameManager.Instance.score >= (700 * (1 + GameManager.Instance.scorePercent)))
            {
                PlayerMoveController.Instance.SetPlayerForwordSpeed(17);
                PlayerMoveController.Instance.SetPlayerRightSpeed(0.6f);
                GameManager.Instance.decreaseHpPerSecondValue = 7;
            }
            else if (GameManager.Instance.score >= (2000 * (1 + GameManager.Instance.scorePercent)))
            {
                PlayerMoveController.Instance.SetPlayerForwordSpeed(18);
                PlayerMoveController.Instance.SetPlayerRightSpeed(0.6f);
                GameManager.Instance.decreaseHpPerSecondValue = 10;
            }
            else if (GameManager.Instance.score >= (4000 * (1 + GameManager.Instance.scorePercent)))
            {
                GameManager.Instance.isHardRound = true;
                PlayerMoveController.Instance.SetPlayerForwordSpeed(19);
                PlayerMoveController.Instance.SetPlayerRightSpeed(0.6f);
                GameManager.Instance.decreaseHpPerSecondValue = 20;
            }
            else if (GameManager.Instance.score >= (6000 * (1 + GameManager.Instance.scorePercent)))
            {
                PlayerMoveController.Instance.SetPlayerForwordSpeed(20);
                GameManager.Instance.decreaseHpPerSecondValue = 30;
            }
            else if (GameManager.Instance.score >= (8000 * (1 + GameManager.Instance.scorePercent)))
            {
                PlayerMoveController.Instance.SetPlayerForwordSpeed(21);
                GameManager.Instance.decreaseHpPerSecondValue = 40;
            }
        }

    }
    public void ScoreStart()
    {
        isLeftRightClick = false;
        isScoreCheck = true;
        currentTime = 0;
    }


}
