using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{

    public int ownMoney;
    public int highScore;
    public bool[] characterIsOwn;
    
}


public class JsonSave : MonoBehaviour
{

    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile3.txt";

   

    private void Start()
    {
        saveData.characterIsOwn = new bool[GameManager.Instance.characterIsOwn.Length];

        SAVE_DATA_DIRECTORY = Application.persistentDataPath + "/Saves/";
        //SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if(!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
            Debug.Log("생성!");

        }

    }


    public void SaveData()
    {
        saveData.ownMoney = GameManager.Instance.ownMoney;
        saveData.highScore = GameManager.Instance.highScore;
        saveData.characterIsOwn = GameManager.Instance.characterIsOwn;
      

      
        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);

    }

   
    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
          
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            GameManager.Instance.ownMoney = saveData.ownMoney;
            GameManager.Instance.highScore = saveData.highScore;
            GameManager.Instance.characterIsOwn = saveData.characterIsOwn;

            Debug.Log("로드 완료");
        }
        else
        {
            Debug.Log("세이브 파일 없음");

        }

    }

  
}
