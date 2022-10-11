using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ShowController : MonoBehaviour
{
    private bool controllerShown = false;
    
    public bool ControllerShown
    {
        get
        {
            return controllerShown;
        }
        set
        {
            controllerShown = value;
            Debug.Log("i fucked");
            foreach(var hand in Player.instance.hands)
            {
                if(controllerShown)
                {
                    hand.ShowController();
                    hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithController);
                }
                else
                {
                    hand.HideController();
                    hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);
                }
            }
        }
    }

    public void ToggleControllers()
    {
        ControllerShown = !controllerShown;
    }


}
