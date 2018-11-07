using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YukariEyeBrowsController : MonoBehaviour
{
    public Sprite anger;
    public Sprite doya;
    public Sprite normal;
    public Sprite sad;

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
            case "anger":
                GetComponent<Image>().sprite = anger;
                break;
            case "doya":
                GetComponent<Image>().sprite = doya;
                break;
            case "normal":
                GetComponent<Image>().sprite = normal;
                break;
            case "sad":
                GetComponent<Image>().sprite = sad;
                break;
            default:
                break;
        }
    }
}
