using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float lightRange;
    private Transform lightArea;
    // Start is called before the first frame update
    void Start()
    {
        lightArea = transform.GetChild(0);
        //lightArea.localScale = new Vector3(lightRange*2,0.01f, lightRange*2);

        RaycastHit hit;
        int layerMask = 1 << 8;
        Ray ray = new Ray(new Vector3(transform.position.x,transform.position.y + 30, transform.position.z), -transform.up);
        if (Physics.Raycast(ray, out hit, 30))
        {
            if (hit.transform.CompareTag("Shadow"))
            {
                hit.transform.GetComponent<ShadowController>().SetGoalLight(hit.textureCoord, lightRange);
                //hit.transform.GetComponent<ShadowController>().SetUnit(hit.textureCoord, lightRange);
            }

        }
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
