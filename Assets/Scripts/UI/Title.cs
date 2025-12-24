using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    private void Start()
    {
        SoundManager.Instance.ChangePlayBGMSound("Title_Song");

    }
   

    public void TouchToShop_Btn()
    {
        SceneManager.LoadScene(GameManager.Instance.SHOP);
    }
}
