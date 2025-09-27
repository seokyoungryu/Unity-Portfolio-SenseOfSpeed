using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureLimitLine : MonoBehaviour
{
    public bool isEnterPlayer;

   
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnterPlayer = true;
            GameManager.Instance.isTreasureLimitLine = true;
           
        }
      

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnterPlayer = false;
            GameManager.Instance.isTreasureLimitLine = false;
        }

    }
}
