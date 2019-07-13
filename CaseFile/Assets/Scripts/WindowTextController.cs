﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowTextController : MonoBehaviour
{
    Text windowText;
    string nowText = "";
    string drawText = "";
    int wordCount = 0;
    bool isHighSpeedText = false;

    // Use this for initialization
    void Start()
    {
        windowText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        wordCount++;
        if (nowText != "" && wordCount <= nowText.Length)
        {
            drawText = nowText.Substring(0, wordCount);
            windowText.text = drawText;
        }
    }

    public void UpdateText(string text, bool isAllDraw)
    {
        if (isAllDraw || isHighSpeedText)
        {
            wordCount = text.Length;
            windowText.text = text;
        }
        else
        {
            wordCount = 0;
        }

        nowText = text;
    }

    public bool IsDrawn()
    {
        return wordCount >= nowText.Length;
    }

    public void AllDrawText()
    {
        wordCount = nowText.Length;
        windowText.text = nowText;
    }

    public void SetHighSpeedText(bool isHighSpeed)
    {
        isHighSpeedText = isHighSpeed;
    }
}
