using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YukariEyeBrowsController : MonoBehaviour
{

    public Sprite normal;
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
            default:
                break;
        }
    }
}
