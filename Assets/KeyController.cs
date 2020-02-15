using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public int keyId;
    public bool canGet;

    void Start() {
        canGet = false;
        UnitiesManager.instance.AddKey(this);
    }

    public void GetKey() {
        if (canGet)
        {
            GameManager.instance.AddKey(keyId);
            UnitiesManager.instance.RemoveKey(this);
            Destroy(this.gameObject);
        }
    }

    public void OnKey() {
        canGet = true;
    }

    public void OutKey()
    {
        canGet = false;
    }
}
