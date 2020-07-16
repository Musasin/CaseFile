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

    Slider bgmSlider, seSlider, voiceSlider;

    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(1024, 768, Screen.fullScreen);
        StaticController.SetFullScreenOnOff(Screen.fullScreen);

        AudioManager.Instance.PlayBGM("bgm_maoudamashii_acoustic32", 0.1f, true);
        yukariObject = GameObject.Find("Yukari");

        canvasAnimator = GameObject.Find("TitleCanvas2").GetComponent<Animator>();
        stateOpeningHash = Animator.StringToHash("Base Layer.TitleAnimation2");
        stateLoopHash = Animator.StringToHash("Base Layer.TitleLoopAnimation");
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();

        if (StaticController.isCleared)
        {
            Button afterWardButton = GameObject.Find("MenuText4").GetComponent<Button>();
            afterWardButton.interactable = true;
        }
        
        bgmSlider = GameObject.Find("BGMVolumeSlider").GetComponent<Slider>();
        seSlider = GameObject.Find("SEVolumeSlider").GetComponent<Slider>();
        voiceSlider = GameObject.Find("VoiceVolumeSlider").GetComponent<Slider>();
        bgmSlider.value = StaticController.bgmVolume * 10;
        seSlider.value = StaticController.seVolume * 10;
        voiceSlider.value = StaticController.voiceVolume * 10;
        seSlider.onValueChanged.AddListener(delegate { SetSEVolume(); });
        voiceSlider.onValueChanged.AddListener(delegate { SetVoiceVolume(); });
        GameObject.Find("Option").SetActive(false);

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
                AudioManager.Instance.PlaySE("title-coal-0", 0.8f, false, true);
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

        StaticController.SetBGMVolume((float)bgmSlider.value / 10);
        AudioManager.Instance.SetBGMVolume(0.1f * StaticController.bgmVolume);
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

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
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
    public void SetFullScreenOnOff()
    {
        bool isOn = GameObject.Find("ToggleFullScreenOn").GetComponent<Toggle>().isOn;
        StaticController.SetFullScreenOnOff(isOn);
        Screen.SetResolution(1024, 768, isOn);
    }
    public void SetSEVolume()
    {
        StaticController.SetSEVolume((float)seSlider.value / 10);
        AudioManager.Instance.PlaySE("decision22", 0.5f);
    }
    public void SetVoiceVolume()
    {
        StaticController.SetVoiceVolume((float)voiceSlider.value / 10);
        switch (Random.Range(0, 3))
        {
            case 0:
                AudioManager.Instance.PlaySE("TitleVoice-0", 0.5f, false, true);
                break;
            case 1:
                AudioManager.Instance.PlaySE("TitleVoice-1", 0.5f, false, true);
                break;
            case 2:
                AudioManager.Instance.PlaySE("TitleVoice-2", 0.5f, false, true);
                break;
        }
    }
}
