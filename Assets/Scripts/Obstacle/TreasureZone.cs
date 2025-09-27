using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureZone : MonoBehaviour
{
    [Header("TreasureZone Speed Value")]
    [SerializeField] private float treasureZone_SpeedMin;
    [SerializeField] private float treasureZone_SpeedMax;
    [SerializeField] private float tempCurrentSpeed;

   
    public float GetTempSpeed()
    {
        return tempCurrentSpeed;
    }
    
  
 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           
            GameManager.Instance.isTreasureBoxZone = true;
            PlayerMoveController.Instance.attackBtn_Panel.SetActive(true);

            tempCurrentSpeed = other.GetComponent<PlayerMoveController>().GetCurrentSpeed();
            float randomSpeed = Random.Range(treasureZone_SpeedMin, treasureZone_SpeedMax);
            float applySpeedPercent = randomSpeed * (1 - GameManager.Instance.treasureZoneDecreaseSpeed);
            PlayerMoveController.Instance.SetPlayerForwordSpeed(randomSpeed);
            PlayerMoveController.Instance.playerAnim.SetBool("IsTreasureZone_Run", GameManager.Instance.isTreasureBoxZone);
          
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerMoveController.Instance.attackBtn_Panel.SetActive(false);

            GameManager.Instance.isTreasureBoxZone = false;
            PlayerMoveController.Instance.SetPlayerForwordSpeed(tempCurrentSpeed);
            PlayerMoveController.Instance.playerAnim.SetBool("IsTreasureZone_Run", GameManager.Instance.isTreasureBoxZone);

            if(GameManager.Instance.isHit == false && !GameManager.Instance.isPassBox)
            {
                GameManager.Instance.isPassBox = true;
                PlayerMoveController.Instance.DamageUI();
                GameManager.Instance.currentHp -= GameManager.Instance.crashValue * (1 - GameManager.Instance.decreaseCrashDamage);
                SoundManager.Instance.PlayEffectSound("Damage");


            }

        }
    }
}
