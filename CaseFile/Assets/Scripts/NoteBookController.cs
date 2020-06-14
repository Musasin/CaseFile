using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBookController : MonoBehaviour
{
    GameController gameController;
    GameObject noteBookOverallObject, closeButton, upButton, downButton, explanationPanel;
    float fisrtPosY;

    // 下段ページフラグ
    bool isLowerPage;

    bool isClose;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        noteBookOverallObject = GameObject.Find("NoteBookOverall");
        closeButton = GameObject.Find("Close");
        upButton = GameObject.Find("UpButton");
        downButton = GameObject.Find("DownButton");
        explanationPanel = GameObject.Find("ExplanationPanel");
        fisrtPosY = noteBookOverallObject.transform.localPosition.y;
        upButton.SetActive(false);
        downButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float step = Time.deltaTime * 2000;

        if (isClose)
        {
            noteBookOverallObject.transform.localPosition = Vector2.MoveTowards(noteBookOverallObject.transform.localPosition, new Vector2(0, fisrtPosY), step);

            float dis = Mathf.Abs(Mathf.Abs(noteBookOverallObject.transform.localPosition.y) - Mathf.Abs(fisrtPosY));
            if (dis < 0.1f)
            {
                gameController.CloseNoteBook();
            }
        }
        else
        {
            explanationPanel.SetActive(StaticController.isWritingMemo);
            if (isLowerPage)
                noteBookOverallObject.transform.localPosition = Vector2.MoveTowards(noteBookOverallObject.transform.localPosition, new Vector2(0, 620), step);
            else
                noteBookOverallObject.transform.localPosition = Vector2.MoveTowards(noteBookOverallObject.transform.localPosition, new Vector2(0, 0), step);
        }
    }

    public void CloseNoteBook()
    {
        isClose = true;
        closeButton.SetActive(false);
        upButton.SetActive(false);
        downButton.SetActive(false);
        if (StaticController.isSEOn)
        {
            AudioManager.Instance.PlaySE("page1");
        }
    }

    public void ResetScale()
    {
        isClose = false;
        isLowerPage = false;
        closeButton.SetActive(true);
        upButton.SetActive(false);
        downButton.SetActive(true);
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
