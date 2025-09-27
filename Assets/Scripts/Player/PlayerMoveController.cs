using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private static PlayerMoveController instance;
    public static PlayerMoveController Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] private float currentSpeed;
    [SerializeField] private float forwordSpeed;
    [SerializeField] private float rightLeftSpeed;

    private Vector3 moveDir;
    private bool isCheckAnim = false;

    public Animator playerAnim;
    public Animator shadowAnim;

    public DamageEffectUI theDamageEffectUI;
    private CharacterChange theCharacterChange;
    public TreasureBox theTreasureBox;

    public MoneyUI theMoneyUI;
    public ScoreUI theScoreUI;

    public GameObject moveBtn_Panel;
    public GameObject attackBtn_Panel;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        moveDir = Vector3.forward;
        SoundManager.Instance.ChangePlayBGMSound("BGM_1");
    }

    private void Start()
    {
        theCharacterChange = GetComponentInChildren<CharacterChange>();
        for (int i = 0; i < theCharacterChange.characters.Length; i++)
        {
            if (theCharacterChange.characters[i].CharacterName == GameManager.Instance.currentCharacterName)
            {
                playerAnim = theCharacterChange.characters[i].GetComponent<Animator>();
            }
        }

    }

    private void Update()
    {
        if (!GameManager.Instance.isGameOver)
            MoveControll();



    }


    private void MoveControll()
    {
        if (Input.GetKeyDown(KeyCode.A) && !GameManager.Instance.isTreasureBoxZone && GameManager.Instance.isGameStart)
        {
            transform.rotation = Quaternion.LookRotation(-Vector3.right);
            currentSpeed = forwordSpeed * rightLeftSpeed;
            theScoreUI.isLeftRightClick = true;

        }
        if (Input.GetKeyDown(KeyCode.D) && !GameManager.Instance.isTreasureBoxZone && GameManager.Instance.isGameStart)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.right);
            currentSpeed = forwordSpeed * rightLeftSpeed;
            theScoreUI.isLeftRightClick = true;

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
            currentSpeed = forwordSpeed;
            GameManager.Instance.isGameStart = true;
            playerAnim.SetBool("IsGameStart_Run", GameManager.Instance.isGameStart);
            shadowAnim.SetBool("IsGameStart", GameManager.Instance.isGameStart);
            theScoreUI.ScoreStart();


        }


        transform.Translate(currentSpeed * moveDir * Time.deltaTime);

    }

    public void SetPlayerMoveZero()
    {
        moveDir = Vector3.zero;
        currentSpeed = 0;

    }

    public void SetPlayerForwordSpeed(float value)
    {
        currentSpeed = value;
        forwordSpeed = value;
    }

    public void SetPlayerRightSpeed(float value)
    {
        rightLeftSpeed = value;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public void Die(string type)
    {
        SetPlayerMoveZero();
        GameManager.Instance.isGameOver = true;

        if (!isCheckAnim)
        {
            if (type == "normal")
                playerAnim.SetTrigger("Die_Normal");
            else if (type == "treasureZone")
                playerAnim.SetTrigger("Die_InTreasureZone");

            shadowAnim.SetTrigger("Die");
            isCheckAnim = true;
        }

    }

    public void DamageUI()
    {
        theDamageEffectUI.DamageUI();
        
    }

    public void Treasure_Attack_Btn()
    {
        if (theTreasureBox != null)
            theTreasureBox.Attack_Btn();
        else
            Debug.Log("theTreasureBox¿¡ ÇÒ´ç ¾ÈµÊ");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TreasureCamRotZone")
        {
            theTreasureBox = other.GetComponentInParent<TreasureBox>();
            GameManager.Instance.isTreasureCamRotZone = true;
            GameManager.Instance.isHpDecreaseStop = true;
            SoundManager.Instance.PlayEffectSound("FindTreasure");
           
        }

      
    }

    public void Front_Btn()
    {
      
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
            currentSpeed = forwordSpeed;
            GameManager.Instance.isGameStart = true;
            playerAnim.SetBool("IsGameStart_Run", GameManager.Instance.isGameStart);
            shadowAnim.SetBool("IsGameStart", GameManager.Instance.isGameStart);
            theScoreUI.ScoreStart();
    }

    public void Left_Btn()
    {
        if (!GameManager.Instance.isTreasureBoxZone && GameManager.Instance.isGameStart)
        {
            transform.rotation = Quaternion.LookRotation(-Vector3.right);
            currentSpeed = forwordSpeed * rightLeftSpeed;
            theScoreUI.isLeftRightClick = true;

        }
       
    }
    public void Right_Btn()
    {
        if (!GameManager.Instance.isTreasureBoxZone && GameManager.Instance.isGameStart)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.right);
            currentSpeed = forwordSpeed * rightLeftSpeed;
            theScoreUI.isLeftRightClick = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TreasureCamRotZone")
        {
            GameManager.Instance.isTreasureCamRotZone = false;
            GameManager.Instance.isHpDecreaseStop = false;

        }
    }

}
