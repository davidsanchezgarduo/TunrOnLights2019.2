using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuLevelUpgradeController : MonoBehaviour
{

    public GameObject LevelMenu;
    public GameObject UpgradeMenu;
    public Text buttonText;
    private AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        buttonText.text = "Upgrade";
        LevelMenu.SetActive(true);
        UpgradeMenu.SetActive(false);
        buttonSound = GameObject.FindGameObjectWithTag("ButtonSound").GetComponent<AudioSource>();
        GetComponent<LevelSelectorController>().buttonSound = buttonSound;
        GetComponent<UnitLevelController>().buttonSound = buttonSound;
    }

    public void ChangeMenu() {
        buttonSound.Play();
        if (LevelMenu.activeSelf)
        {
            buttonText.text = "Levels";
            LevelMenu.SetActive(false);
            UpgradeMenu.SetActive(true);
        }
        else {
            buttonText.text = "Upgrade";
            LevelMenu.SetActive(true);
            UpgradeMenu.SetActive(false);
        }
    }
}
