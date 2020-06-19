using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardInputController : MonoBehaviour
{
    GameController gameController;
    Text inputText;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        inputText = GameObject.Find("InputText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressConfirmButton()
    {
        gameController.ConfirmKeyBoardInput(inputText.text);
    }
}
