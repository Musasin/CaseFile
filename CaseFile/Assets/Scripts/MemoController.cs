using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MemoController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
    GameController gameController;
	public GameObject mouseOverPanel;
	GameObject mouseOverPanelObject;

    WindowTextController windowTextController;
	string memoText;
	int memoId;

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        windowTextController = GameObject.Find("Window").GetComponentInChildren<WindowTextController>();
		memoText = "";
		memoId = 0;
		GetComponentInChildren<Text>().text = memoText;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PushMemo()
    {
		if (StaticController.isWritingMemo)
		{
			memoText = windowTextController.GetNowText();
			GetComponentInChildren<Text>().text = memoText.Replace("\r", "").Replace("\n", "");
			memoText = GameObject.Find("NameText").GetComponent<Text>().text + "\n" + memoText;
			memoId = gameController.GetMemoId();
			StaticController.SetWritingMemo(false);
		}
		else if (memoId != 0)
        {
            gameController.SelectChoiceMemos(memoId);
        }
    }

	public void OnPointerEnter(PointerEventData eventData)
    {
		if (memoText == "")
			return;

		mouseOverPanelObject = Instantiate(mouseOverPanel);
		mouseOverPanelObject.name = "MouseOverPanel";
		mouseOverPanelObject.transform.SetParent(transform.parent);
		mouseOverPanelObject.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 200);
		float correctionValueY = (transform.position.y < 300) ? 125 : -125;
		mouseOverPanelObject.transform.position = new Vector2(520, transform.position.y + correctionValueY);
		mouseOverPanelObject.GetComponentInChildren<Text>().text = memoText;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
		if (mouseOverPanelObject != null)
			Destroy(mouseOverPanelObject);
    }
    public void OnDisable()
    {
		if (mouseOverPanelObject != null)
			Destroy(mouseOverPanelObject);
    }
}
