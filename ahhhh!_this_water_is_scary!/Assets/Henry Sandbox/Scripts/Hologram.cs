using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


[RequireComponent(typeof(Collider))]
public class Hologram : MonoBehaviour
{

    public GameObject hologram;
    public GameObject hologramReferencePoint;

    public Vector3 currentPosition;

    private Vector3 mouseScroll;
    [SerializeField] float zPosition;

[SerializeField]
    private bool isControllerDefaultPosition;


    public Vector3 positionDifference;

    // Start is called before the first frame update
    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        zPosition = transform.position.z;
        positionDifference = new Vector3(0f, 0f, 0f);
        isControllerDefaultPosition = true;
    }

    private void Update()
    {
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (hologram == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("hologram"))
                    {
                        return;
                    }

                    hologram = hit.collider.gameObject;

                }
            }
            

            else
            { //when the hologram is released from hand
                if (isControllerDefaultPosition)
                {
                    hologram.transform.position = hologramReferencePoint.transform.position;
                    hologram = null;
                    positionDifference = new Vector3(0, 0, 0);
                }
                else{
                    hologram.transform.position = currentPosition;
                    hologram = null;
                }
            }
            
        }
        */

        
            

            /*
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(hologram.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            worldPosition.z += mouseScroll.y;
            hologram.transform.position = new Vector3(Mathf.Clamp(worldPosition.x, hologramReferencePoint.transform.position.x - 10, hologramReferencePoint.transform.position.x + 10), Mathf.Clamp(worldPosition.y, hologramReferencePoint.transform.position.y - 10, hologramReferencePoint.transform.position.y + 10), Mathf.Clamp(worldPosition.z, hologramReferencePoint.transform.position.z - 10, hologramReferencePoint.transform.position.z + 10));
            */
            
            currentPosition = hologram.transform.position;
            positionDifference = hologramReferencePoint.transform.position - currentPosition;
        
        /*
        if (Input.GetKey("s"))
        {
            isControllerDefaultPosition = !isControllerDefaultPosition;
        }
        */
    }

    /*
    //Ignore this method, only for mouse.
    private RaycastHit CastRay()
    {
        Vector3 screenmousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);

        Vector3 screenmousePosNear = new Vector3(
        Input.mousePosition.x,
        Input.mousePosition.y,
        Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenmousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenmousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
    */
}
