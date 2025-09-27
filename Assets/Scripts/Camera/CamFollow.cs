using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private float amount;
    [SerializeField] private float camPosXMoveLimit;
    [SerializeField] private float camPosXMove;
   
    [SerializeField] private Vector3 offset;

    [Header("Cam Rotate")]
    [SerializeField] private Vector3 originRot;
    [SerializeField] private Vector3 treasureZoneRot;
    [SerializeField] private float treasureZoneRotAmount;
    [SerializeField] private float returnRotAmount;
    [SerializeField] private Transform player;
    [SerializeField] private Camera cam;
    private float playerPosX;
    private bool isCheck = true;

    void Update()
    {
       if(GameManager.Instance.isTreasureCamRotZone)
        {
            cam.transform.eulerAngles = Vector3.Lerp(cam.transform.eulerAngles, treasureZoneRot, treasureZoneRotAmount * Time.deltaTime);
            isCheck = false;
        }
        else if(!GameManager.Instance.isTreasureCamRotZone && !isCheck)
        {
            cam.transform.eulerAngles = Vector3.Lerp(cam.transform.eulerAngles, originRot, returnRotAmount * Time.deltaTime);
            if (cam.transform.eulerAngles == originRot )
                isCheck = true;

        }

        Vector3 camPos = offset + player.transform.position;
        transform.position = Vector3.Lerp(transform.position, camPos, amount * Time.deltaTime);



    }

    private void UpdatePlayerPosX()
    {
        playerPosX = player.position.x;

        if (playerPosX >= camPosXMoveLimit)
            offset.x = camPosXMove;
        else if (playerPosX <= -camPosXMoveLimit)
            offset.x = -camPosXMove;
        else
            offset.x = 0;
    }
}
