using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessage : MonoBehaviour
{
    [SerializeField] private string command;

    public void SendCommand()
    {
        this.gameObject.SendMessage(command);
    }
    

}
