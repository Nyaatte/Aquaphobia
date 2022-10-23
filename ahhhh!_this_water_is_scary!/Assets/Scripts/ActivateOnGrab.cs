using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;
using System;

public class ActivateOnGrab : MonoBehaviour
{
    public SteamVR_Action_Single trigger;

    public SteamVR_Input_Sources hand;
    [SerializeField] private float grabThreshold = 0.9f;
    public SteamVR_ActionSet activateActionSet;

    private Transform homePosition;

    public UnityEvent onGrab;
    public UnityEvent onRelease;

    [SerializeField] private ParticleSystem grabParticle;

    public bool grabbed;
    public bool handSet;
    private bool reset;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (grabbed)
        {
            if (trigger.GetAxis(hand) > grabThreshold && grabbed && reset == false)
            {
                DisableControl();
            }

            if (trigger.GetAxis(hand) < grabThreshold && grabbed && reset)
            {
                reset = false;
            }

        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("Hand") && !handSet)
        {
            hand = col.gameObject.GetComponent<Hand>().handType;
            handSet = true;
        }
        
        
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name.Contains("Hand"))
        {
            if (handSet == false || grabbed) return;
            if (trigger.GetAxis(hand) > grabThreshold)
            {
                EnableControl();
                Debug.Log(col.gameObject);
                var obj = col.transform.Find("ObjectAttachmentPoint").gameObject;
                Debug.Log(obj);
                this.transform.parent = col.transform;
                this.transform.position = obj.transform.position;

                

            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name.Contains("Hand"))
        {
            if (col.gameObject.GetComponent<Hand>().handType == hand)
            {
                hand = SteamVR_Input_Sources.Any;
                handSet = false;
            }
            
        }
    }

    private void EnableControl()
    {
        Debug.Log("Enable Invoked");
        onGrab.Invoke();
        grabbed = true;
        reset = true;
        grabParticle.Play();
        Debug.Log("Object grabbed = " + grabbed);
        if (activateActionSet != null)
            activateActionSet.Activate(hand);

    }

    private void DisableControl()
    {
        onRelease.Invoke();
        grabbed = false;
        //hand = SteamVR_Input_Sources.Any;
        if (activateActionSet != null)
        activateActionSet.Deactivate(hand);
        //handSet = false;
        grabParticle.Play();
        Debug.Log("Disable Invoked");
        
    }
}
