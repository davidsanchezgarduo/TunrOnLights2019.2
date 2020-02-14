using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateHelper : MonoBehaviour
{
    public void EndAnim() {
        gameObject.SetActive(false);
    }

    public void EndPause() {
        Time.timeScale = 0;
    }
}
