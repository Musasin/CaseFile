using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBookController : MonoBehaviour
{
    GameController gameController;
    GameObject noteBookOverallObject;
    float time;

    // Use this for initialization
    void Start()
    {
        time = 0;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        noteBookOverallObject = GameObject.Find("NoteBookOverall");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 3;
        float[] scales = new float[2] { time, 1.0f };
        float scale = Mathf.Min(scales);
        noteBookOverallObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void CloseNoteBook()
    {
        gameController.CloseNoteBook();
    }

    public void ResetScale()
    {
        time = 0;
        noteBookOverallObject.transform.localScale = new Vector3(0, 0, 0);
    }
}
