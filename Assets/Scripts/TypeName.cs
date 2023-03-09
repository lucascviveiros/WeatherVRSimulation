using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TypeName : MonoBehaviour
{
    public Text nameText;
    private string user_name = null;

    private void Awake() {
        //ReceiveVRKey("OI");
        //Debug.Log("Call Receive");
    }

    public void ReceiveVRKey(string key)
    {
        user_name += key;
        nameText.text = user_name;

    }

    public void Delete()
    {
        user_name = user_name.Remove(user_name.Length - 1);
        nameText.text = user_name;
    }

    public void Space()
    {
        user_name += " ";
        nameText.text = user_name;
    }
}
