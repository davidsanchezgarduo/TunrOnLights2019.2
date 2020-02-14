using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonController : MonoBehaviour
{
    public int myId;
    public TextMeshProUGUI myText;
    private LevelSelectorController main;

    public void Init(int levelId, string name, LevelSelectorController _main) {
        myText.text = "Level " + (levelId + 1) + "\n" + name;
        myId = levelId;
        main = _main;
    }

    public void ClickButton() {
        main.SelectLevel(myId);
    }
}
