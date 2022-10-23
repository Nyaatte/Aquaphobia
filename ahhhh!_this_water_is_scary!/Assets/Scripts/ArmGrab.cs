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
    [SerializeField] private float minDist;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask layer;
    private bool objectGrabbed;
    private Rigidbody objectRb;
    private bool reset; //used to 'reset' the trigger grab, so that it only happens once. is set false once checked and then when the trigger pull is less than the threshold it returns true


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

        
        if (trigger.GetAxis(onGrab.hand) > grabThreshold && reset)
        {
            HandleObjectPickup();
        }

        if (!reset && trigger.GetAxis(onGrab.hand) < grabThreshold)
        {
            reset = true;
        }

        
        
        
    }

    private void FixedUpdate()
    {
        MoveObject();
    }

    private void HandleObjectPickup()
    {
        reset = false; //make sure this function is not called again until the player releases the trigger below the threshold

        if (objectGrabbed)
        {
            grabbedObject = null;
            objectRb = null;
            objectGrabbed = false;
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(rayshooter.position, rayshooter.TransformDirection(Vector3.forward), out hit, grabRange, layer))
            {
                if (hit.transform.gameObject.CompareTag("Object"))
                {
                    objectGrabbed = true;
                    grabbedObject = hit.transform.gameObject;
                    objectRb = grabbedObject.GetComponent<Rigidbody>();
                }
            }
        }
        
    }

    private void MoveObject()
    {
        if (!objectGrabbed) return;
        if (objectRb == null)
        {
            objectRb = grabbedObject.GetComponent<Rigidbody>();
            return;
        }
        
        float frameStep = (moveSpeed*Time.deltaTime);
        Vector3 dir =  (grabPosition.position - grabbedObject.transform.position).normalized;


        dir /= Time.fixedDeltaTime; //it will reach the position immediately with this calculation
        dir = Vector3.ClampMagnitude(dir, Vector3.Distance(grabbedObject.transform.position, grabPosition.position)); //clamp it down

        objectRb.velocity = (dir * moveSpeed);

        //if ((Vector3.Distance(grabbedObject.transform.position, grabPosition.position)) < ) //i was going to make it so when its at the position the velocity is 0 but then you wouldn't be able to throw it auuughhhhhhh
        
        /*
        float hookshotSpeed = Mathf.Clamp(Vector3.Distance(player.transform.position, hookshotPosition), hookshotSpeedMin, hookshotSpeedMax); //Limit the speed between two variables        
        rb.MovePosition(player.transform.position + hookshotDir * hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime); //Move towards the hookshot target position
        FUCK OFF DONT USE THIS
        */ 

        //grabbedObject.transform.position = Vector3.MoveTowards(grabbedObject.transform.position, grabPosition.position, frameStep); // no it stays on the ground
        //objectRb.MovePosition(grabbedObject.transform.position + dir * moveSpeed); //i didn't know you could have negative infinity
        //objectRb.AddForce(dir * moveSpeed, ForceMode.Impulse); //goodbye
    }

    

}
