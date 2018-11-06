using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YukariOptionController : MonoBehaviour
{

    public Sprite blunder;
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
            case "blunder":
                GetComponent<Image>().sprite = blunder;
                break;
            case "laugh":
                GetComponent<Image>().sprite = laugh;
                break;
            default:
                break;
        }
    }
}
