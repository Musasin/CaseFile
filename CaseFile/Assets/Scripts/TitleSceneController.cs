using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
    GameObject yukariObject;
    Animator canvasAnimator;
    FadePanelController fadePanelController;
    public enum State { Init = 1, FadeIn = 2, TitleCoal = 3, Idle = 4, FadeOut = 5 };
    State state;
    int stateOpeningHash, stateLoopHash;

    // Use this for initialization
    void Start()
    {
        if (StaticController.isBGMOn)
        {
            AudioManager.Instance.PlayBGM("bgm_maoudamashii_acoustic32", 0.1f, true);
        }
        yukariObject = GameObject.Find("Yukari");

        canvasAnimator = GameObject.Find("TitleCanvas").GetComponent<Animator>();
        stateOpeningHash = Animator.StringToHash("Base Layer.TitleAnimation");
        stateLoopHash = Animator.StringToHash("Base Layer.TitleLoopAnimation");
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();

        if (StaticController.isCleared)
        {
            Button afterWardButton = GameObject.Find("MenuText4").GetComponent<Button>();
            afterWardButton.interactable = true;
        }


        state = State.Init;

    }

    // Update is called once per frame
    void Update()
    {
        int animationHash = canvasAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;

        if (yukariObject.transform.position.x > 80 && state == State.FadeIn)
        {
            if (StaticController.isVoiceOn)
            {
                AudioManager.Instance.PlaySE("title-coal-4", 1.0f);
            }
            state = State.TitleCoal;
        }

        if (animationHash == stateOpeningHash && state == State.Init)
        {
            state = State.FadeIn;
        }

        if (animationHash == stateLoopHash && state == State.FadeIn)
        {
            state = State.Idle;
        }
    }

    public void PlayGame()
    {
        if (StaticController.isSEOn)
        {
            AudioManager.Instance.PlaySE("decision27", 0.5f);
        }
        AudioManager.Instance.FadeOutBGM();
        fadePanelController.FadeOut("MainScene");
    }

    public void OpenOption()
    {
        if (StaticController.isSEOn)
        {
            AudioManager.Instance.PlaySE("decision22", 0.5f);
        }
    }

    public void CloseOption()
    {
        if (StaticController.isSEOn)
        {
            AudioManager.Instance.PlaySE("decision22", 0.5f);
        }
    }

    public void OpenCredit()
    {

        if (StaticController.isSEOn)
        {
            AudioManager.Instance.PlaySE("decision22", 0.5f);
        }
    }
    public void CloseCredit()
    {
        if (StaticController.isSEOn)
        {
            AudioManager.Instance.PlaySE("decision22", 0.5f);
        }
    }
    public void OpenAfterword()
    {
        if (StaticController.isSEOn)
        {
            AudioManager.Instance.PlaySE("decision22", 0.5f);
        }

    }

    public void CloseAfterword()
    {

        if (StaticController.isSEOn)
        {
            AudioManager.Instance.PlaySE("decision22", 0.5f);
        }
    }

    public void SetHighSpeedText()
    {
        bool isHigh = GameObject.Find("ToggleHighSpeed").GetComponent<Toggle>().isOn;
        StaticController.SetHighSpeedText(isHigh);
    }
    public void SetVoiceOnOff()
    {
        bool isOn = GameObject.Find("ToggleVoiceOn").GetComponent<Toggle>().isOn;
        StaticController.SetVoiceOnOff(isOn);
    }
    public void SetSEOnOff()
    {
        bool isOn = GameObject.Find("ToggleSEOn").GetComponent<Toggle>().isOn;
        StaticController.SetSEOnOff(isOn);
    }
    public void SetBGMOnOff()
    {
        bool isOn = GameObject.Find("ToggleBGMOn").GetComponent<Toggle>().isOn;
        Debug.Log("title" + isOn);
        StaticController.SetBGMOnOff(isOn);
        if (isOn)
        {
            AudioManager.Instance.PlayBGM("bgm_maoudamashii_acoustic32", 0.1f, true);
        }
        else
        {
            AudioManager.Instance.StopBGM();
        }
    }
}
