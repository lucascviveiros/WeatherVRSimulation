using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemVRWebManager : Singleton<SystemVRWebManager>
{
    //[SerializeField] private Text outputSystemText;
    private string systemType;

    void Awake()
    {
        systemType = SystemInfo.deviceName.ToString();
        //Debug.Log("device: " + systemType);

        //if (ReturnDevice())
          //  outputSystemText = GameObject.Find("CanvasLauncher/Demo Intro UI/Resume").GetComponent<Text>();
        //else
            //outputSystemText = GameObject.Find("CanvasLauncher/Demo Intro UI/Resume").GetComponent<Text>();

        //outputSystemText.text = systemType;
        DontDestroyOnLoad(this.gameObject);
    }

    public string GetSystemDevice()
    {
        return systemType;
    }

	public bool ReturnDevice()
	{
		if (SystemVRWebManager.instance.GetSystemDevice() == "Oculus Quest" || SystemVRWebManager.instance.GetSystemDevice() == "Oculus Quest 2")
		{
			return true;
		}
		else 
		{
			return false;
		}

		return false;
	}    
}
