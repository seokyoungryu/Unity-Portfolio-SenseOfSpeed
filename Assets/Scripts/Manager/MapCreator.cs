using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    private static MapCreator instance;
    public static MapCreator Instance {
        get
        {
            return instance;
        }
    }

    [Header("Ground")]
    [SerializeField] private GameObject startGround;
    [SerializeField] private GameObject[] grounds_arr;
    [SerializeField] private int StartGroundCount;
    private float groundSizeZ;
    private Vector3 nextGroundPos;

    [Header("Obstacle")]
    [SerializeField] private GameObject startObatacle;
    [SerializeField] private GameObject[] obstacles_arr;  // 순서 맨 처음 NormalObs -> HardObs -> TresureBox 순 
    [SerializeField] private float normalObsDistance;
    [SerializeField] private float hardObsDistance;
    [SerializeField] private int StartObstacleCount;
    [SerializeField] private string prevObstacleName;
    [SerializeField] private int countCurrentObs;
    [SerializeField] private int countNextTreasure;

    [Header("Treasure Box")]
    [SerializeField] private float[] treasureObsDistance;
    [SerializeField] private float makedTreasureNextObsDistance;
    
    [Header("Model Count")]
    [SerializeField] private int HowManyNormalObsModel;
    [SerializeField] private int HowManyHardObsModel;
    [SerializeField] private int HowManyTreasureBoxModel;

   
    private float ObsDistance;
    private Vector3 nextObstaclePos;

    private bool isMakedTreasure = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isHardRound)
            countNextTreasure = 2;
    }

    private void Start()
    {
        nextGroundPos = startGround.transform.position;
        groundSizeZ = startGround.transform.localScale.z;

        GroundCreate(StartGroundCount);

        nextObstaclePos = startObatacle.transform.position;
        ObstacleCreate(StartObstacleCount);
    }

    public void GroundCreate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, grounds_arr.Length);
            GameObject tempGround = ObjectPooling.Instance.GetOBP(grounds_arr[rand].GetComponent<Ground>().objName);
            tempGround.transform.position = nextGroundPos + new Vector3(0, 0, groundSizeZ);
            nextGroundPos = tempGround.transform.position;

        }
    }

    public void ObstacleCreate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject tempObs = Check();

            if (tempObs.tag == "normalObs" )
            {
                if (isMakedTreasure)
                    ObsDistance = normalObsDistance + makedTreasureNextObsDistance;
                else
                    ObsDistance = normalObsDistance;
               // tempObs.GetComponent<HPItemCreate>().ItemCreate();
                isMakedTreasure = false;
            }
            else if(tempObs.tag == "hardObs")
            {
                if (isMakedTreasure)
                    ObsDistance = hardObsDistance + makedTreasureNextObsDistance;
                else
                    ObsDistance = hardObsDistance;
               // tempObs.GetComponent<HPItemCreate>().ItemCreate();
                isMakedTreasure = false;
            }
            else if (tempObs.tag == "treasureObs")
            {
                if (!GameManager.Instance.isHardRound)
                    ObsDistance = treasureObsDistance[0];
                else if (GameManager.Instance.isHardRound)
                    ObsDistance = treasureObsDistance[0] + 15;
                isMakedTreasure = true;
            }

            tempObs.transform.position = nextObstaclePos + new Vector3(0, 0, ObsDistance);
            nextObstaclePos = tempObs.transform.position;



        }
    }


    private GameObject Check()
    {
        bool tempBool = true;
        while (tempBool)
        {
            int rand = MakeRandomObstacleOrTreasureBox();
            string thisObsName = obstacles_arr[rand].GetComponent<Obstacle>().objName;
           
            if (thisObsName != prevObstacleName)
            {
                tempBool = false;
                GameObject tempObs = ObjectPooling.Instance.GetOBP(obstacles_arr[rand].GetComponent<Obstacle>().objName);
                prevObstacleName = thisObsName;
                return tempObs;
            }

        }

        return null;
    }

    private int MakeRandomObstacleOrTreasureBox()
    {
       
        if( countCurrentObs < countNextTreasure)
        {   
            if(!GameManager.Instance.isHardRound)
            {
                int normalObsRandomNumber = Random.Range(0, obstacles_arr.Length - HowManyTreasureBoxModel - HowManyHardObsModel);
                countCurrentObs += 1;
                return normalObsRandomNumber;
            }
           else if(GameManager.Instance.isHardRound)
            {
                int hardObsRandomNumber = Random.Range(obstacles_arr.Length - HowManyHardObsModel - HowManyTreasureBoxModel, obstacles_arr.Length - HowManyTreasureBoxModel);
                countCurrentObs += 1;
                return hardObsRandomNumber;
            }
        }
        else if(countCurrentObs >= countNextTreasure)
        {

            int TreasureBoxNumber = Random.Range(obstacles_arr.Length - HowManyTreasureBoxModel, obstacles_arr.Length);
            countCurrentObs = 0;
            return TreasureBoxNumber;
        }

        return 1;
    }
}
