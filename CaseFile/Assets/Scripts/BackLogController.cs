using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLogController : MonoBehaviour
{
    GameController gameController;
    float defaultPositionY;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        defaultPositionY = this.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = this.gameObject.transform.position;
        pos.y -= scroll * 100;
        if (pos.y > defaultPositionY)
        {
            pos.y = defaultPositionY;
            gameController.CloseBackLog();
        }
        this.gameObject.transform.position = pos;
    }

    public void ResetPosition()
    {
        Vector3 pos = this.gameObject.transform.position;
        pos.y = defaultPositionY;
        this.gameObject.transform.position = pos;
    }
}
