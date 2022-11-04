using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Component to trigger Unity events on input actions.
/// </summary>
/// 
[AddComponentMenu("SentienceLab/Interaction/Action Controller")]
public class ActionController_InputSystem : MonoBehaviour 
{
	public InputActionProperty action;

	[Header("One-Shot Behaviour")]
	public UnityEvent ActionPerformed;

	[Header("Toggle Behaviour")]
	public bool       ToggleState = false; 
	public UnityEvent ToggleOn;
	public UnityEvent ToggleOff;


	public void Start()
	{
		if (action != null) 
		{ 
			action.action.Enable();
			action.action.performed += OnActionPerformed;
		}
	}

	private void OnActionPerformed(InputAction.CallbackContext obj)
	{
		PerformAction();
		PerformToggle();		
	}


	public void PerformAction()
	{
		ActionPerformed.Invoke();
	}


	public void PerformToggle()
	{
		SetToggleState(!ToggleState);
	}


	public void SetToggleState(bool _newState)
	{
		ToggleState = _newState;

		if (ToggleState) ToggleOn.Invoke();
		else             ToggleOff.Invoke();
	}
}