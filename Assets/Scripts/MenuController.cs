using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private AudioSource buttonSound;

    private void Start()
    {
        buttonSound = GameObject.FindGameObjectWithTag("ButtonSound").GetComponent<AudioSource>();
    }

    public void ClickStart() {
        buttonSound.Play();
        SceneManager.LoadScene("LevelSelector");   
    }
}
