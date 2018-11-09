using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    enum Types { Appearance = 1, Talk = 2, Jump = 3, Choice = 4, FlagCheck = 5, FlagUpdate = 6 };
    private char lf = (char)10;
    YukariEyeController eyeController;
    YukariMouseController mouseController;
    YukariEyeBrowsController eyeBrowsController;
    YukariOptionController optionController1;
    YukariOptionController optionController2;
    YukariOptionController optionController3;
    Text windowText;
    Text nameText;
    GameObject choicesObject;
    Text choicesText1;
    Text choicesText2;
    GameObject masterObject;


    Dictionary<string, bool> flags = new Dictionary<string, bool>();

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
        windowText = GameObject.Find("WindowText").GetComponent<Text>();
        nameText = GameObject.Find("NameText").GetComponent<Text>();
        choicesObject = GameObject.Find("Choices");
        choicesText1 = GameObject.Find("ChoicesText1").GetComponent<Text>();
        choicesText2 = GameObject.Find("ChoicesText2").GetComponent<Text>();
        choicesObject.SetActive(false);

        masterObject = GameObject.Find("Master");

        //csvFile = Resources.Load("sabun_check") as TextAsset;
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
            PlayScenario();
        }
    }

    void PlayScenario()
    {
        Debug.Log("ID: " + csvDatas[nowId].id);

        switch ((Types)Enum.ToObject(typeof(Types), csvDatas[nowId].type))
        {
            case Types.Appearance:

                // ここはそのうちなんとかする
                switch (csvDatas[nowId].character)
                {
                    case "yukari":
                        break;
                    case "master":
                        masterObject.GetComponent<Image>().enabled = true;
                        break;
                    default:
                        nameText.text = "";
                        break;
                }

                ChangeFacial();
                nowId++;
                PlayScenario(); // 次の行に進めて、もう一度PlayScenarioを実行する
                break;
            case Types.Talk:
                ChangeFacial();
                Talk();
                nowId++;
                break;
            case Types.Jump:
                nowId = csvDatas[nowId].jump_id1;
                PlayScenario(); // jump_idの先に進めて、もう一度PlayScenarioを実行する
                break;
            case Types.Choice:
                choicesText1.text = csvDatas[nowId].choise1;
                choicesText2.text = csvDatas[nowId].choise2;
                choicesObject.SetActive(true); // 選択肢を表示して、選ばれるまでIDはそのまま
                break;
            case Types.FlagCheck:
                Debug.Log("flag check: " + csvDatas[nowId].target_flag);
                if (flags.ContainsKey(csvDatas[nowId].target_flag) && flags[csvDatas[nowId].target_flag])
                {
                    Debug.Log("true. jump to " + csvDatas[nowId].jump_id1);
                    nowId = csvDatas[nowId].jump_id1;
                }
                else
                {
                    Debug.Log("false. jump to " + csvDatas[nowId].jump_id1);
                    nowId = csvDatas[nowId].jump_id2;
                }
                PlayScenario(); // jump_idの先に進めて、もう一度PlayScenarioを実行する
                break;
            case Types.FlagUpdate:
                Debug.Log("flag update: " + csvDatas[nowId].target_flag);
                flags.Add(csvDatas[nowId].target_flag, true);
                nowId++;
                PlayScenario(); // 次の行に進めて、もう一度PlayScenarioを実行する
                break;
            default:
                break;
        }
    }

    void ChangeFacial()
    {
        eyeController.SetSprite(csvDatas[nowId].eye);
        mouseController.SetSprite(csvDatas[nowId].mouse);
        eyeBrowsController.SetSprite(csvDatas[nowId].eye_brows);
        optionController1.SetSprite(csvDatas[nowId].option1);
        optionController2.SetSprite(csvDatas[nowId].option2);
        optionController3.SetSprite(csvDatas[nowId].option3);
    }

    void Talk()
    {
        if (csvDatas[nowId].voice != "")
        {
            Debug.Log("SE: " + csvDatas[nowId].voice);
            AudioManager.Instance.StopSE();
            AudioManager.Instance.PlaySE(csvDatas[nowId].voice);
        }

        // ここはそのうちなんとかする
        switch (csvDatas[nowId].character)
        {
            case "yukari":
                nameText.text = "【ゆかり】";
                break;
            case "master":
                nameText.text = "【館の主人】";
                break;
            default:
                nameText.text = "";
                break;
        }

        windowText.text = csvDatas[nowId].words.Replace("\\n", lf.ToString());
    }

    public void SelectChoices1()
    {
        nowId = csvDatas[nowId].jump_id1;
        choicesObject.SetActive(false);
        PlayScenario();
    }
    public void SelectChoices2()
    {
        nowId = csvDatas[nowId].jump_id2;
        choicesObject.SetActive(false);
        PlayScenario();
    }

    public void DebugResetButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
