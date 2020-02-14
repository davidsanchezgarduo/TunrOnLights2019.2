using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitUpgradeController : MonoBehaviour
{
    public Image mySprite;
    public TextMeshProUGUI stadisticsText;
    public Text levelButtonText;
    private UnitContext myContext;
    public int currentLevel;
    public Button levelUpButton;
    private int myId;
    private UnitLevelController main;

    public void Init(UnitContext context, int level, int id, UnitLevelController _main) {
        myContext = context;
        main = _main;
        myId = id;
        mySprite.sprite = context.image;
        currentLevel = level;
        PrintData();
        
    }

    public void LevelUp() {
        if (main.HasPoints(myContext.levelsDescription[currentLevel].priceLevelUp,myId)) {
            currentLevel++;
            PrintData();
        }
    }

    private void PrintData() {
        if (currentLevel < myContext.levelsDescription.Length)
        {
            levelButtonText.text = "Level Up " + myContext.levelsDescription[currentLevel].priceLevelUp;
            stadisticsText.text = "Name: " + myContext.typeName + "\nFuerza: " + myContext.levelsDescription[currentLevel + 1].forceAttack + "\nRango: " + myContext.levelsDescription[currentLevel + 1].lightRange +
                "\nVelocidad: " + myContext.levelsDescription[currentLevel + 1].speedAttack + "\nVida: " + myContext.levelsDescription[currentLevel + 1].lives;
        }
        else
        {
            levelButtonText.text = "Mex Level";
            stadisticsText.text = "Name: " + myContext.typeName + "\nFuerza: " + myContext.levelsDescription[currentLevel].forceAttack + "\nRango: " + myContext.levelsDescription[currentLevel].lightRange +
                "\nVelocidad: " + myContext.levelsDescription[currentLevel].speedAttack + "\nVida: " + myContext.levelsDescription[currentLevel].lives;
            levelUpButton.interactable = false;
        }
    }
}
