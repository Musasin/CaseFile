using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemPanelController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName;
    public string description;

    Text itemNameText;
    Text descriptionText;
    Image itemImage;

    // Use this for initialization
    void Start()
    {
        itemNameText = GameObject.Find("ItemName").GetComponent<Text>();
        descriptionText = GameObject.Find("Description").GetComponent<Text>();
        itemImage = GameObject.Find("WindowItemImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 名前と説明文を上書き
        itemNameText.text = itemName;
        descriptionText.text = description;

        // 子要素で持っている画像をアイテムウィンドウに上書き
        itemImage.sprite = transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
