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
    [SerializeField] private float torque;
    [SerializeField] private float angularDampen = 0.8f;
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


        dir /= Time.fixedDeltaTime; //caculate distance to travel to reach position in the next frame
        dir = Vector3.ClampMagnitude(dir, Vector3.Distance(grabbedObject.transform.position, grabPosition.position)); //clamp it down

        objectRb.velocity = (dir * moveSpeed);

        //roate object by applying torque
        Quaternion rot = Quaternion.FromToRotation(grabbedObject.transform.up, grabPosition.transform.up);

        

        // Quaternion delta = Quaternion.Euler(new Vector3(rot.x, rot.y, rot.z) * torque * Time.fixedDeltaTime);
        

        objectRb.MoveRotation(objectRb.rotation * rot);

        //objectRb.AddTorque(-objectRb.angularVelocity * torque, ForceMode.Acceleration);
        //objectRb.AddTorque(axis.normalized * angle * torque, ForceMode.Acceleration);
    }
        

    

}
