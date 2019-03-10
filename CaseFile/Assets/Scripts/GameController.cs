using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public enum State { Playing, SkipDialog, ItemList, BackLog, Config };
    State state;

    public GameObject skipDialogPrefab;
    public GameObject choicesPrefab;
    public GameObject itemListPrefab;
    public GameObject backLogPrefab;

    private char lf = (char)10;
    GameObject yukariOverObject;
    GraphicController eyeController;
    GraphicController mouseController;
    GraphicController eyeBrowsController;
    GraphicController optionController1;
    GraphicController optionController2;
    GraphicController optionController3;
    FadePanelController fadePanelController;
    Text windowText;
    Text nameText;
    Text backLogText;
    Text debugFlagText;
    GameObject skipDialogObject;
    GameObject choiceObject;
    GameObject itemListObject;
    GameObject backLogObject;
    GameObject[] choicesObjects = new GameObject[7];
    Text[] choicesText = new Text[7];
    GameObject masterObject;
    GameObject maidObject;
    GameObject butlerObject;
    GameObject takagiObject;
    GameObject imageObject;
    GameObject backGroundObject;
    GameObject cousinFirstSonObject;
    GameObject cousinSecondWifeObject;
    GameObject cousinFirstWifeObject;
    GameObject cousinFirstHusObject;
    GameObject cousinSecondHusObject;
    GameObject cousinSecondDaughterObject;
    GameObject sectetChildObject;
    public string csvFileName;

    const float DEFAULT_CHARACTER_SCALE = 0.7f;
    const int DEFAULT_FONT_SIZE = 30;
    private bool isFading = false;

    Dictionary<string, bool> flags = new Dictionary<string, bool>();
    string lastUpdateFlag = "";

    private struct ScenarioData
    {
        public int id;
        public string type;
        public string character;
        public int pos_x;
        public int pos_y;
        public float scale;
        public string eye;
        public string eye_brows;
        public string mouse;
        public string option1;
        public string option2;
        public string option3;
        public string voice;
        public int font_size;
        public string words;
        public string[] choices;
        public string target_flag;
        public string[] jump_ids;

        public ScenarioData(string id, string type, string character, string pos_x, string pos_y, string scale, string eye, string eye_brows, string mouse, string option1, string option2, string option3, string voice, string font_size, string words, string choices, string target_flag, string jump_ids)
        {
            this.id = int.Parse(id);
            this.type = type;
            this.character = character;
            this.pos_x = (pos_x == "" ? 0 : int.Parse(pos_x));
            this.pos_y = (pos_y == "" ? 0 : int.Parse(pos_y));
            this.scale = (scale == "" ? DEFAULT_CHARACTER_SCALE : float.Parse(scale));
            this.eye = eye;
            this.eye_brows = eye_brows;
            this.mouse = mouse;
            this.option1 = option1;
            this.option2 = option2;
            this.option3 = option3;
            this.voice = voice;
            this.font_size = (font_size == "" ? DEFAULT_FONT_SIZE : int.Parse(font_size));
            this.words = words;
            this.choices = (choices == "" ? new string[] { } : choices.Split('/'));
            this.target_flag = target_flag;
            this.jump_ids = (jump_ids == "" ? new string[] { } : jump_ids.Split('/'));
        }
    }

    private TextAsset csvFile;
    private Dictionary<int, ScenarioData> csvDatas = new Dictionary<int, ScenarioData>();
    int nowId = 1001001;

    private void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        choiceObject = Instantiate(choicesPrefab, canvas.transform.position, Quaternion.identity);
        choiceObject.transform.parent = canvas.transform;
        skipDialogObject = Instantiate(skipDialogPrefab, canvas.transform.position, Quaternion.identity);
        skipDialogObject.transform.parent = canvas.transform;
        itemListObject = Instantiate(itemListPrefab, canvas.transform.position, Quaternion.identity);
        itemListObject.transform.parent = canvas.transform;
        backLogObject = Instantiate(backLogPrefab, canvas.transform.position, Quaternion.identity);
        backLogObject.transform.parent = canvas.transform;
    }

    void Start()
    {
        state = State.Playing;
        yukariOverObject = GameObject.Find("YukariOver");
        eyeController = GameObject.Find("Eye").GetComponent<GraphicController>();
        mouseController = GameObject.Find("Mouse").GetComponent<GraphicController>();
        eyeBrowsController = GameObject.Find("EyeBrows").GetComponent<GraphicController>();
        optionController1 = GameObject.Find("Option1").GetComponent<GraphicController>();
        optionController2 = GameObject.Find("Option2").GetComponent<GraphicController>();
        optionController3 = GameObject.Find("Option3").GetComponent<GraphicController>();
        windowText = GameObject.Find("WindowText").GetComponent<Text>();
        nameText = GameObject.Find("NameText").GetComponent<Text>();
        backLogText = GameObject.Find("BackLogText").GetComponent<Text>();
        debugFlagText = GameObject.Find("DebugFlagText").GetComponent<Text>();
        skipDialogObject.SetActive(false);
        skipDialogObject.name = "SkipDialog";
        choicesObjects[0] = GameObject.Find("Choices1");
        choicesObjects[1] = GameObject.Find("Choices2");
        choicesObjects[2] = GameObject.Find("Choices3");
        choicesObjects[3] = GameObject.Find("Choices4");
        choicesObjects[4] = GameObject.Find("Choices5");
        choicesObjects[5] = GameObject.Find("Choices6");
        choicesObjects[6] = GameObject.Find("Choices7");
        choicesText[0] = GameObject.Find("ChoicesText1").GetComponent<Text>();
        choicesText[1] = GameObject.Find("ChoicesText2").GetComponent<Text>();
        choicesText[2] = GameObject.Find("ChoicesText3").GetComponent<Text>();
        choicesText[3] = GameObject.Find("ChoicesText4").GetComponent<Text>();
        choicesText[4] = GameObject.Find("ChoicesText5").GetComponent<Text>();
        choicesText[5] = GameObject.Find("ChoicesText6").GetComponent<Text>();
        choicesText[6] = GameObject.Find("ChoicesText7").GetComponent<Text>();
        choiceObject.SetActive(false);
        itemListObject.SetActive(false);
        backLogObject.SetActive(false);
        imageObject = GameObject.Find("Image");
        backGroundObject = GameObject.Find("BackGround");
        imageObject.SetActive(false);
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();

        masterObject = GameObject.Find("Master");
        maidObject = GameObject.Find("Maid");
        butlerObject = GameObject.Find("Butler");
        takagiObject = GameObject.Find("Takagi");
        cousinFirstSonObject = GameObject.Find("CousinFirstSon");
        cousinSecondWifeObject = GameObject.Find("CousinSecondWife");
        cousinFirstWifeObject = GameObject.Find("CousinFirstWife");
        cousinFirstHusObject = GameObject.Find("CousinFirstHus");
        cousinSecondHusObject = GameObject.Find("CousinSecondHus");
        cousinSecondDaughterObject = GameObject.Find("CousinSecondDaughter");
        sectetChildObject = GameObject.Find("SectetChild");

        csvFile = Resources.Load(csvFileName) as TextAsset;

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
            var scenarioData = new ScenarioData(datas[0], datas[1], datas[2], datas[3], datas[4], datas[5], datas[6], datas[7], datas[8], datas[9], datas[10], datas[11], datas[12], datas[13], datas[14], datas[15], datas[16], datas[17]);
            csvDatas.Add(id, scenarioData);
        }
    }

    void Update()
    {
        if (isFading && !fadePanelController.IsFading())
        {
            isFading = false;
            PlayScenario();
        }

        // 右クリック
        if (Input.GetMouseButtonDown(1))
        {
            switch (state)
            {
                case State.Playing:
                    OpenItemList();
                    break;
                case State.SkipDialog:
                    CloseSkipDialog();
                    break;
                case State.ItemList:
                    CloseItemList();
                    break;
                case State.BackLog:
                    backLogText.GetComponent<BackLogController>().ResetPosition();
                    CloseBackLog();
                    break;
                case State.Config:
                    break;
            }
        }

        // ホイール
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log(scroll);
        if (scroll > 0 && state == State.Playing)
        {
            OpenBackLog();
        }
    }

    public void PlayScenario(bool isSkip = false)
    {
        Debug.Log("ID: " + csvDatas[nowId].id);
        debugFlagText.text = "last_update_flag : " + lastUpdateFlag + "\n";
        foreach (KeyValuePair<string, bool> flag in flags)
        {
            debugFlagText.text += flag.Key + ",";
        }


        GameObject targetObject = GetTargetObject(csvDatas[nowId].character);
        switch (csvDatas[nowId].type)
        {
            case "appear":
                if (targetObject != null && targetObject != yukariOverObject)
                {
                    SetNpcPosition(targetObject, csvDatas[nowId].pos_x, csvDatas[nowId].pos_y);
                    SetNpcScale(targetObject, csvDatas[nowId].scale);
                    targetObject.GetComponent<Image>().enabled = true;
                }

                ChangeFacial();
                nowId++;
                PlayScenario(); // 次の行に進めて、もう一度PlayScenarioを実行する
                break;
            case "out":
                if (targetObject != null)
                {
                    targetObject.GetComponent<Image>().enabled = false;
                }
                nowId++;
                PlayScenario(); // 次の行に進めて、もう一度PlayScenarioを実行する
                break;

            case "talk":
                ChangeFacial();
                Talk(isSkip);
                nowId++;
                break;
            case "jump":
                nowId = int.Parse(csvDatas[nowId].jump_ids[0]);
                PlayScenario(); // jump_idの先に進めて、もう一度PlayScenarioを実行する
                break;
            case "choice":

                Debug.Log("choices count" + csvDatas[nowId].choices.Length);

                int length = csvDatas[nowId].choices.Length;
                choiceObject.SetActive(true);
                choiceObject.transform.localPosition = new Vector3(0, (length - 1) * 50, 0);

                string[] targetFlags = csvDatas[nowId].target_flag.Split('/');
                for (int i = 0; i < 7; i++)
                {
                    if (i < length)
                    {
                        choicesText[i].text = csvDatas[nowId].choices[i];
                        choicesObjects[i].SetActive(true);

                        // すでに選択済みの選択肢を消すため、target_flagsを満たしたものは非表示にする
                        if (targetFlags.Length > i)
                        {
                            if (flags.ContainsKey(targetFlags[i]) && flags[targetFlags[i]])
                            {
                                choicesObjects[i].SetActive(false);
                            }
                        }
                    }
                    else
                    {
                        choicesObjects[i].SetActive(false);
                    }
                }
                break;
            case "flag_check":
                Debug.Log("flag check: " + csvDatas[nowId].target_flag);
                if (flags.ContainsKey(csvDatas[nowId].target_flag) && flags[csvDatas[nowId].target_flag])
                {
                    Debug.Log("true. jump to " + csvDatas[nowId].jump_ids[0]);
                    nowId = int.Parse(csvDatas[nowId].jump_ids[0]);
                }
                else
                {
                    Debug.Log("false. jump to " + csvDatas[nowId].jump_ids[1]);
                    nowId = int.Parse(csvDatas[nowId].jump_ids[1]);
                }
                PlayScenario(); // jump_idの先に進めて、もう一度PlayScenarioを実行する
                break;
            case "last_update_flag_check":
                Debug.Log("last_update_flag check: " + csvDatas[nowId].target_flag);
                if (lastUpdateFlag == csvDatas[nowId].target_flag)
                {
                    Debug.Log("true. jump to " + csvDatas[nowId].jump_ids[0]);
                    nowId = int.Parse(csvDatas[nowId].jump_ids[0]);
                }
                else
                {
                    Debug.Log("false. jump to " + csvDatas[nowId].jump_ids[1]);
                    nowId = int.Parse(csvDatas[nowId].jump_ids[1]);
                }
                PlayScenario(); // jump_idの先に進めて、もう一度PlayScenarioを実行する
                break;
            case "flag_update":
                Debug.Log("flag update: " + csvDatas[nowId].target_flag);

                if (flags.ContainsKey(csvDatas[nowId].target_flag))
                {
                    flags[csvDatas[nowId].target_flag] = true;
                }
                else
                {
                    flags.Add(csvDatas[nowId].target_flag, true);
                }
                lastUpdateFlag = csvDatas[nowId].target_flag;

                nowId++;
                PlayScenario(); // 次の行に進めて、もう一度PlayScenarioを実行する
                break;
            case "fade_out":
                fadePanelController.FadeOut();
                isFading = true;
                nowId++;
                break;
            case "fade_in":
                fadePanelController.FadeIn();
                isFading = true;
                nowId++;
                break;
            case "change_background":
                Sprite backGroundSprite = Resources.Load<Sprite>("BackGround/" + csvDatas[nowId].character);
                backGroundObject.GetComponent<Image>().sprite = backGroundSprite;
                backGroundObject.GetComponent<RectTransform>().sizeDelta = new Vector2(backGroundSprite.bounds.size.x, backGroundSprite.bounds.size.y);
                backGroundObject.transform.position = new Vector2(csvDatas[nowId].pos_x, csvDatas[nowId].pos_y);
                backGroundObject.transform.localScale = new Vector2(csvDatas[nowId].scale, csvDatas[nowId].scale);
                backGroundObject.SetActive(true);
                nowId++;
                PlayScenario(); // 次の行に進めて、もう一度PlayScenarioを実行する
                break;
            case "view_image":
                if (csvDatas[nowId].character == "")
                {
                    imageObject.SetActive(false);
                }
                else
                {
                    Sprite sprite = Resources.Load<Sprite>("Images/" + csvDatas[nowId].character);
                    imageObject.GetComponent<Image>().sprite = sprite;
                    imageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(sprite.bounds.size.x, sprite.bounds.size.y);
                    imageObject.transform.position = new Vector2(csvDatas[nowId].pos_x, csvDatas[nowId].pos_y);
                    imageObject.transform.localScale = new Vector2(csvDatas[nowId].scale, csvDatas[nowId].scale);
                    imageObject.SetActive(true);
                }
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
        optionController1.SetSprite(csvDatas[nowId].option1 == "" ? "nothing" : csvDatas[nowId].option1);
        optionController2.SetSprite(csvDatas[nowId].option2 == "" ? "nothing" : csvDatas[nowId].option2);
        optionController3.SetSprite(csvDatas[nowId].option3 == "" ? "nothing" : csvDatas[nowId].option3);
    }

    void Talk(bool isSkip)
    {
        if (csvDatas[nowId].voice != "" && !isSkip)
        {
            Debug.Log("SE: " + csvDatas[nowId].voice);
            AudioManager.Instance.StopSE();
            AudioManager.Instance.PlaySE(csvDatas[nowId].voice);
        }

        GameObject targetObject = GetTargetObject(csvDatas[nowId].character);
        if (targetObject != null)
        {
            if (targetObject != yukariOverObject)
            {
                SetNpcPosition(targetObject, csvDatas[nowId].pos_x, csvDatas[nowId].pos_y);
                SetNpcScale(targetObject, csvDatas[nowId].scale);
            }

            SetAllCharacterDarken();
            SetClearCharacter(targetObject);
        }

        nameText.text = GetCharacterName(csvDatas[nowId].character);
        windowText.fontSize = csvDatas[nowId].font_size;
        windowText.text = csvDatas[nowId].words.Replace("\\n", lf.ToString());
        backLogText.text += lf.ToString() + lf.ToString() + nameText.text;
        backLogText.text += lf.ToString() + windowText.text;
        backLogText.rectTransform.sizeDelta = new Vector2(backLogText.rectTransform.sizeDelta.x, backLogText.preferredHeight);
    }

    string GetCharacterName(string character)
    {
        switch (character)
        {
            case "yukari":
                return "ゆかり";
            case "master":
                return "相馬　胤朋";
            case "maid":
                return "早瀬　さゆり";
            case "butler":
                return "執事";
            case "takagi":
                return "高城刑事";
            case "cousin_first_son":
                return "相馬　亮";
            case "cousin_second_wife":
                return "相馬　理恵";
            case "cousin_first_wife":
                return "相馬　陽子";
            case "cousin_first_hus":
                return "相馬　康彦";
            case "cousin_second_hus":
                return "相馬　孝二";
            case "cousin_second_daughter":
                return "相馬　綾";
            case "secret_child":
                return "三船　和人";
            default:
                return "";
        }
    }

    GameObject GetTargetObject(string character)
    {
        switch (character)
        {
            case "master":
                return masterObject;
            case "maid":
                return maidObject;
            case "butler":
                return butlerObject;
            case "takagi":
                return takagiObject;
            case "yukari":
                return yukariOverObject;
            case "cousin_first_son":
                return cousinFirstSonObject;
            case "cousin_second_wife":
                return cousinSecondWifeObject;
            case "cousin_first_wife":
                return cousinFirstWifeObject;
            case "cousin_first_hus":
                return cousinFirstHusObject;
            case "cousin_second_hus":
                return cousinSecondHusObject;
            case "cousin_second_daughter":
                return cousinSecondDaughterObject;
            case "secret_child":
                return sectetChildObject;
            default:
                return null;
        }
    }

    void SetAllCharacterDarken()
    {
        masterObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        maidObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        butlerObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        takagiObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        cousinFirstSonObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        cousinSecondWifeObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        cousinFirstWifeObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        cousinFirstHusObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        cousinSecondHusObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        cousinSecondDaughterObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        sectetChildObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        yukariOverObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 0.7f);
    }

    void SetClearCharacter(GameObject characterObject)
    {
        if (characterObject == yukariOverObject)
        {
            yukariOverObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 0.0f);
        }
        else
        {
            characterObject.GetComponent<Image>().color = new Color(100, 100, 100);
        }
    }

    void SetNpcPosition(GameObject npcObject, int pos_x, int pos_y)
    {
        if (pos_x != 0 || pos_y != 0)
        {
            npcObject.transform.position = new Vector3(pos_x, pos_y);
        }
    }

    void SetNpcScale(GameObject npcObject, float scale)
    {
        npcObject.transform.localScale = new Vector2(scale, scale);
    }

    public void SelectChoices(int choiceNumber)
    {
        nowId = int.Parse(csvDatas[nowId].jump_ids[choiceNumber - 1]);
        choiceObject.SetActive(false);
        PlayScenario();
    }

    public void DebugResetButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenSkipDialog()
    {
        state = State.SkipDialog;
        skipDialogObject.SetActive(true);
    }

    public void CloseSkipDialog()
    {
        state = State.Playing;
        skipDialogObject.SetActive(false);
    }

    public void OpenItemList()
    {
        state = State.ItemList;
        itemListObject.SetActive(true);
    }

    public void CloseItemList()
    {
        state = State.Playing;
        itemListObject.SetActive(false);
    }

    public void OpenBackLog()
    {
        state = State.BackLog;
        backLogObject.SetActive(true);
    }

    public void CloseBackLog()
    {
        state = State.Playing;
        backLogObject.SetActive(false);
    }

    public void SkipScenario()
    {
        Debug.Log("a");
        while (true)
        {
            Debug.Log("b");
            if (csvDatas[nowId].type == "choice")
            {
                PlayScenario();
                break;
            }
            else
            {
                PlayScenario(true);
            }
        }
    }
}
