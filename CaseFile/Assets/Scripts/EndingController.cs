using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeOutBGM()
    {
        AudioManager.Instance.FadeOutBGM();
    }

    public void FadeInEndingBGM()
    {
        if (StaticController.isBGMOn)
        {
            AudioManager.Instance.FadeInBGM("bgm_maoudamashii_healing03", 0.1f, false);
        }
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
