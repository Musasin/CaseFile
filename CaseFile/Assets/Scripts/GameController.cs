using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    YukariEyeController eyeController;
    YukariMouseController mouseController;
    YukariEyeBrowsController eyeBrowsController;
    YukariOptionController optionController1;
    YukariOptionController optionController2;
    YukariOptionController optionController3;

    private struct ScenarioData
    {
        public int id;
        public int type;
        public string character;
        public string eye;
        public string eye_brows;
        public string mouse;
        public string option1;
        public string option2;
        public string option3;
        public string voice;
        public string words;
        public string choise1;
        public string choise2;
        public string target_flag;
        public int jump_id1;
        public int jump_id2;

        public ScenarioData(string id, string type, string character, string eye, string eye_brows, string mouse, string option1, string option2, string option3, string voice, string words, string choise1, string choise2, string target_flag, string jump_id1, string jump_id2)
        {
            this.id = int.Parse(id);
            this.type = int.Parse(type);
            this.character = character;
            this.eye = eye;
            this.eye_brows = eye_brows;
            this.mouse = mouse;
            this.option1 = option1;
            this.option2 = option2;
            this.option3 = option3;
            this.voice = voice;
            this.words = words;
            this.choise1 = choise1;
            this.choise2 = choise2;
            this.target_flag = target_flag;
            this.jump_id1 = (jump_id1 == "" ? 0 : int.Parse(jump_id1));
            this.jump_id2 = (jump_id2 == "" ? 0 : int.Parse(jump_id2));
        }
    }

    private TextAsset csvFile;
    private Dictionary<int, ScenarioData> csvDatas = new Dictionary<int, ScenarioData>();
    int nowId = 1001001;

    void Start()
    {
        eyeController = GameObject.Find("Eye").GetComponent<YukariEyeController>();
        mouseController = GameObject.Find("Mouse").GetComponent<YukariMouseController>();
        eyeBrowsController = GameObject.Find("EyeBrows").GetComponent<YukariEyeBrowsController>();
        optionController1 = GameObject.Find("Option1").GetComponent<YukariOptionController>();
        optionController2 = GameObject.Find("Option2").GetComponent<YukariOptionController>();
        optionController3 = GameObject.Find("Option3").GetComponent<YukariOptionController>();

        csvFile = Resources.Load("scenario") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        int i = 0;
        while (reader.Peek() > -1)
        {
            i++;
            string line = reader.ReadLine();
            string[] datas = line.Split(',');
            if (i == 1 || datas.Length < 16 || datas[0] == "")
            {
                continue;
            }
            int id = int.Parse(datas[0]);
            var scenarioData = new ScenarioData(datas[0], datas[1], datas[2], datas[3], datas[4], datas[5], datas[6], datas[7], datas[8], datas[9], datas[10], datas[11], datas[12], datas[13], datas[14], datas[15]);
            csvDatas.Add(id, scenarioData);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(csvDatas[nowId].id);

            eyeController.SetSprite(csvDatas[nowId].eye);
            mouseController.SetSprite(csvDatas[nowId].mouse);
            eyeBrowsController.SetSprite(csvDatas[nowId].eye_brows);
            optionController1.SetSprite(csvDatas[nowId].option1);
            optionController2.SetSprite(csvDatas[nowId].option2);
            optionController3.SetSprite(csvDatas[nowId].option3);


            if (csvDatas[nowId].jump_id1 != 0)
            {
                nowId = csvDatas[nowId].jump_id1;
            }
            else
            {
                nowId++;
            }
        }
    }
}
