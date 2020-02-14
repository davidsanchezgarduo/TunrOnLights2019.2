using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class UnityCreator : MonoBehaviour
{
    public int typeId;
    public TextMeshProUGUI myName;
    private GameObject prefabObject;
    private GameObject currentUnity;
    private Material currentUnityMaterial;
    private Camera mainCamera;
    private Vector3 dir;
    private Color initialColor;
    public Image myImage;

    // Start is called before the first frame update
    public void Init(int id, Sprite s, string name)
    {
        typeId = id;
        myImage.sprite = s;
        myName.text = name;
        prefabObject = UnitiesManager.instance.GetUnityPrefab(typeId);
        mainCamera = Camera.main;
    }

    public void StartDrag(Vector2 posTouch) {
        CameraControl.instance.inMovement = false;
        //Obtener unity gameobject
        currentUnity = Instantiate(prefabObject, Vector3.zero, Quaternion.identity);
        currentUnityMaterial = currentUnity.GetComponent<MeshRenderer>().material;
        initialColor = currentUnityMaterial.color;
        /*Vector3 pos = mainCamera.ScreenToWorldPoint(new Vector3(posTouch.x,posTouch.y,10));
        Debug.Log("POsStart darg "+pos);

        currentUnity.transform.position = pos;*/
        UnityController controller = currentUnity.GetComponent<UnityController>();
        UIController.instance.SetUnitData(controller.socialName,controller.forceAttack,controller.speedAttack,controller.lightRange,controller.lives);
    }

    public void EndDrag(Vector2 posTouch)
    {
        CameraControl.instance.inMovement = true;
        if (posTouch.y < 200)
        {
            Destroy(currentUnity);
            currentUnity = null;
            currentUnityMaterial = null;
        }
        else {
            Vector3 pos = mainCamera.ScreenToWorldPoint(new Vector3(posTouch.x, posTouch.y, 5));
            dir = pos - mainCamera.transform.position;
            RaycastHit hitInfo;
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            if (Physics.Raycast(mainCamera.transform.position, dir, out hitInfo, 20,layerMask))
            {
                //Debug.Log(hitInfo.collider.name + ", " + hitInfo.collider.tag + " . " + hitInfo.point);
                currentUnity.transform.position = new Vector3(hitInfo.point.x, 0.5f, hitInfo.point.z);
                NavMeshHit hitNav;
                bool b1 = UnitiesManager.instance.CheckPosition(hitInfo.point);
                bool b2 = NavMesh.SamplePosition(hitInfo.point, out hitNav, 0.5f, NavMesh.AllAreas);
                //Debug.Log(b1+" . "+ b2);
                if (b1 && b2)
                {
                    currentUnityMaterial.color = initialColor;
                    UnitiesManager.instance.AddUnity(currentUnity,typeId);
                    currentUnity = null;
                    currentUnityMaterial = null;
                }
                else
                {
                    Destroy(currentUnity);
                    currentUnity = null;
                    currentUnityMaterial = null;
                }
            }
        }

        UIController.instance.ClearUnitData();


    }

    public void OnDrag(Vector2 posTouch)
    {
        if (posTouch.y > 200)
        {
            Vector3 pos = mainCamera.ScreenToWorldPoint(new Vector3(posTouch.x, posTouch.y, 5));
            dir = pos - mainCamera.transform.position;
            RaycastHit hitInfo;
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            if (Physics.Raycast(mainCamera.transform.position, dir, out hitInfo, 20,layerMask))
            { 
                currentUnity.transform.position = new Vector3(hitInfo.point.x,0.5f,hitInfo.point.z);
                NavMeshHit hitNav;
                bool b1 = UnitiesManager.instance.CheckPosition(hitInfo.point);
                bool b2 = NavMesh.SamplePosition(hitInfo.point, out hitNav, 0.5f, NavMesh.AllAreas);
                //Debug.Log(b1 + " . " + b2);
                if (b1 && b2)
                {
                    currentUnityMaterial.color = initialColor;
                }
                else {
                    currentUnityMaterial.color = Color.red;
                }
            }

        }
    }
}
