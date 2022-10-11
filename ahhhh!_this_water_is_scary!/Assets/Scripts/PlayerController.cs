using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;
    private CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
        charController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - (new Vector3(0, 9.81f/2, 0) * Time.deltaTime));
    }
    
}
