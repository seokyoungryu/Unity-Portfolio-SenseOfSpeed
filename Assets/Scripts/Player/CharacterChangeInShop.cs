using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CharacterChangeInShop : MonoBehaviour
{
    public Character currentCharacter;
    public Character previewCharacter;


    public Character[] characters;

    public int previewNumber = 0;

    public CharacterRotate theCharacterRotate;

    public Text characterName_Kor_Text;
    public Button select_Btn;
    public Button buy_Btn;
    public GameObject info_Panel;
    public Text[] info_Ability_text;
    public TMP_Text ownMoney_text;
    public TMP_Text cost_text;
    public Image coin_Img;
    public Material lock_Mat;
    public Material Unlock_Mat;

    private void Start()
    {
        InfoAbilityTextSet(0);

        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].isOwn = GameManager.Instance.characterIsOwn[i];

        }

        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].gameObject.SetActive(false);
        }

        previewCharacter.gameObject.SetActive(true);

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].isOwn == false)
                characters[i].gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = Unlock_Mat;
            else if (characters[i].isOwn == true)
                characters[i].gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = lock_Mat;
        }


    }


    private void Update()
    {
        ownMoney_text.text = GameManager.Instance.ownMoney.ToString();
    }

    private void Change(string name)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].CharacterName == name && characters[i].isOwn)
            {
                currentCharacter = characters[i];
                GameManager.Instance.currentCharacterName = currentCharacter.CharacterName;
                GameManager.Instance.characterIsOwn[i] = characters[i].isOwn;


            }
        }

    }


    private void Preview(int num)
    {
        if (num > (characters.Length - 1))
            previewNumber = 0;
        else if (num < 0)
            previewNumber = (characters.Length - 1);

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].previewNum == previewNumber)
            {
                Debug.Log("¹Ù²ñ");
                previewCharacter.gameObject.SetActive(false);
                previewCharacter = characters[i];
                previewCharacter.gameObject.SetActive(true);
                theCharacterRotate.ResetCharacterRotation();
                characterName_Kor_Text.text = characters[i].Name_Kor;
                if (characters[i].isOwn)
                {
                    SelectBtnActiveTrue();
                    InfoAbilityTextSet(characters[i].previewNum);
                    cost_text.text = "";
                    coin_Img.gameObject.SetActive(false);
                }
                else if (!characters[i].isOwn)
                {
                    BuyBtnActiveTrue();

                    cost_text.text = characters[i].cost.ToString();
                    coin_Img.gameObject.SetActive(true);
                    if (characters[i].cost <= GameManager.Instance.ownMoney)
                        cost_text.color = Color.black;
                    else if (characters[i].cost > GameManager.Instance.ownMoney)
                        cost_text.color = Color.red;
                }

            }


        }

    }

    private void InfoAbilityTextSet(int num)
    {
        for (int a = 0; a < info_Ability_text.Length; a++)
        {
            info_Ability_text[a].text = "";
        }


        int count = characters[num].ability.Length;

        for (int i = 0; i < count; i++)
        {
            info_Ability_text[i].text = characters[num].ability[i];
            Debug.Log(characters[num].ability[i]);

        }

    }

    public void BuyCharacter_Btn()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].previewNum == previewNumber)
            {
                if (!characters[i].isOwn)
                {
                    if (GameManager.Instance.ownMoney >= characters[i].cost)
                    {
                        GameManager.Instance.ownMoney -= characters[i].cost;
                        coin_Img.gameObject.SetActive(false);
                        theCharacterRotate.ResetCharacterRotation();
                        characters[i].isOwn = true;
                        GameManager.Instance.characterIsOwn[i] = true;
                        characters[i].GetComponentInChildren<SkinnedMeshRenderer>().material = lock_Mat;
                        cost_text.text = "";
                        SelectBtnActiveTrue();
                        InfoAbilityTextSet(characters[i].previewNum);
                        SoundManager.Instance.PlayEffectSound("Character_Buy");
                        GameManager.Instance.json.SaveData();
                    }
                }
            }
        }
    }


    public void SelectBtnActiveTrue()
    {
        info_Panel.SetActive(true);
        select_Btn.gameObject.SetActive(true);
        buy_Btn.gameObject.SetActive(false);
    }

    public void BuyBtnActiveTrue()
    {
        info_Panel.SetActive(false);
        select_Btn.gameObject.SetActive(false);
        buy_Btn.gameObject.SetActive(true);
    }


    public void RightPreview_Btn()
    {
        Preview(++previewNumber);
    }

    public void LeftPreview_Btn()
    {
        Preview(--previewNumber);
    }

    public void Select_Btn()
    {
        Change(previewCharacter.CharacterName);
        SceneManager.LoadScene(GameManager.Instance.GAME);

    }

}

