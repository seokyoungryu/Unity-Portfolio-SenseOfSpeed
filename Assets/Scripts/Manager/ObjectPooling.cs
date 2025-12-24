using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolData
{
    public string objName;
    public GameObject prefab;
    public Transform parentPos;
    public int count;

}

public class ObjectPooling : MonoBehaviour
{

    private static ObjectPooling instance;
    public static ObjectPooling Instance
    {
        get
        {
            return instance;
        }

    }

    public List<PoolData> obpDatas = new List<PoolData>();
    private Dictionary<string, Queue<GameObject>> diction_Obp = new Dictionary<string, Queue<GameObject>>();



    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }
       
        Initialization();
    }


    private void Initialization()
    {
        foreach (PoolData data in obpDatas)
        {
            Queue<GameObject> tempQ = new Queue<GameObject>();

            for (int i = 0; i < data.count; i++)
            {
                CreatePrefab(data, tempQ);
            }
            diction_Obp.Add(data.objName, tempQ);


        }
    }

    public void CreatePrefab(PoolData data, Queue<GameObject> tempQ)
    {
        GameObject temp_go = Instantiate(data.prefab);
        temp_go.SetActive(false);
        temp_go.transform.SetParent(data.parentPos);
        tempQ.Enqueue(temp_go);
        

    }

    public GameObject GetOBP(string dataName)
    {
        if (!diction_Obp.ContainsKey(dataName))
        {
            Debug.Log(dataName + "이 오브젝트 풀링에 존재하지 않음 ( GetOBP)");
            return null;
        }

        foreach (PoolData data in obpDatas)
        {
            if (dataName == data.objName)
            {
                if (diction_Obp[dataName].Count <= 0)
                {
                    CreatePrefab(data, diction_Obp[data.objName]);
                }

                GameObject temp_go = diction_Obp[dataName].Dequeue();
                temp_go.SetActive(true);
                return temp_go;
            }
        }

        return null;
    }

    public void SetOBP(string dataName, GameObject go)
    {
        if (!diction_Obp.ContainsKey(dataName))
        {
            Debug.Log(dataName + "이 오브젝트 풀링에 존재하지 않음 ( SetOBP)");
            return;
        }

        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        go.SetActive(false);
        diction_Obp[dataName].Enqueue(go);

    }

}
