using System.Linq;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Logger : Singleton<Logger>
{
    [SerializeField]
    private TextMeshProUGUI debugAreaText = null;

    [SerializeField]
    private int maxLines = 15;

    private bool enableDebug = true;

    void Awake()
    {
        if (debugAreaText == null)
        {
            debugAreaText = GetComponent<TextMeshProUGUI>();
        }
        debugAreaText.text = string.Empty;
    }

    void OnEnable()
    {
        debugAreaText.enabled = enableDebug;

        if (enableDebug)
        {
            debugAreaText.text += $"<color=\"white\">{DateTime.Now.ToString("HH:mm:ss")} {this.GetType().Name} enabled</color>\n";
        }
    }

    public void Clear() => debugAreaText.text = string.Empty; //method to Clear Lines when maxLines is achieved.

    public void LogInfo(string message)
    {
        ClearLines();
        debugAreaText.text += $"<color=\"green\">{DateTime.Now.ToString("HH:mm:ss")} {message}</color>\n";
    }

    public void LogError(string message)
    {
        ClearLines();
        debugAreaText.text += $"<color=\"red\">{DateTime.Now.ToString("HH:mm:ss")} {message}</color>\n";
    }

    public void LogWarning(string message)
    {
        ClearLines();
        debugAreaText.text += $"<color=\"yellow\">{DateTime.Now.ToString("HH:mm:ss")} {message}</color>\n";
    }

    private void ClearLines()
    {
        if (debugAreaText.text.Split('\n').Count() >= maxLines)
        {
            debugAreaText.text = string.Empty;
        }
    }

    
    public void AddLoggerText(string message)
    {
        //debugAreaText.text += message;
        debugAreaText.text += $"<color=\"yellow\">{DateTime.Now.ToString("HH:mm:ss")} {message}</color>\n";

    }

    private void OnDisable()
    {
        //Debug.Log("OnDisable debug");
        debugAreaText.enabled = !enableDebug;
        debugAreaText.text = null;
    }
}