using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;
    private Vector2 lastPosition;
    private Vector2 deltaMovement;
    private Vector3 movementVector;
    public float speedMovement;
    public Vector2 limitX;
    public Vector2 limitZ;
    public Vector2 startPosition;

    public bool inMovement;

    private bool paused;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        inMovement = true;
        transform.position = new Vector3(startPosition.x,transform.position.y, startPosition.y);
        lastPosition = new Vector2(-1,-1);
    }

    // Update is called once per frame
    void Update()
    {

        if (paused) {
            return;
        }

#if UNITY_ANDROID
        if (Input.touchCount > 0) {
            Debug.Log("Input "+Input.GetTouch(0).position);
        }
#else
        if (Input.GetMouseButtonUp(0) && !GameManager.instance.inHorde)
        {
            //.Log("Mouse up");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            if (Physics.Raycast(ray, out hit, 30, layerMask))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.CompareTag("Door")) {
                    hit.collider.GetComponent<DoorControl>().OpenDoors();
                }
            }
        }

        if (Input.GetMouseButton(0)) {
            if (inMovement)
            {
                if (UIController.instance.menuOpen && Input.mousePosition.y < 200)
                {


                }
                else
                {
                    if (lastPosition.x == -1)
                    {
                        lastPosition = Input.mousePosition;
                    }
                    else
                    {
                        deltaMovement.x = -(Input.mousePosition.x - lastPosition.x);
                        deltaMovement.y = -(Input.mousePosition.y - lastPosition.y);

                        if ((deltaMovement.x * speedMovement) + transform.position.x > limitX.y)
                        {
                            deltaMovement.x = limitX.y - transform.position.x;
                        }
                        else if ((deltaMovement.x * speedMovement) + transform.position.x < limitX.x)
                        {
                            deltaMovement.x = limitX.x - transform.position.x;
                        }
                        else
                        {
                            deltaMovement.x *= speedMovement;
                        }

                        if ((deltaMovement.y * speedMovement) + transform.position.z > limitZ.y)
                        {
                            deltaMovement.y = limitZ.y - transform.position.z;
                        }
                        else if ((deltaMovement.y * speedMovement) + transform.position.z < limitZ.x)
                        {
                            deltaMovement.y = limitZ.x - transform.position.z;
                        }
                        else
                        {
                            deltaMovement.y *= speedMovement;
                        }

                        transform.Translate(new Vector3(deltaMovement.x, 0, deltaMovement.y));

                        lastPosition = Input.mousePosition;

                    }
                }
            }
            else {
                //Posicionando Objeto

            }
        }
        else{
            //Debug.Log("No mouse");
            if (!inMovement) {

            }

            lastPosition.x = -1;
            lastPosition.y = -1;
        }
#endif

    }

    public void PausedGame(bool p) {
        paused = p;
    }
}
