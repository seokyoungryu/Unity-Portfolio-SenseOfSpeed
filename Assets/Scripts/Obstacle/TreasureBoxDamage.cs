using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBoxDamage : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerMoveController.Instance.Die("treasureZone");

        }
    }
}
