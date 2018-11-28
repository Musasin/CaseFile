using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesController : MonoBehaviour
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

    public void SelectChoices1()
    {
        gameController.SelectChoices1();
        this.gameObject.SetActive(false);
    }
    public void SelectChoices2()
    {
        gameController.SelectChoices2();
        this.gameObject.SetActive(false);
    }

}
