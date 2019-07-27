using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemPanelController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private char lf = (char)10;
    GameController gameController;

    public string itemName;
    public string description;
    public string targetFlag;

    Text itemNameText;
    Text descriptionText;
    Image itemImage;
    Image itemPanelImage;

    // Use this for initialization
    void Start()
    {
        itemNameText = GameObject.Find("ItemName").GetComponent<Text>();
        descriptionText = GameObject.Find("Description").GetComponent<Text>();
        itemImage = GameObject.Find("WindowItemImage").GetComponent<Image>();
        itemPanelImage = this.transform.GetChild(0).GetComponent<Image>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        itemPanelImage.enabled = IsEnabledItem();
    }

    // Update is called once per frame
    void Update()
    {
        itemPanelImage.enabled = IsEnabledItem();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsEnabledItem())
        {
            return;
        }
        // 名前と説明文を上書き
        itemNameText.text = itemName;
        descriptionText.text = description.Replace("\\n", lf.ToString());

        // 子要素で持っている画像をアイテムウィンドウに上書き
        itemImage.sprite = transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        itemImage.transform.localScale = transform.GetChild(0).gameObject.transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsEnabledItem())
        {
            return;
        }
        if (itemName != "")
        {
            gameController.SelectChoiceItems(itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public bool IsEnabledItem()
    {
        return targetFlag == "" || gameController.FlagCheck(targetFlag);
    }
}
