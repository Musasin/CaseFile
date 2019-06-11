using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceNameController : MonoBehaviour
{

    Text placeNameText;
    Text dayText;
    Animator placeNameAnimator;

    // Use this for initialization
    void Start()
    {
        placeNameText = GameObject.Find("PlaceNameText").GetComponent<Text>();
        dayText = GameObject.Find("dayText").GetComponent<Text>();
        placeNameAnimator = GameObject.Find("PlaceName").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangePlaceName(string words)
    {
        if (words == "")
        {
            placeNameAnimator.SetInteger("placePanelStatus", 2);
        }
        else
        {
            string[] placeNameAndDay = words.Split('/');
            placeNameAnimator.SetInteger("placePanelStatus", 1);
            placeNameText.text = placeNameAndDay[0];
            if (placeNameAndDay.Length >= 2)
            {
                dayText.text = placeNameAndDay[1];
            }
        }
    }
}
