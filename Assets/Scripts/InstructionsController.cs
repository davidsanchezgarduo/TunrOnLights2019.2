using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsController : MonoBehaviour
{

    public GameObject[] InsPanels;
    private int currentPanel;
    // Start is called before the first frame update
    void Start()
    {
        currentPanel = 0;
        for(int i = 0; i < InsPanels.Length; i++)
        {
            InsPanels[i].SetActive(false);
        }
        InsPanels[currentPanel].SetActive(true);

    }


    public void ClickNext() {

        if (currentPanel >= InsPanels.Length-1)
        {
            SceneManager.LoadScene("HotelLevel");
        }
        else
        {
            InsPanels[currentPanel].SetActive(false);
            currentPanel++;
            InsPanels[currentPanel].SetActive(true);
        }
    }
}
