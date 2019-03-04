using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListController : MonoBehaviour
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

    public void CloseItemList()
    {
        gameController.CloseItemList();
    }
}
