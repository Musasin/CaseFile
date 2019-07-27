using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ToggleExt
{
    public static void SetIsOnWithoutCallback(this Toggle self, bool isOn)
    {
        var onValueChanged = self.onValueChanged;
        self.onValueChanged = new Toggle.ToggleEvent();
        self.isOn = isOn;
        self.onValueChanged = onValueChanged;
    }
}

public class OptionController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject.Find("ToggleNormalSpeed").GetComponent<Toggle>().SetIsOnWithoutCallback(!StaticController.isHighSpeed);
        GameObject.Find("ToggleHighSpeed").GetComponent<Toggle>().SetIsOnWithoutCallback(StaticController.isHighSpeed);
        GameObject.Find("ToggleBGMOn").GetComponent<Toggle>().SetIsOnWithoutCallback(StaticController.isBGMOn);
        GameObject.Find("ToggleBGMOff").GetComponent<Toggle>().SetIsOnWithoutCallback(!StaticController.isBGMOn);
        GameObject.Find("ToggleSEOn").GetComponent<Toggle>().SetIsOnWithoutCallback(StaticController.isSEOn);
        GameObject.Find("ToggleSEOff").GetComponent<Toggle>().SetIsOnWithoutCallback(!StaticController.isSEOn);
        GameObject.Find("ToggleVoiceOn").GetComponent<Toggle>().SetIsOnWithoutCallback(StaticController.isVoiceOn);
        GameObject.Find("ToggleVoiceOff").GetComponent<Toggle>().SetIsOnWithoutCallback(!StaticController.isVoiceOn);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(StaticController.isBGMOn);
    }
}
