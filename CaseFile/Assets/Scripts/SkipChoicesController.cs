using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipChoicesController : MonoBehaviour
{
    GameController gameController;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectChoices(int choiceNumber)
    {
        Debug.Log(choiceNumber);
        if (choiceNumber == 1)
        {
            gameController.SkipScenario();
            GameObject.Find("SkipDialog").SetActive(false);
        }
        else
        {
            GameObject.Find("SkipDialog").SetActive(false);
        }
    }
}
