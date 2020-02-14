using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LevelSelectorController : MonoBehaviour
{
    public Transform container;
    public GameObject levelButtonPrefab;
    public LevelScriptableObject levelsData;
    public TextMeshProUGUI levelDataText;
    public Button startGameButton;

    private float widthButton = 300;
    private float offsetButton = 50;
    private int currentLevel;

    public AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        levelDataText.text = "";
        for (int i = 0; i < levelsData.levels.Length; i++) {
            GameObject g = Instantiate(levelButtonPrefab,Vector3.zero, Quaternion.identity);
            g.transform.parent = container;
            g.GetComponent<RectTransform>().localPosition = new Vector2(((i+1)*offsetButton)+(i*widthButton),0);
            g.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            g.GetComponent<LevelButtonController>().Init(i,levelsData.levels[i].name,this);
        }
    }

    public void SelectLevel(int id) {
        buttonSound.Play();
        levelDataText.text = "Level "+(id+1)+" - "+ levelsData.levels[id].name+"\nCiviles: "+ levelsData.levels[id].civilPositions.Length;
        currentLevel = id;
        startGameButton.interactable = true;
    }

    public void ClickStart() {
        buttonSound.Play();
        DataController.instance.LastLevelSelected = levelsData.levels[currentLevel].sceneName;
        SceneManager.LoadScene(levelsData.levels[currentLevel].sceneName);
    }

    public void ClickReturn()
    {
        buttonSound.Play();
        SceneManager.LoadScene("MenuScene");
    }



}
