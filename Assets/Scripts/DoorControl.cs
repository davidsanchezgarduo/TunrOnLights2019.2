﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DoorControl : MonoBehaviour
{
    public Animator myAnim;
    public SpotController[] spots;
    public bool canOpen;
    public Material doorOutlineMaterial;
    private Material initialMaterial;
    public MeshRenderer door1;
    public MeshRenderer door2;
    public GameObject hiddenArea;
    public int keyId = -1;
    private void Start()
    {
        UnitiesManager.instance.AddDoor(this);

        //myRender = GetComponent<MeshRenderer>();
        initialMaterial = door1.material;
    }

    public void OpenDoors() {
        if (canOpen)
        {
            if (keyId == -1)
            {
                hiddenArea.SetActive(false);
                myAnim.SetTrigger("Open");
                OutDooor();
                for (int i = 0; i < spots.Length; i++)
                {
                    spots[i].isActive = true;
                }
            }
            else {
                if (GameManager.instance.CheckKey(keyId))
                {
                    hiddenArea.SetActive(false);
                    myAnim.SetTrigger("Open");
                    OutDooor();
                    for (int i = 0; i < spots.Length; i++)
                    {
                        spots[i].isActive = true;
                    }
                }
                else {
                    UIController.instance.ShowAlertKey();
                }
            }
        }
    }

    public void OnDoor()
    {
        //if (canOpen) {
        canOpen = true;
        door1.material = doorOutlineMaterial;
        door2.material = doorOutlineMaterial;
        //
    }

    public void OutDooor() {
        door1.material = initialMaterial;
        door2.material = initialMaterial;
    }
}
