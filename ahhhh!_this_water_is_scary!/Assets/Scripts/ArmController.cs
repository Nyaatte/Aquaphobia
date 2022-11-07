using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArmController : MonoBehaviour
{
    [Header("Arm Transforms")]
    [SerializeField] private Transform controllerPosition;
    [SerializeField] private Transform controllerHomePosition;
    [SerializeField] private Transform armPosition;
    [SerializeField] private Transform armHomePosition;
    [SerializeField] private ActivateOnGrab onGrab;
    [SerializeField] private Rigidbody targetRb;
    [SerializeField] private float matchSpeed;


    [Header("Audio")]
    [SerializeField] private AudioSource audo;
    [SerializeField] private float playThreshold;
    private bool isPlaying;


    void Start()
    {
        
    }

    
    void Update()
    {
        

        // Vector3 controllerOffset = controllerHomePosition.position - controllerPosition.position;

        // armPosition.position = armHomePosition.position - controllerOffset;
        // armPosition.rotation = controllerPosition.rotation;
    }

    private void FixedUpdate() 
    {
        if (onGrab.grabbed)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        Vector3 controllerOffset = controllerHomePosition.position - controllerPosition.position;
        Vector3 target = armHomePosition.position - controllerOffset;
        

        Vector3 dir = (target - armPosition.position).normalized;
        
        
        dir /= Time.fixedDeltaTime; //calc distance to travel to target in next frame
        dir = Vector3.ClampMagnitude(dir, Vector3.Distance(armPosition.position, target));
        

        targetRb.velocity = (dir * matchSpeed);

        audo.pitch = (1 + targetRb.velocity.magnitude); //range between 0 and ~0.7
        armPosition.rotation = controllerPosition.rotation;

        //Handle arm audio
        if (isPlaying && dir.magnitude < playThreshold)
        {
            audo.Stop();
            isPlaying = false;
        }
        else if (!isPlaying && dir.magnitude > playThreshold)
        {
            audo.Play();
            isPlaying = true;
        }

        //float step = dir.magnitude;
        //armPosition.position = Vector3.MoveTowards(armPosition.position, target, step);


    }

    

    public void OnRelease()
    {
        targetRb.velocity = new Vector3(0, 0, 0);
        audo.Stop();
        isPlaying = false;
    }
}
