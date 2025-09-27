using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float radius;

    [SerializeField] private Vector3 forcePosOffset;
    [SerializeField] private Transform forceTransform;
    [SerializeField] private GameObject treasureBox_go;
    [SerializeField] private GameObject treasureBoxFragments_go;

    [SerializeField] private Material change_Mat;
    [SerializeField] private Material unChange_Mat;

    private Transform[] fragment_Tr;
    private Vector3[] saveFragmentPos;
    private Vector3[] saveFragmentRot;

    public TreasureZone theTreasureZone;
    public TreasureLimitLine theTreasureLimitLine;
    [SerializeField] private bool isCheckHit = false;

    private void Awake()
    {
        theTreasureZone = GetComponentInChildren<TreasureZone>();
        theTreasureLimitLine = GetComponentInChildren<TreasureLimitLine>();
        treasureBox_go.GetComponentInChildren<MeshRenderer>().material = unChange_Mat;
        fragment_Tr = treasureBoxFragments_go.GetComponentsInChildren<Transform>();

        saveFragmentPos = new Vector3[fragment_Tr.Length];
        saveFragmentRot = new Vector3[fragment_Tr.Length];


    }

    private void Start()
    {
        for (int i = 0; i < fragment_Tr.Length; i++)
        {
            saveFragmentPos[i] = fragment_Tr[i].transform.localPosition;
            saveFragmentRot[i] = fragment_Tr[i].transform.localEulerAngles;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isPassBox)
            TreasureColorOrigin();
        else
            TreasureColorChange();

        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.isHitCheckBtn)
        {
            if (GameManager.Instance.isTreasureBoxZone && !theTreasureLimitLine.isEnterPlayer)
                isCheckHit = true;

            if (isCheckHit) // 제한선 아닐떄 클릭시 
            {

                GameManager.Instance.currentHp -= (GameManager.Instance.crashValue * (1 - GameManager.Instance.decreaseCrashDamage));
                PlayerMoveController.Instance.DamageUI();
                GameManager.Instance.isPassBox = true;
                isCheckHit = false;
            }
            else  
                InputCheckInLimitLine();

            GameManager.Instance.isHitCheckBtn = true;
        }
    }

    private void TreasureColorChange()
    {
        treasureBox_go.GetComponentInChildren<MeshRenderer>().material = change_Mat;

    }
    private void TreasureColorOrigin()
    {
        treasureBox_go.GetComponentInChildren<MeshRenderer>().material = unChange_Mat;

    }

    private void BreakTreasureBox()
    {
        treasureBox_go.SetActive(false);
        treasureBoxFragments_go.SetActive(true);
        Rigidbody[] fragments_rg = treasureBoxFragments_go.GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < fragments_rg.Length; i++)
        {
            fragments_rg[i].AddExplosionForce(force, forceTransform.position, radius);
        }
    }

    private void InputCheckInLimitLine()
    {
        if (theTreasureLimitLine.isEnterPlayer && GameManager.Instance.isTreasureLimitLine)
        {
            BreakTreasureBox();
            GameManager.Instance.isHit = true;
            PlayerMoveController.Instance.SetPlayerForwordSpeed(theTreasureZone.GetTempSpeed());
            PlayerMoveController.Instance.theMoneyUI.RandomGetMoney(10,200);
            GameManager.Instance.currentHp += 8f;
            MapCreator.Instance.ObstacleCreate(1);
            PlayerMoveController.Instance.playerAnim.SetTrigger("Hit");
            SoundManager.Instance.PlayEffectSound("TreasureBox_Break");

            GameManager.Instance.isHpDecreaseStop = false;
            theTreasureLimitLine.isEnterPlayer = false;
            GameManager.Instance.isTreasureCamRotZone = false;
            GameManager.Instance.isTreasureBoxZone = false;
            GameManager.Instance.isTreasureLimitLine = false;
            PlayerMoveController.Instance.playerAnim.SetBool("IsTreasureZone_Run", GameManager.Instance.isTreasureBoxZone);

            PlayerMoveController.Instance.attackBtn_Panel.SetActive(false);

        }

    }

    
    public void ResetFragmentValue()
    {
        GameManager.Instance.isHitCheckBtn = false;
        GameManager.Instance.isPassBox = false;
        GameManager.Instance.isHit = false;
        for (int i = 0; i < fragment_Tr.Length; i++)
        {
            fragment_Tr[i].localEulerAngles = saveFragmentRot[i];
            fragment_Tr[i].localPosition = saveFragmentPos[i];
        }
        treasureBoxFragments_go.SetActive(false);
        treasureBox_go.SetActive(true);
        ObjectPooling.Instance.SetOBP(this.GetComponent<Obstacle>().objName, this.gameObject);
    }


    public void Attack_Btn()
    {
        if (!GameManager.Instance.isHitCheckBtn)
        {
            if (GameManager.Instance.isTreasureBoxZone && !theTreasureLimitLine.isEnterPlayer)
                isCheckHit = true;

            if (isCheckHit) // 제한선 아닐떄 클릭시 
            {
                SoundManager.Instance.PlayEffectSound("Damage");
                GameManager.Instance.currentHp -= GameManager.Instance.crashValue * (1 - GameManager.Instance.decreaseCrashDamage);
                PlayerMoveController.Instance.DamageUI();
                GameManager.Instance.isPassBox = true;
                isCheckHit = false;
            }
            else
                InputCheckInLimitLine();

            GameManager.Instance.isHitCheckBtn = true;
        }
    }

   
}
