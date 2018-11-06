using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YukariMouseController : MonoBehaviour
{

    public Sprite smile;
    public Sprite laugh;

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
            case "smile":
                GetComponent<Image>().sprite = smile;
                break;
            case "laugh":
                GetComponent<Image>().sprite = laugh;
                break;
            default:
                break;
        }
    }
}
