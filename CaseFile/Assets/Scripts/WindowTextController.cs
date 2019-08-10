using System.Collections;
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
    GameObject pointerCircleObject;
    float drawnTime = 0;

    // Use this for initialization
    void Start()
    {
        isHighSpeedText = StaticController.isHighSpeed;
        windowText = this.GetComponent<Text>();
        pointerCircleObject = GameObject.Find("PointerCircle");
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
        if (IsDrawnAllText())
        {
            drawnTime += Time.deltaTime;
            pointerCircleObject.SetActive(!isHighSpeedText);
        }
        else
        {
            drawnTime = 0;
            pointerCircleObject.SetActive(false);
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
            windowText.text = "";
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

    public void SetHighSpeedText()
    {
        bool isHigh = GameObject.Find("ToggleHighSpeed").GetComponent<Toggle>().isOn;
        StaticController.SetHighSpeedText(isHigh);
        isHighSpeedText = isHigh;
    }

    public bool IsDrawnAllText()
    {
        return (wordCount > nowText.Length);
    }

    public bool IsEnoughDrawnTime()
    {
        // 全部描画し終わったあとに、文字数/20 + 1秒待つ
        return IsDrawnAllText() && (drawnTime >= (float)drawText.Length / 20 + 1.0f);
    }

    public void SetFontSize(int fontSize)
    {
        windowText.fontSize = fontSize;
    }
}
