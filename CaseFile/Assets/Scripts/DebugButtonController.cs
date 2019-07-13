using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugButtonController : MonoBehaviour
{
    bool isOn;
    float firstPosX;
    Text openCloseText;

    // Use this for initialization
    void Start()
    {
        isOn = false;
        firstPosX = this.transform.position.x;
        openCloseText = GameObject.Find("DebugOpenText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float newPosX = (isOn ? firstPosX - 150 : firstPosX);
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(newPosX, this.transform.position.y), 20);
    }

    public void ChangeOnOff()
    {
        this.isOn = !this.isOn;
        if (this.isOn)
        {
            openCloseText.text = "▶︎";
        }
        else
        {
            openCloseText.text = "◀︎";
        }

    }
}
