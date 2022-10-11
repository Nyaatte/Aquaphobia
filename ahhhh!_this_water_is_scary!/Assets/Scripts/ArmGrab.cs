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
        
    }

    // public void GrabAxis(float _g)
    // {
    //     //grab = _g;
    //     Debug.Log("Grab: " + grab);
    //     clawAnim.SetFloat(grabHash, grab);
    // }

}
