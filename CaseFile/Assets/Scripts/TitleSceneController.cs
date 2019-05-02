using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        AudioManager.Instance.PlayBGM("bgm_maoudamashii_acoustic32", 0.1f, true);
        yukariObject = GameObject.Find("Yukari");

        canvasAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        stateOpeningHash = Animator.StringToHash("Base Layer.TitleAnimation");
        stateLoopHash = Animator.StringToHash("Base Layer.TitleLoopAnimation");
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();

        state = State.Init;
    }

    // Update is called once per frame
    void Update()
    {
        int animationHash = canvasAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;

        if (yukariObject.transform.position.x > 80 && state == State.FadeIn)
        {
            AudioManager.Instance.PlaySE("title-coal-3", 1.0f);
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
        AudioManager.Instance.FadeOutBGM();
        fadePanelController.FadeOut("MainScene");
    }
}
