﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticController : MonoBehaviour
{
    public static bool isHighSpeed = false;
    public static bool isVoiceOn = true;
    public static bool isSEOn = true;
    public static bool isBGMOn = true;
    public static string nowBGM = "";
    public static bool isCleared = false;
    public static bool isFullScreenOn = false;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public static void SetHighSpeedText(bool isHigh)
    {
        isHighSpeed = isHigh;
    }
    public static void SetVoiceOnOff(bool isOn)
    {
        isVoiceOn = isOn;
    }
    public static void SetSEOnOff(bool isOn)
    {
        isSEOn = isOn;
    }
    public static void SetBGMOnOff(bool isOn)
    {
        isBGMOn = isOn;
    }
    public static void SetFullScreenOnOff(bool isOn)
    {
        isFullScreenOn = isOn;
    }
    public static void SetClear(bool isClear)
    {
        isCleared = isClear;
    }

}
