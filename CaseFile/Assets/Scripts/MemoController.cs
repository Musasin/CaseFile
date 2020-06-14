using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MemoController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject mouseOverPanel;
	GameObject mouseOverPanelObject;

    WindowTextController windowTextController;
	string memoText;

	// Use this for initialization
	void Start () {
        windowTextController = GameObject.Find("Window").GetComponentInChildren<WindowTextController>();
		memoText = "";
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
			GetComponentInChildren<Text>().text = memoText;
        }
    }

	public void OnPointerEnter(PointerEventData eventData)
    {
		mouseOverPanelObject = Instantiate(mouseOverPanel);
		mouseOverPanelObject.transform.SetParent(transform.parent);
		mouseOverPanelObject.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 200);
		float correctionValueY = (transform.position.y < 300) ? 125 : -125;
		mouseOverPanelObject.transform.position = new Vector2(520, transform.position.y + correctionValueY);
		mouseOverPanelObject.GetComponentInChildren<Text>().text = memoText;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(mouseOverPanelObject);
    }
}
