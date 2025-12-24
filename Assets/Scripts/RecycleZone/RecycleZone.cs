using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleZone : MonoBehaviour
{
    [SerializeField] private Transform recycleRayPos;
    [SerializeField] private float distance;
    public LayerMask lay;
    private RaycastHit hitInfo;

   
    private void Update()
    {
        if(Physics.Raycast(recycleRayPos.position,Vector3.forward,out hitInfo, distance, lay))
        {
            Obstacle hitObs = hitInfo.transform.GetComponent<Obstacle>();

            if (hitObs.tag == "treasureObs")
                hitObs.GetComponent<TreasureBox>().ResetFragmentValue();
            else
                ObjectPooling.Instance.SetOBP(hitObs.objName, hitInfo.transform.gameObject);


            MapCreator.Instance.ObstacleCreate(1);
        }

        Debug.DrawRay(recycleRayPos.position, Vector3.forward * distance,Color.red);
                                            
    }


    private void OnTriggerStay(Collider other)
    {
       if (other.tag == "Ground")
        {
            Ground otherIsGround = other.GetComponent<Ground>();
            ObjectPooling.Instance.SetOBP(otherIsGround.objName, otherIsGround.gameObject);
            MapCreator.Instance.GroundCreate(1);
        }

     
    }

   
}
