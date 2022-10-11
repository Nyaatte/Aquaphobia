using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class IndexInput : MonoBehaviour
{

    [SerializeField] private SteamVR_Action_Single squeezeAction;
    [SerializeField] private SteamVR_Action_Vector2 touchpadAction;

    
    private void Update()
    {
        if(SteamVR_Actions._default.Teleport.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("TeleportDown");
        }

        if(SteamVR_Actions._default.GrabPinch.GetStateUp(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Pinched your balls");
        }

        float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.Any);

        if (triggerValue > 0f)
        {
            Debug.Log("squeezing your balls");
        }
    }
}
