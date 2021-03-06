﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicController : MonoBehaviour
{

    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    public string folderPass;

    // Use this for initialization
    void Start()
    {
        Sprite[] files = Resources.LoadAll<Sprite>(folderPass);
        foreach (Sprite var in files)
        {
            sprites.Add(var.name, var);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSprite(string spriteName)
    {
        if (spriteName == "")
        {
            return;
        }
        if (!sprites.ContainsKey(spriteName))
        {
            Debug.LogWarning("not found sprite: " + spriteName);
            return;
        }
        GetComponent<Image>().sprite = sprites[spriteName];
    }
}