using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YukariEyeController : MonoBehaviour
{
    public Sprite close;
    public Sprite close_2;
    public Sprite cross;
    public Sprite cross_tears;
    public Sprite emptiness;
    public Sprite guruguru;
    public Sprite half_open;
    public Sprite normal;
    public Sprite sanpakugan;
    public Sprite sanpakugan_tears;
    public Sprite sickness;
    public Sprite smile;
    public Sprite sprise_white;
    public Sprite suprise_white_tears;
    public Sprite surprise;
    public Sprite white_eyes;
    public Sprite white_eyes_tears;

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
            case "close":
                GetComponent<Image>().sprite = close;
                break;
            case "close_2":
                GetComponent<Image>().sprite = close_2;
                break;
            case "cross":
                GetComponent<Image>().sprite = cross;
                break;
            case "cross_tears":
                GetComponent<Image>().sprite = cross_tears;
                break;
            case "emptiness":
                GetComponent<Image>().sprite = emptiness;
                break;
            case "guruguru":
                GetComponent<Image>().sprite = guruguru;
                break;
            case "half_open":
                GetComponent<Image>().sprite = half_open;
                break;
            case "normal":
                GetComponent<Image>().sprite = normal;
                break;
            case "sanpakugan":
                GetComponent<Image>().sprite = sanpakugan;
                break;
            case "sanpakugan_tears":
                GetComponent<Image>().sprite = sanpakugan_tears;
                break;
            case "sickness":
                GetComponent<Image>().sprite = sickness;
                break;
            case "smile":
                GetComponent<Image>().sprite = smile;
                break;
            case "sprise_white":
                GetComponent<Image>().sprite = sprise_white;
                break;
            case "suprise_white_tears":
                GetComponent<Image>().sprite = suprise_white_tears;
                break;
            case "surprise":
                GetComponent<Image>().sprite = surprise;
                break;
            case "white_eyes":
                GetComponent<Image>().sprite = white_eyes;
                break;
            case "white_eyes_tears":
                GetComponent<Image>().sprite = white_eyes_tears;
                break;
        }
    }
}
