using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReCycleFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float amount;
    private void Update()
    {
        if (!GameManager.Instance.isTreasureCamRotZone)
            FollowTarget();
        else if (GameManager.Instance.isTreasureCamRotZone)
            FollowTargetInTreasureZone();
    }


    private void FollowTarget()
    {
        Vector3 targetPos = new Vector3(0, 0, target.position.z) + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, amount * Time.deltaTime);

    }

    private void FollowTargetInTreasureZone()
    {
        Vector3 targetPos = new Vector3(0, 0, target.position.z -40) + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, amount * Time.deltaTime);

    }
}
