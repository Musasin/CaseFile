using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBookController : MonoBehaviour
{
    GameController gameController;
    GameObject noteBookOverallObject, upButton, downButton;
    float fisrtPosY;

    // 下段ページフラグ
    bool isLowerPage;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        noteBookOverallObject = GameObject.Find("NoteBookOverall");
        upButton = GameObject.Find("UpButton");
        downButton = GameObject.Find("DownButton");
        fisrtPosY = noteBookOverallObject.transform.localPosition.y;
        upButton.SetActive(false);
        downButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float step = Time.deltaTime * 2000;

        if (isLowerPage)
            noteBookOverallObject.transform.localPosition = Vector2.MoveTowards(noteBookOverallObject.transform.localPosition, new Vector2(0, 620), step);
        else
            noteBookOverallObject.transform.localPosition = Vector2.MoveTowards(noteBookOverallObject.transform.localPosition, new Vector2(0, 0), step);
    }

    public void CloseNoteBook()
    {
        gameController.CloseNoteBook();
    }

    public void ResetScale()
    {
        isLowerPage = false;
        noteBookOverallObject.transform.localPosition = new Vector2(0, fisrtPosY);
    }
    
    public void UpPage()
    {
        isLowerPage = false;
        upButton.SetActive(false);
        downButton.SetActive(true);
    }
    public void DownPage()
    {
        isLowerPage = true;
        upButton.SetActive(true);
        downButton.SetActive(false);
    }
}
