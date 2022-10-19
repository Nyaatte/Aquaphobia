using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ArmGrab : MonoBehaviour
{
    public SteamVR_Action_Single trigger = SteamVR_Input.GetAction<SteamVR_Action_Single>("submarine", "Grab");
    [SerializeField] private Animator clawAnim;
    private ActivateOnGrab onGrab;

    [SerializeField] private GameObject grabbedObject;
    [SerializeField] private Transform grabPosition;
    [SerializeField] private Transform rayshooter;
    [SerializeField] private float grabThreshold = 0.9f;
    [SerializeField] private float grabRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask layer;
    private bool objectGrabbed;


    private float grab;
    private int grabHash = Animator.StringToHash("grab");

    private void Start()
    {
        if (onGrab == null) onGrab = GetComponent<ActivateOnGrab>();
    }

    private void Update()
    {
        if (onGrab.grabbed)
        {
            grab = trigger.GetAxis(onGrab.hand);
            Debug.Log("Grab: " + grab);
            clawAnim.SetFloat(grabHash, grab);
        }

        
        if (trigger.GetAxis(onGrab.hand) > grabThreshold)
        {
            HandleObjectPickup();
        }

        MoveObject();
        
        
    }

    private void HandleObjectPickup()
    {
        if (objectGrabbed)
        {
            grabbedObject = null;
            objectGrabbed = false;
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(rayshooter.position, rayshooter.TransformDirection(Vector3.forward), out hit, grabRange, layer))
            {
                if (hit.transform.gameObject.CompareTag("putsomethingheredickhead"))
                {
                    objectGrabbed = true;
                    grabbedObject = hit.transform.gameObject;
                }
            }
        }
        
    }

    private void MoveObject()
    {
        if (!objectGrabbed) return;
        float frameStep = moveSpeed * Time.deltaTime;
        grabbedObject.transform.position = Vector3.MoveTowards(grabbedObject.transform.position, grabPosition.position, frameStep);
    }

}
