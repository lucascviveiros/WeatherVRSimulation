using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneClick : MonoBehaviour
{
    Button button;
    void Awake()
    {
        button = GameObject.Find("CanvasLauncher/UI controls/Connect Button").GetComponent<Button>();
    }

    public void OneClickButton()
    {
        button.interactable = false;
    }
}
