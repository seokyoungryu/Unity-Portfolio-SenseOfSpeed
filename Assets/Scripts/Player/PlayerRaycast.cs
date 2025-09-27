using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [Header("Raycast distance")]
    [SerializeField] private float distance;
 
    [Header("Ray Start Transform")]
    [SerializeField] private Transform rayPosCenter;
    [SerializeField] private Transform rayPosRight1; // center �� right(��) �߰�
    [SerializeField] private Transform rayPosRight2;
    [SerializeField] private Transform rayPosLeft1; // center �� left(��) �߰�
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

        //���� ����ĳ��Ʈ
        RayCheck(rayPosCenter.position, distance);

        //������ �߰� ����ĳ��Ʈ
        RayCheck(rayPosRight1.position, distance);
        //������ �� ����ĳ��Ʈ
        RayCheck(rayPosRight2.position, distance);

        //���� �߰� ����ĳ��Ʈ
        RayCheck(rayPosLeft1.position, distance);
        //���� �� ����ĳ��Ʈ
        RayCheck(rayPosLeft2.position, distance);

    }


    private void RayCheck(Vector3 rayPos, float distance)
    {
        if (Physics.Raycast(rayPos, transform.forward, out hitInfo, distance, layerWall ))
        {
            PlayerMoveController.Instance.Die("normal");
        }
    }

    //�������� ���Ѽ� ����ĳ��Ʈ 
    private void RayLimitLine()
    {
        if(Physics.Raycast(rayPosCenter.position,transform.forward, out hitInfo_LimitLine, distance, layerLimitLine))
        {
            hitInfo_LimitLine.transform.GetComponent<TreasureLimitLine>().isEnterPlayer = true;
        }
    }
 

}
