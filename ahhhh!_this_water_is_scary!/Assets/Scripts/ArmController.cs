using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] private Transform controllerPosition;
    [SerializeField] private Transform controllerHomePosition;
    [SerializeField] private Transform armPosition;
    [SerializeField] private Transform armHomePosition;

    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 controllerOffset = controllerHomePosition.position - controllerPosition.position;

        armPosition.position = armHomePosition.position - controllerOffset;
        armPosition.rotation = controllerPosition.rotation;
    }
}
