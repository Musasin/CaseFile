using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YukariMouseController : MonoBehaviour
{
    public Sprite awawa;
    public Sprite axtu;
    public Sprite cat;
    public Sprite drool;
    public Sprite drool_2;
    public Sprite gununu;
    public Sprite laugh;
    public Sprite mu;
    public Sprite muu;
    public Sprite o;
    public Sprite oxtu;
    public Sprite smile;
    public Sprite smile_2;
    public Sprite tongue;
    public Sprite waxtu;

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
            case "awawa":
                GetComponent<Image>().sprite = awawa;
                break;
            case "axtu":
                GetComponent<Image>().sprite = axtu;
                break;
            case "cat":
                GetComponent<Image>().sprite = cat;
                break;
            case "drool":
                GetComponent<Image>().sprite = drool;
                break;
            case "drool_2":
                GetComponent<Image>().sprite = drool_2;
                break;
            case "gununu":
                GetComponent<Image>().sprite = gununu;
                break;
            case "laugh":
                GetComponent<Image>().sprite = laugh;
                break;
            case "mu":
                GetComponent<Image>().sprite = mu;
                break;
            case "muu":
                GetComponent<Image>().sprite = muu;
                break;
            case "o":
                GetComponent<Image>().sprite = o;
                break;
            case "oxtu":
                GetComponent<Image>().sprite = oxtu;
                break;
            case "smile":
                GetComponent<Image>().sprite = smile;
                break;
            case "smile_2":
                GetComponent<Image>().sprite = smile_2;
                break;
            case "tongue":
                GetComponent<Image>().sprite = tongue;
                break;
            case "waxtu":
                GetComponent<Image>().sprite = waxtu;
                break;
            default:
                break;
        }
    }
}
