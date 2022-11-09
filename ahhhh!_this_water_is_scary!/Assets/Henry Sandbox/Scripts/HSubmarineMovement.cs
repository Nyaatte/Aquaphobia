using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class HSubmarineMovement : MonoBehaviour
{
    private Interactable interactable;

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Hologram hologram;
    [SerializeField]
    private float curSpeed;
    
    [SerializeField]
    private GameObject hologramInteractable;

    [SerializeField]
    Vector3 positionDifference;



    [SerializeField]
    private float zMinThreshold;
    [SerializeField]
    private float yMinThreshold;
    [SerializeField]
    private float xMinThreshold;

    [SerializeField]
    private float zMaxThreshold;
    [SerializeField]
    private float yMaxThreshold;
    [SerializeField]
    private float xMaxThreshold;

    [Header("Movement Settings")]

    private float minThrustSpeed;
    private float minRotateSpeed;
    private float minFloatSpeed;

    [SerializeField]
    private float maxThrustSpeed;

    [SerializeField]
    public float maxRotateSpeed;
    [SerializeField]
    public float maxFloatSpeed;

    private Vector3 EulerAngleLeftVelocity;
    private Vector3 EulerAngleRightVelocity;

    [SerializeField]
    private float currentThrustSpeed;
    [SerializeField]
    private float currentRotateSpeed;
    [SerializeField]
    private float currentFloatSpeed;

    public float thrustPercentageChange;
    public float rotatePercentageChange;
    public float floatPercentageChange;
    void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        hologram = hologramInteractable.GetComponent<Hologram>();
        minFloatSpeed = 0f;
        minThrustSpeed = 0f;
        minRotateSpeed = 0f;
        interactable = GetComponent<Interactable>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        positionDifference = hologram.positionDifference;
        calculateLerpScale();
        MovementDirection();


    }

    private void MovementDirection()
    {
        if (hologram.positionDifference.y < -yMinThreshold)
        {  //down
            currentFloatSpeed = Mathf.Lerp(minFloatSpeed, maxFloatSpeed, floatPercentageChange);
            rb.AddForce(-transform.up * currentFloatSpeed);
            Debug.Log("Going Down");
        }

        if (hologram.positionDifference.y > yMinThreshold)
        {  //up
            currentFloatSpeed = Mathf.Lerp(minFloatSpeed, maxFloatSpeed, floatPercentageChange);
            rb.AddForce(transform.up * currentFloatSpeed);
            Debug.Log("Going Up");
        }

        if (hologram.positionDifference.z < -zMinThreshold)
        {  //backward
            currentThrustSpeed = Mathf.Lerp(minThrustSpeed, maxFloatSpeed, thrustPercentageChange);
            rb.AddForce(-transform.forward * maxThrustSpeed);
            Debug.Log("Going Backwards");
        }

        if (hologram.positionDifference.z > zMinThreshold)
        {  //forward
            currentThrustSpeed = Mathf.Lerp(minThrustSpeed, maxThrustSpeed, thrustPercentageChange);
            rb.AddForce(transform.forward * currentThrustSpeed);
            Debug.Log("Going Forwards");
        }

        if (hologram.positionDifference.x < -xMinThreshold)
        {  //left
            currentRotateSpeed = Mathf.Lerp(minRotateSpeed, maxRotateSpeed, rotatePercentageChange);
            EulerAngleLeftVelocity = new Vector3(0, currentRotateSpeed, 0);
            Quaternion deltaLeftRotation = Quaternion.Euler(
                    EulerAngleLeftVelocity * Time.fixedDeltaTime
                );
            rb.MoveRotation(rb.rotation * deltaLeftRotation);
            Debug.Log("Turning Left");
        }

        if (hologram.positionDifference.x > xMinThreshold)
        {  //right
            currentRotateSpeed = Mathf.Lerp(minRotateSpeed, maxRotateSpeed, rotatePercentageChange);
            EulerAngleRightVelocity = new Vector3(0, -currentRotateSpeed, 0);
            Quaternion deltaRightRotation = Quaternion.Euler(
                    EulerAngleRightVelocity * Time.fixedDeltaTime
                );

            rb.MoveRotation(rb.rotation * deltaRightRotation);
            Debug.Log("Turning Right");
        }
    }

    private void calculateLerpScale()
    {
        if (positionDifference.z > zMinThreshold)
        {
            thrustPercentageChange = (positionDifference.z - zMinThreshold) / zMaxThreshold;
            if (thrustPercentageChange > 1)
            {
                thrustPercentageChange = 1;
            }
        }

        if (positionDifference.z < -zMinThreshold)
        {
            thrustPercentageChange = (-positionDifference.z - zMinThreshold) / zMaxThreshold;
            if (thrustPercentageChange > 1)
            {
                thrustPercentageChange = 1;
            }
        }
        if (positionDifference.x > xMinThreshold)
        {
            rotatePercentageChange = (positionDifference.x - xMinThreshold) / xMaxThreshold;
            if (rotatePercentageChange > 1)
            {
                rotatePercentageChange = 1;
            }
        }
        if (positionDifference.x < -xMinThreshold)
        {
            rotatePercentageChange = (-positionDifference.x - xMinThreshold) / xMaxThreshold;
            if (rotatePercentageChange > 1)
            {
                rotatePercentageChange = 1;
            }
        }
        if (positionDifference.y > yMinThreshold)
        {
            floatPercentageChange = (positionDifference.y - yMinThreshold) / yMaxThreshold;
            if (floatPercentageChange > 1)
            {
                floatPercentageChange = 1;
            }
        }
        if (positionDifference.y < -yMinThreshold)
        {
            floatPercentageChange = (-positionDifference.y - yMinThreshold) / yMaxThreshold;
            if (floatPercentageChange > 1)
            {
                floatPercentageChange = 1;
            }
        }
    }
}
