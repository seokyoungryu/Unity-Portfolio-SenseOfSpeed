using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChange : MonoBehaviour
{
    public Character[] characters;



    private void Start()
    {
        ChangeCharacter();
        ApplyAbility();
    }

    private void ChangeCharacter()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].gameObject.SetActive(false);
        }

        foreach (Character characterObj in characters)
        {
            if(characterObj.CharacterName == GameManager.Instance.currentCharacterName)
            {
                characterObj.gameObject.SetActive(true);
                GameManager.Instance.MaxHp = characterObj.MaxHp;
                GameManager.Instance.currentHp = characterObj.MaxHp;


                return;
            }
        }
    }


    private void ApplyAbility()
    {
        foreach (Character characterObj in characters)
        {
            if (characterObj.CharacterName == GameManager.Instance.currentCharacterName)
            {
                GameManager.Instance.GoldPercent = characterObj.GoldPercent;
                GameManager.Instance.healPercent = characterObj.healPercent;
                GameManager.Instance.scorePercent = characterObj.scorePercent;
                GameManager.Instance.decreaseHpPercent = characterObj.decreaseHpPercent;
                GameManager.Instance.treasureZoneDecreaseSpeed = characterObj.treasureZoneDecreaseSpeed;
                GameManager.Instance.decreaseCrashDamage = characterObj.decreaseCrashDamage;


                return;
            }
        }
     

}
}
