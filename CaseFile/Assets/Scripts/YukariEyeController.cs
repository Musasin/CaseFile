using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YukariEyeController : MonoBehaviour
{

    public Sprite normal;
    public Sprite smile;

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
            case "normal":
                GetComponent<Image>().sprite = normal;
                break;
            case "smile":
                GetComponent<Image>().sprite = smile;
                break;
            default:
                break;
        }
    }
}
