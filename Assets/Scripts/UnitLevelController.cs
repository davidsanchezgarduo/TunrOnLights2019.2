using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitLevelController : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public GameObject unitPrefab;
    public UnitScriptableObject unitsScriptable;
    public Transform container;

    private float widthButton = 300;
    private float offsetButton = 50;

    public AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        pointsText.text ="Puntos: "+ DataController.instance.playerData.points;

        for (int i = 0; i < unitsScriptable.units.Length; i++) {
            GameObject g = Instantiate(unitPrefab,Vector3.zero,Quaternion.identity);
            g.transform.parent = container;
            g.GetComponent<RectTransform>().localPosition = Vector3.zero;
            g.GetComponent<RectTransform>().anchoredPosition = new Vector2(((i + 1) * offsetButton) + (i * widthButton), 0);
            g.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            
            g.GetComponent<UnitUpgradeController>().Init(unitsScriptable.units[i],DataController.instance.unitsData.units[i].level,i,this);
            
        }
    }

    public bool HasPoints(int needPoints, int unitId) {
        buttonSound.Play();
        if (DataController.instance.playerData.points >= needPoints) {
            DataController.instance.playerData.points -= needPoints;
            DataController.instance.SaveData();
            pointsText.text = "Puntos: " + DataController.instance.playerData.points;
            DataController.instance.unitsData.units[unitId].level++;
            DataController.instance.SaveUnitsData();
            return true;
        }
        return false;
    }
}
