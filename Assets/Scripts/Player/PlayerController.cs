using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
 

    [SerializeField] private float playerSpeed;
    [SerializeField] private float tempTreasureSpeed;
    [SerializeField] private int moveKeyNumber = 1;


   
    private void Update()
    {
        Run();
        MoveLeftRight();
    }


    private void Run()
    {
        transform.position += playerSpeed * Vector3.forward * Time.deltaTime;
    }

    private void MoveLeftRight()
    {
        if (Input.GetKeyDown(KeyCode.A))
            SetMoveKeyNumber(moveKeyNumber - 1);
        else if (Input.GetKeyDown(KeyCode.D))
            SetMoveKeyNumber(moveKeyNumber + 1);

    }

    private void SetMoveKeyNumber(int num)
    {

        if (num > 2)
            num = 2;
        else if (num < 0)
            num = 0;

        if (num == moveKeyNumber)
            return;
        else
            moveKeyNumber = num;
        
        Vector3 tempPos = transform.position;
        
        if (num == 0)
            tempPos.x = -3;
        else if (num == 1)
            tempPos.x =  0;
        else if (num == 2)
            tempPos.x =  3;

        transform.position = tempPos;
    }


}
