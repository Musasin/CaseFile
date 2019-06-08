using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButtonController : MonoBehaviour
{
    bool isOn;
    float firstPosX;

    // Use this for initialization
    void Start()
    {
        isOn = false;
        firstPosX = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosX = (isOn ? firstPosX - 150 : firstPosX);
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(newPosX, this.transform.position.y), 20);
    }

    public void ChangeOnOff(bool isOn)
    {
        this.isOn = isOn;
    }
}
