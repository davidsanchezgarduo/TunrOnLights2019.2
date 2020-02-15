using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private AudioSource buttonSound;
    public GameObject creditsPanel;
    public GameObject menuPanel;

    private void Start()
    {
        ReturnMenu();
        buttonSound = GameObject.FindGameObjectWithTag("ButtonSound").GetComponent<AudioSource>();
    }

    public void ClickStart() {
        buttonSound.Play();
        SceneManager.LoadScene("LevelSelector");   
    }

    public void ClickCredits() {
        creditsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void ReturnMenu() {
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
