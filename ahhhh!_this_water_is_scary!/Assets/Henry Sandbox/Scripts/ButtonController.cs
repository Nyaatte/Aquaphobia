using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;
    public float rotateSpeed;
    public float floatSpeed;
    Vector3 EulerAngleLeftVelocity;
    Vector3 EulerAngleRightVelocity;

    // Start is called before the first frame update
    void Start()
    {
       // MyEventSystem.current.onButtonPressedDown += OnSubMovement;
        EulerAngleLeftVelocity = new Vector3(0, -rotateSpeed, 0);
        EulerAngleRightVelocity = new Vector3(0, rotateSpeed, 0);
    }

    private void OnSubMovement(string id)
    {
        if (id == "Left") //left
        {
            Quaternion deltaLeftRotation = Quaternion.Euler(
                EulerAngleLeftVelocity * Time.fixedDeltaTime
            );
            rb.MoveRotation(rb.rotation * deltaLeftRotation);
            rb.AddForce(transform.right * moveSpeed);
        }
        if (id == "Right") //right
        {
            Quaternion deltaRightRotation = Quaternion.Euler(
                EulerAngleRightVelocity * Time.fixedDeltaTime
            );
            rb.MoveRotation(rb.rotation * deltaRightRotation);
            rb.AddForce(-transform.right * moveSpeed);
        }
        if (id == "Forward") //forward
        {
            rb.AddForce((transform.forward * moveSpeed));
        }
        if (id == "Backward") //backward
        {
            rb.AddForce((-transform.forward * moveSpeed));
        }
        if (id == "Up") //up
        {
            rb.AddForce((transform.up * floatSpeed));
        }
        if (id == "Down") //down
        {
            rb.AddForce((-transform.up * floatSpeed));
        }
    }

    // private void OnDestroy(){
    //     MyEventSystem.current.onButtonPressedDown -= OnSubMovement;
    // }
}
