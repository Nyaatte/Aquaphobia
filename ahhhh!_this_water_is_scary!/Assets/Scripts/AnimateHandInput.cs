using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty inputPinch;
    [SerializeField] private InputActionProperty inputGrip;
    [SerializeField] private Animator hand;

    private float pinch;
    private int pinchHash = Animator.StringToHash("Trigger");
    private int gripHash = Animator.StringToHash("Grip");
    
    void Start()
    {
        
    }

    void Awake()
    {
        inputPinch.action.performed += ctx => SetPinch(ctx.ReadValue<float>());
        inputGrip.action.performed += ctx => SetGrip(ctx.ReadValue<float>());

    }

    
    void Update()
    {
        
    }

    private void SetPinch(float _p)
    {
        hand.SetFloat(pinchHash, _p);
    }

    private void SetGrip(float _g)
    {
        hand.SetFloat(gripHash, _g);
    }
}
