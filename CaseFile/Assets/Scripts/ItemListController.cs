using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListController : MonoBehaviour
{
    GameController gameController;
    GameObject itemListOverallObject;
    float time;

    // Use this for initialization
    void Start()
    {
        time = 0;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        itemListOverallObject = GameObject.Find("ItemListOverall");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 5;
        float[] scales = new float[2] { time, 1.0f };
        float scale = Mathf.Min(scales);
        itemListOverallObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void CloseItemList()
    {
        gameController.CloseItemList();
    }

    public void ResetScale()
    {
        time = 0;
        itemListOverallObject.transform.localScale = new Vector3(0, 0, 0);
    }
}
