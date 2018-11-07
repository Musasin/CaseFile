using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YukariOptionController : MonoBehaviour
{
    public Sprite nothing;
    public Sprite exclamation;
    public Sprite ex_question;
    public Sprite question;
    public Sprite blunder;
    public Sprite blusher;
    public Sprite blusher_2;
    public Sprite breath;
    public Sprite bright_red;
    public Sprite bubble;
    public Sprite buruburu;
    public Sprite chill;
    public Sprite close_eyes_tears;
    public Sprite dark;
    public Sprite dark_2;
    public Sprite flash;
    public Sprite flash_2;
    public Sprite kega;
    public Sprite knife;
    public Sprite knife_blood;
    public Sprite laugh;
    public Sprite megane;
    public Sprite muka;
    public Sprite open_eye_tears;
    public Sprite oxtu;
    public Sprite star;
    public Sprite star_2;
    public Sprite sweat;
    public Sprite sweat_2;
    public Sprite sweat_3;
    public Sprite sweat_4;
    public Sprite zokuzoku;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetSprite(string spriteName)
    {
        switch (spriteName)
        {
            case "exclamation":
                GetComponent<Image>().sprite = exclamation;
                break;
            case "ex_question":
                GetComponent<Image>().sprite = ex_question;
                break;
            case "question":
                GetComponent<Image>().sprite = question;
                break;
            case "blunder":
                GetComponent<Image>().sprite = blunder;
                break;
            case "blusher":
                GetComponent<Image>().sprite = blusher;
                break;
            case "blusher_2":
                GetComponent<Image>().sprite = blusher_2;
                break;
            case "breath":
                GetComponent<Image>().sprite = breath;
                break;
            case "bright_red":
                GetComponent<Image>().sprite = bright_red;
                break;
            case "bubble":
                GetComponent<Image>().sprite = bubble;
                break;
            case "buruburu":
                GetComponent<Image>().sprite = buruburu;
                break;
            case "chill":
                GetComponent<Image>().sprite = chill;
                break;
            case "close_eyes_tears":
                GetComponent<Image>().sprite = close_eyes_tears;
                break;
            case "dark":
                GetComponent<Image>().sprite = dark;
                break;
            case "dark_2":
                GetComponent<Image>().sprite = dark_2;
                break;
            case "flash":
                GetComponent<Image>().sprite = flash;
                break;
            case "flash_2":
                GetComponent<Image>().sprite = flash_2;
                break;
            case "kega":
                GetComponent<Image>().sprite = kega;
                break;
            case "knife":
                GetComponent<Image>().sprite = knife;
                break;
            case "knife_blood":
                GetComponent<Image>().sprite = knife_blood;
                break;
            case "laugh":
                GetComponent<Image>().sprite = laugh;
                break;
            case "megane":
                GetComponent<Image>().sprite = megane;
                break;
            case "muka":
                GetComponent<Image>().sprite = muka;
                break;
            case "open_eye_tears":
                GetComponent<Image>().sprite = open_eye_tears;
                break;
            case "oxtu":
                GetComponent<Image>().sprite = oxtu;
                break;
            case "star":
                GetComponent<Image>().sprite = star;
                break;
            case "star_2":
                GetComponent<Image>().sprite = star_2;
                break;
            case "sweat":
                GetComponent<Image>().sprite = sweat;
                break;
            case "sweat_2":
                GetComponent<Image>().sprite = sweat_2;
                break;
            case "sweat_3":
                GetComponent<Image>().sprite = sweat_3;
                break;
            case "sweat_4":
                GetComponent<Image>().sprite = sweat_4;
                break;
            case "zokuzoku":
                GetComponent<Image>().sprite = zokuzoku;
                break;
            default:
                GetComponent<Image>().sprite = nothing;
                break;
        }
    }
}
