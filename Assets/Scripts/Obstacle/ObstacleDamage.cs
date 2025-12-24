using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{

    public Material damage_Mat;
    public Material noDamage_Mat;

    private GameObject player;

    
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SoundManager.Instance.PlayEffectSound("Damage");
            GameManager.Instance.currentHp -= 45;

                if (GameManager.Instance.currentHp > 0)
                    StartCoroutine(HitMaterialEffect_Co());

                if (GameManager.Instance.currentHp <= 0)
                {
                    Debug.Log("½ÇÇà!");
                    PlayerMoveController.Instance.Die("normal");
                }
           
        }
       

    }



    IEnumerator HitMaterialEffect_Co()
    {
        GameManager.Instance.isCrash = true;
        GameManager.Instance.isHpDecreaseStop = true;
        PlayerMoveController.Instance.DamageUI();
        player = PlayerMoveController.Instance.playerAnim.gameObject;
        float tempSpeed = PlayerMoveController.Instance.GetCurrentSpeed();
        PlayerMoveController.Instance.SetPlayerForwordSpeed(tempSpeed * 0.4f);
        player.GetComponentInChildren<SkinnedMeshRenderer>().material = damage_Mat;
        yield return new WaitForSeconds(0.2f);
        player.GetComponentInChildren<SkinnedMeshRenderer>().material = noDamage_Mat;
        yield return new WaitForSeconds(0.2f);
        player.GetComponentInChildren<SkinnedMeshRenderer>().material = damage_Mat;
        yield return new WaitForSeconds(0.2f);
        player.GetComponentInChildren<SkinnedMeshRenderer>().material = noDamage_Mat;
        yield return new WaitForSeconds(0.2f);
        player.GetComponentInChildren<SkinnedMeshRenderer>().material = damage_Mat;
        yield return new WaitForSeconds(0.1f);
        player.GetComponentInChildren<SkinnedMeshRenderer>().material = noDamage_Mat;
        PlayerMoveController.Instance.SetPlayerForwordSpeed(tempSpeed);
        GameManager.Instance.isHpDecreaseStop = false;
        GameManager.Instance.isCrash = false;
    }



   
}
