using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [Header("Raycast distance")]
    [SerializeField] private float distance;
 
    [Header("Ray Start Transform")]
    [SerializeField] private Transform rayPosCenter;
    [SerializeField] private Transform rayPosRight1; // center 와 right(끝) 중간
    [SerializeField] private Transform rayPosRight2;
    [SerializeField] private Transform rayPosLeft1; // center 와 left(끝) 중간
    [SerializeField] private Transform rayPosLeft2;


    public LayerMask layerWall;
    public LayerMask layerLimitLine;

    private PlayerMoveController thePlayerMoveController;
    private RaycastHit hitInfo;
    private RaycastHit hitInfo_LimitLine;
   
    private void Start()
    {
        thePlayerMoveController = GetComponent < PlayerMoveController>();

    }

    private void Update()
    {
        PlayerWallRayCheck();
        RayLimitLine();
    }

    private void PlayerWallRayCheck()
    {

        //센터 레이캐스트
        RayCheck(rayPosCenter.position, distance);

        //오른쪽 중간 레이캐스트
        RayCheck(rayPosRight1.position, distance);
        //오른쪽 끝 레이캐스트
        RayCheck(rayPosRight2.position, distance);

        //왼쪽 중간 레이캐스트
        RayCheck(rayPosLeft1.position, distance);
        //왼쪽 끝 레이캐스트
        RayCheck(rayPosLeft2.position, distance);

    }


    private void RayCheck(Vector3 rayPos, float distance)
    {
        if (Physics.Raycast(rayPos, transform.forward, out hitInfo, distance, layerWall ))
        {
            PlayerMoveController.Instance.Die("normal");
        }
    }

    //보물상자 제한선 레이캐스트 
    private void RayLimitLine()
    {
        if(Physics.Raycast(rayPosCenter.position,transform.forward, out hitInfo_LimitLine, distance, layerLimitLine))
        {
            hitInfo_LimitLine.transform.GetComponent<TreasureLimitLine>().isEnterPlayer = true;
        }
    }
 

}
