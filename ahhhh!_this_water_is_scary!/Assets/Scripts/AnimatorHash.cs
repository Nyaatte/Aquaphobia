using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AnimatorHash : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] string parameterName;

    public int hashID;
    public void GetHash()
    {
        hashID = Animator.StringToHash(parameterName);

    }

    
}
