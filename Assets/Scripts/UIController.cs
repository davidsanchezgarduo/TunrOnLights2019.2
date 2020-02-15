using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public TextMeshProUGUI dataText;
    public Animator menuAnimator;
    public Animator pauseAnimator;
    public Button menuButton;
    private TextMeshProUGUI menuButtonText;
    public TextMeshProUGUI hordeText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI civilsText;
    public bool menuOpen;
    public UnitScriptableObject unitScriptable;
    public GameObject unitPrefab;
    public Transform content;
    public GameObject gameOverPanel;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI restantZombies;

    public GameObject endLevelButton;

    private float widthButton = 120;
    private float offsetButton = 30;

    public int TotalCivils;
    public GameObject NeedKey;
    public TextMeshProUGUI restantUnits;
    private int totalEnemies;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        NeedKey.SetActive(false);
        endLevelButton.SetActive(false);
        gameOverPanel.SetActive(false);
        pauseAnimator.gameObject.SetActive(false);
        dataText.text = "Unidad: \nFuerza: \nVelocidad: \nRango: \nVitalidad: ";
        hordeText.text = "Horda: 0";
        menuButtonText = menuButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //civilsText.text = "Civils 0/";
        menuOpen = false;

        for (int i = 0; i < unitScriptable.units.Length; i++) {
            GameObject g = Instantiate(unitPrefab, Vector3.zero, Quaternion.identity);
            g.transform.parent = content;
            g.GetComponent<RectTransform>().localPosition = Vector3.zero;
            g.GetComponent<RectTransform>().anchoredPosition = new Vector2(((i + 1) * offsetButton) + (i * widthButton), 50);
            g.GetComponent<RectTransform>().localRotation = Quaternion.identity;
            g.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            g.GetComponent<UnityCreator>().Init(i,unitScriptable.units[i].image, unitScriptable.units[i].typeName);
        }
    }

    public void EnterMenu() {
        //CameraControl.instance.inMovement = false;
        menuAnimator.SetTrigger("In");
        menuOpen = true;
    }

    public void CloseMenu() {
        //CameraControl.instance.inMovement = true;
        menuAnimator.SetTrigger("Out");
        menuOpen = false;
    }

    public void ClickPause() {
        pauseAnimator.gameObject.SetActive(true);
        pauseAnimator.SetTrigger("In");

        UnitiesManager.instance.PausedGame(true);
        EnemyGenerator.instance.PausedGame(true);
        CameraControl.instance.PausedGame(true);
        GameManager.instance.gamePaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        pauseAnimator.SetTrigger("Out");
        UnitiesManager.instance.PausedGame(false);
        EnemyGenerator.instance.PausedGame(false);
        CameraControl.instance.PausedGame(false);
        GameManager.instance.gamePaused = false;
    }

    public void ExitGame() {
        SceneManager.LoadScene("MenuScene");
    }

    public void SetUnitData(string _name, float _force, float speed, float _range, int _live) {
        dataText.text = "Unidad: "+ _name + "\nFuerza: "+ _force + "\nVelocidad: "+ speed + "\nRango: "+ _range + "\nVitalidad: "+ _live;
    }
    public void ClearUnitData() {
        dataText.text = "Unidad: \nFuerza: \nVelocidad: \nRango: \nVitalidad: ";
    }

    public void SetHorde(int h,int enemies)
    {
        if (menuOpen)
        {
            CloseMenu();
        }
        totalEnemies = enemies;
        menuButton.interactable = false;
        menuButtonText.text = "Zombies: "+ totalEnemies;
        hordeText.text = "Horda: " + h;
    }

    public void SetRestantZombies() {
        totalEnemies--;
        menuButtonText.text = "Zombies: " + totalEnemies;
    }

    public void FinishHorde() {
        endLevelButton.SetActive(false);
        menuButton.interactable = true;
        menuButtonText.text = "Unidades";
    }

    public void SetLivesText(int lives) {
        livesText.text = "" + lives;
    }

    public void SetCivils(int civils)
    {
        Debug.Log(""+ TotalCivils);
        civilsText.text = "Civils: " + civils+"/"+TotalCivils;
    }

    public void FinishGame(bool win) {
        UnitiesManager.instance.PausedGame(true);
        EnemyGenerator.instance.PausedGame(true);
        CameraControl.instance.PausedGame(true);
        GameManager.instance.gamePaused = true;
        if (win)
        {
            messageText.text = "Ganaste";
        }
        else {
            messageText.text = "Perdiste";
        }

        gameOverPanel.SetActive(true);
    }

    public void ClickContinue() {
        SceneManager.LoadScene("LevelSelector");
    }

    public void ClickRetry() {
        SceneManager.LoadScene(DataController.instance.LastLevelSelected);
    }

    public void ActivateEndLevel() {
        endLevelButton.SetActive(true);
    }

    public void ClickEndLevel() {
        EnemyGenerator.instance.ActivateEnd();
    }

    public void ShowAlertKey() {
        NeedKey.SetActive(true);
        StartCoroutine(WaitAlertKey());
    }

    IEnumerator WaitAlertKey() {
        yield return new WaitForSeconds(2f);
        NeedKey.SetActive(false);
    }

    public void SetUnits(int res) {
        restantUnits.text = "Unidades: " + res;
    }

}
