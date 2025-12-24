using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotate : MonoBehaviour
{
    public Vector3 current_Rot;
    public Vector3 origin_Rot;

    private void Start()
    {
        origin_Rot = transform.eulerAngles;
    }

    void Update()
    {
        transform.eulerAngles += current_Rot * Time.deltaTime;
    }


    public void ResetCharacterRotation()
    {
        transform.eulerAngles = origin_Rot;
    }
}
