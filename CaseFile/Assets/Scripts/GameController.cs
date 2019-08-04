using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    Animator canvasAnimator;

    const int SCENARIO_INDEX_ID = 0;
    const int SCENARIO_INDEX_TYPE = 1;
    const int SCENARIO_INDEX_CHARACTER = 2;
    const int SCENARIO_INDEX_NAME = 3;
    const int SCENARIO_INDEX_POS_X = 4;
    const int SCENARIO_INDEX_POS_Y = 5;
    const int SCENARIO_INDEX_SCALE = 6;
    const int SCENARIO_INDEX_EYE = 7;
    const int SCENARIO_INDEX_EYE_BROWS = 8;
    const int SCENARIO_INDEX_MOUSE = 9;
    const int SCENARIO_INDEX_OPTION1 = 10;
    const int SCENARIO_INDEX_OPTION2 = 11;
    const int SCENARIO_INDEX_OPTION3 = 12;
    const int SCENARIO_INDEX_VOICE = 13;
    const int SCENARIO_INDEX_VOLUME = 14;
    const int SCENARIO_INDEX_FONT_SIZE = 15;
    const int SCENARIO_INDEX_WORDS = 16;
    const int SCENARIO_INDEX_CHOICES = 18;
    const int SCENARIO_INDEX_TARGET_FLAG = 19;
    const int SCENARIO_INDEX_JUMP_IDS = 20;

    public enum State { Playing, SkipDialog, ItemList, ChoiceItemList, BackLog, Config, NoteBook, GoToTitle, Ending };
    State state;



    public GameObject skipDialogPrefab;
    public GameObject noteBookPrefab;
    public GameObject choicesPrefab;
    public GameObject itemListPrefab;
    public GameObject backLogPrefab;

    private char lf = (char)10;
    GameObject yukariOverObject;
    WindowTextController windowTextController;
    GraphicController eyeController;
    GraphicController mouseController;
    GraphicController eyeBrowsController;
    GraphicController optionController1;
    GraphicController optionController2;
    GraphicController optionController3;
    FadePanelController fadePanelController;
    FadePanelController overFadePanelController;
    PlaceNameController placeNameController;
    ItemListController itemListController;
    NoteBookController noteBookController;
    Text windowText;
    Text nameText;
    Text backLogText;
    Text debugFlagText;
    GameObject sepiaPanelObject;
    GameObject hidePanelObject;
    GameObject skipDialogObject;
    GameObject noteBookObject;
    GameObject choiceObject;
    GameObject itemListObject;
    GameObject backLogObject;
    GameObject[] choicesObjects = new GameObject[8];
    Text[] choicesText = new Text[8];
    GameObject masterObject;
    GameObject maidObject;
    GameObject butlerObject;
    GameObject takagiObject;
    GameObject takagi2Object;
    GameObject aritaObject;
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
    public int firstId;
    int nowId;

    const float DEFAULT_CHARACTER_SCALE = 0.7f;
    const float DEFAULT_VOLUME = 1.0f;
    const int DEFAULT_FONT_SIZE = 30;
    private bool isFading = false;
    private bool isVoiceOn;
    private bool isSEOn;
    private bool isBGMOn;
    private string nowBGM = "";

    Dictionary<string, bool> flags = new Dictionary<string, bool>();
    string lastUpdateFlag = "";

    private struct ScenarioData
    {
        public int id;
        public string type;
        public string character;
        public string name;
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
        public float volume;
        public int font_size;
        public string words;
        public string[] choices;
        public string target_flag;
        public string[] jump_ids;

        public ScenarioData(string id, string type, string character, string name, string pos_x, string pos_y, string scale, string eye, string eye_brows, string mouse, string option1, string option2, string option3, string voice, string volume, string font_size, string words, string choices, string target_flag, string jump_ids)
        {
            this.id = int.Parse(id);
            this.type = type;
            this.character = character;
            this.name = name;
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
            this.volume = (volume == "" ? DEFAULT_VOLUME : float.Parse(volume));
            this.font_size = (font_size == "" ? DEFAULT_FONT_SIZE : int.Parse(font_size));
            this.words = words;
            this.choices = (choices == "" ? new string[] { } : choices.Split('/'));
            this.target_flag = target_flag;
            this.jump_ids = (jump_ids == "" ? new string[] { } : jump_ids.Split('/'));
        }
    }

    private TextAsset csvFile;
    private Dictionary<int, ScenarioData> csvDatas = new Dictionary<int, ScenarioData>();

    private void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        canvasAnimator = canvas.GetComponent<Animator>();
        choiceObject = Instantiate(choicesPrefab, canvas.transform.position, Quaternion.identity);
        choiceObject.transform.parent = canvas.transform;
        skipDialogObject = Instantiate(skipDialogPrefab, canvas.transform.position, Quaternion.identity);
        skipDialogObject.transform.parent = canvas.transform;
        noteBookObject = Instantiate(noteBookPrefab, canvas.transform.position, Quaternion.identity);
        noteBookObject.transform.parent = canvas.transform;
        itemListObject = Instantiate(itemListPrefab, canvas.transform.position, Quaternion.identity);
        itemListObject.transform.parent = canvas.transform;
        backLogObject = Instantiate(backLogPrefab, canvas.transform.position, Quaternion.identity);
        backLogObject.transform.parent = canvas.transform;
    }

    void Start()
    {
        canvasAnimator.SetBool("isEnding", false);
        isSEOn = StaticController.isSEOn;
        isBGMOn = StaticController.isBGMOn;
        isVoiceOn = StaticController.isVoiceOn;
        state = State.Playing;
        yukariOverObject = GameObject.Find("YukariOver");
        eyeController = GameObject.Find("Eye").GetComponent<GraphicController>();
        mouseController = GameObject.Find("Mouse").GetComponent<GraphicController>();
        eyeBrowsController = GameObject.Find("EyeBrows").GetComponent<GraphicController>();
        optionController1 = GameObject.Find("Option1").GetComponent<GraphicController>();
        optionController2 = GameObject.Find("Option2").GetComponent<GraphicController>();
        optionController3 = GameObject.Find("Option3").GetComponent<GraphicController>();
        itemListController = itemListObject.GetComponent<ItemListController>();
        noteBookController = noteBookObject.GetComponent<NoteBookController>();
        windowText = GameObject.Find("WindowText").GetComponent<Text>();
        nameText = GameObject.Find("NameText").GetComponent<Text>();
        backLogText = GameObject.Find("BackLogText").GetComponent<Text>();
        //debugFlagText = GameObject.Find("DebugFlagText").GetComponent<Text>();
        skipDialogObject.SetActive(false);
        skipDialogObject.name = "SkipDialog";
        choicesObjects[0] = GameObject.Find("Choices1");
        choicesObjects[1] = GameObject.Find("Choices2");
        choicesObjects[2] = GameObject.Find("Choices3");
        choicesObjects[3] = GameObject.Find("Choices4");
        choicesObjects[4] = GameObject.Find("Choices5");
        choicesObjects[5] = GameObject.Find("Choices6");
        choicesObjects[6] = GameObject.Find("Choices7");
        choicesObjects[7] = GameObject.Find("Choices8");
        choicesText[0] = GameObject.Find("ChoicesText1").GetComponent<Text>();
        choicesText[1] = GameObject.Find("ChoicesText2").GetComponent<Text>();
        choicesText[2] = GameObject.Find("ChoicesText3").GetComponent<Text>();
        choicesText[3] = GameObject.Find("ChoicesText4").GetComponent<Text>();
        choicesText[4] = GameObject.Find("ChoicesText5").GetComponent<Text>();
        choicesText[5] = GameObject.Find("ChoicesText6").GetComponent<Text>();
        choicesText[6] = GameObject.Find("ChoicesText7").GetComponent<Text>();
        choicesText[7] = GameObject.Find("ChoicesText8").GetComponent<Text>();

        noteBookObject.SetActive(false);
        choiceObject.SetActive(false);
        itemListObject.SetActive(false);
        backLogObject.SetActive(false);
        imageObject = GameObject.Find("Image");
        backGroundObject = GameObject.Find("BackGround");
        imageObject.SetActive(false);

        windowTextController = windowText.GetComponent<WindowTextController>();
        fadePanelController = GameObject.Find("FadePanel").GetComponent<FadePanelController>();
        overFadePanelController = GameObject.Find("OverFadePanel").GetComponent<FadePanelController>();
        placeNameController = GameObject.Find("PlaceName").GetComponent<PlaceNameController>();

        sepiaPanelObject = GameObject.Find("SepiaPanel");
        sepiaPanelObject.SetActive(false);
        hidePanelObject = GameObject.Find("HidePanel");
        hidePanelObject.SetActive(false);
        masterObject = GameObject.Find("Master");
        maidObject = GameObject.Find("Maid");
        butlerObject = GameObject.Find("Butler");
        takagiObject = GameObject.Find("Takagi");
        takagi2Object = GameObject.Find("Takagi2");
        aritaObject = GameObject.Find("Arita");
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
            int id = 0;
            int.TryParse(datas[SCENARIO_INDEX_ID], out id);
            if (id == 0)
            {
                continue;
            }
            var scenarioData = new ScenarioData(
                datas[SCENARIO_INDEX_ID],
                datas[SCENARIO_INDEX_TYPE],
                datas[SCENARIO_INDEX_CHARACTER],
                datas[SCENARIO_INDEX_NAME],
                datas[SCENARIO_INDEX_POS_X],
                datas[SCENARIO_INDEX_POS_Y],
                datas[SCENARIO_INDEX_SCALE],
                datas[SCENARIO_INDEX_EYE],
                datas[SCENARIO_INDEX_EYE_BROWS],
                datas[SCENARIO_INDEX_MOUSE],
                datas[SCENARIO_INDEX_OPTION1],
                datas[SCENARIO_INDEX_OPTION2],
                datas[SCENARIO_INDEX_OPTION3],
                datas[SCENARIO_INDEX_VOICE],
                datas[SCENARIO_INDEX_VOLUME],
                datas[SCENARIO_INDEX_FONT_SIZE],
                datas[SCENARIO_INDEX_WORDS],
                datas[SCENARIO_INDEX_CHOICES],
                datas[SCENARIO_INDEX_TARGET_FLAG],
                datas[SCENARIO_INDEX_JUMP_IDS]
            );
            csvDatas.Add(id, scenarioData);
        }
        overFadePanelController.FadeIn();

        isFading = true;
        nowId = firstId;
    }

    void Update()
    {
        if (isFading && !fadePanelController.IsFading() && !overFadePanelController.IsFading())
        {
            isFading = false;
            PlayScenario();
        }

        if (Input.GetKey(KeyCode.LeftControl) && state == State.Playing)
        {
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
                case State.NoteBook:
                    CloseNoteBook();
                    break;
                case State.ItemList:
                    CloseItemList();
                    break;
                case State.BackLog:
                    backLogText.GetComponent<BackLogController>().ResetPosition();
                    CloseBackLog();
                    break;
                case State.Config:
                default:
                    break;
            }
        }

        // ホイール
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0 && state == State.Playing)
        {
            OpenBackLog();
        }
    }

    public void PlayScenario(bool isSkip = false)
    {
        if (state == State.GoToTitle || state == State.Ending)
        {
            return;
        }

        Debug.Log("ID: " + csvDatas[nowId].id);
        //debugFlagText.text = "last_update_flag : " + lastUpdateFlag + "\n";
        //foreach (KeyValuePair<string, bool> flag in flags)
        //{
        //    debugFlagText.text += flag.Key + ",";
        //}


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
                if (windowTextController.IsDrawn())
                {
                    AudioManager.Instance.StopSE();
                    ChangeFacial();
                    Talk(isSkip);
                    nowId++;
                }
                else
                {
                    windowTextController.AllDrawText();
                }
                break;
            case "jump":
                nowId = int.Parse(csvDatas[nowId].jump_ids[0]);
                PlayScenario(); // jump_idの先に進めて、もう一度PlayScenarioを実行する
                break;
            case "choice":

                Debug.Log("choices count" + csvDatas[nowId].choices.Length);

                int length = csvDatas[nowId].choices.Length;
                choiceObject.SetActive(true);

                if (length == 8)
                {
                    choiceObject.transform.localPosition = new Vector3(0, 320, 0);
                    choiceObject.transform.localScale = new Vector3(0.9f, 0.9f, 1.0f);
                }
                else
                {
                    choiceObject.transform.localPosition = new Vector3(0, (length - 1) * 50, 0);
                    choiceObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }



                string[] targetFlags;
                if (csvDatas[nowId].target_flag == "")
                {
                    targetFlags = new string[] { };
                }
                else
                {
                    targetFlags = csvDatas[nowId].target_flag.Split('/');
                }

                Debug.Log(targetFlags.Length);
                for (int i = 0; i < 8; i++)
                {
                    if (i < length)
                    {
                        choicesText[i].text = csvDatas[nowId].choices[i];
                        choicesObjects[i].SetActive(true);

                        // すでに選択済みの選択肢を消すため、target_flagsを満たしたものは非表示にする
                        if (targetFlags.Length > i)
                        {
                            if (FlagCheck(targetFlags[i]))
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
                if (FlagCheck(csvDatas[nowId].target_flag))
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

                if (FlagCheck(csvDatas[nowId].target_flag))
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
            case "play_bgm":
                Debug.Log("BGM: " + csvDatas[nowId].voice);

                if (csvDatas[nowId].voice == "")
                {
                    AudioManager.Instance.StopBGM();
                }
                else if (isBGMOn)
                {
                    AudioManager.Instance.FadeInBGM(csvDatas[nowId].voice, csvDatas[nowId].volume, true);
                }
                nowBGM = csvDatas[nowId].voice;
                nowId++;
                PlayScenario(); // 次の行に進めて、もう一度PlayScenarioを実行する
                break;
            case "back_effect":
                switch (csvDatas[nowId].character)
                {
                    case "sepia":
                        sepiaPanelObject.SetActive(true);
                        break;
                    case "hide":
                        hidePanelObject.SetActive(true);
                        break;
                    default:
                        sepiaPanelObject.SetActive(false);
                        hidePanelObject.SetActive(false);
                        break;
                }
                nowId++;
                PlayScenario(); // 次の行に進めて、もう一度PlayScenarioを実行する
                break;
            case "choice_item":
                OpenItemList(true);
                break;
            case "go_to_title":
                state = State.GoToTitle;
                overFadePanelController.FadeOut("TitleScene");
                break;
            case "go_to_ending":
                StartEnding();
                break;
            case "place_change":
                placeNameController.ChangePlaceName(csvDatas[nowId].words);
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
            Debug.Log("SE: " + csvDatas[nowId].voice + ", VOLUME: " + csvDatas[nowId].volume);
            AudioManager.Instance.StopSE();

            if ((isVoiceOn && csvDatas[nowId].character == "Y") || (isSEOn && csvDatas[nowId].character != "Y"))
            {
                AudioManager.Instance.PlaySE(csvDatas[nowId].voice, csvDatas[nowId].volume);
            }
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

        nameText.text = csvDatas[nowId].name;
        windowText.fontSize = csvDatas[nowId].font_size;
        string text = csvDatas[nowId].words.Replace("\\n", lf.ToString());
        windowTextController.UpdateText(text, isSkip);

        backLogText.text += lf.ToString() + lf.ToString() + nameText.text;
        backLogText.text += lf.ToString() + text;

        // 15000字を超えるとUnityのtextで処理しきれなくなるので、10000字超えたら先頭から1000字消す
        if (backLogText.text.Length > 10000)
        {
            backLogText.text = backLogText.text.Remove(0, 1000);
        }
        backLogText.rectTransform.sizeDelta = new Vector2(backLogText.rectTransform.sizeDelta.x, backLogText.preferredHeight + 100);
    }

    GameObject GetTargetObject(string character)
    {
        switch (character)
        {
            case "S":
                return masterObject;
            case "M":
                return maidObject;
            case "B":
                return butlerObject;
            case "T":
                return takagiObject;
            case "T2":
                return takagi2Object;
            case "arita":
                return aritaObject;
            case "Y":
                return yukariOverObject;
            case "R":
                return cousinFirstSonObject;
            case "E":
                return cousinSecondWifeObject;
            case "O":
                return cousinFirstWifeObject;
            case "H":
                return cousinFirstHusObject;
            case "K":
                return cousinSecondHusObject;
            case "A":
                return cousinSecondDaughterObject;
            case "F":
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
        takagi2Object.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        aritaObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
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

    public void SelectChoiceItems(string choiceItem)
    {
        if (state == State.ChoiceItemList)
        {
            int nextId = nowId + 1;
            int length = csvDatas[nowId].choices.Length;
            for (int i = 0; i < csvDatas[nowId].choices.Length; i++)
            {
                if (csvDatas[nowId].choices[i] == choiceItem)
                {
                    nextId = int.Parse(csvDatas[nowId].jump_ids[i]);
                }
            }
            nowId = nextId;
            CloseItemList();
            PlayScenario();
        }
    }

    public void DebugResetButton(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void DebugItemButton()
    {
        string[] itemFlags = new string[] { "tsuru", "tabako", "oreta_kasa", "kawagutsu", "key", "clip", "notification_adoption", "isho", "kusuri", "memo_wet", "memo_blood", "memo_key", "memo_hari" };
        foreach (string itemFlag in itemFlags)
        {
            if (FlagCheck(itemFlag))
            {
                flags[itemFlag] = true;
            }
            else
            {
                flags.Add(itemFlag, true);
            }

        }
    }

    public void DebugJumpButton(int id)
    {
        string[] characters = new string[] { "S", "M", "B", "T", "arita", "Y", "R", "E", "O", "H", "K", "A", "F" };
        foreach (string character in characters)
        {
            GameObject targetObject = GetTargetObject(character);
            targetObject.GetComponent<Image>().enabled = false;
        }
        nowId = id;
        PlayScenario();
    }

    public void OpenNoteBook()
    {
        state = State.NoteBook;
        noteBookObject.SetActive(true);
        if (isSEOn)
        {
            AudioManager.Instance.PlaySE("page1");
        }
    }

    public void CloseNoteBook()
    {
        if (state == State.NoteBook)
        {
            state = State.Playing;
        }
        noteBookController.ResetScale();
        noteBookObject.SetActive(false);
    }

    public void OpenSkipDialog()
    {
        state = State.SkipDialog;
        skipDialogObject.SetActive(true);
    }

    public void CloseSkipDialog()
    {
        if (state == State.SkipDialog)
        {
            state = State.Playing;
        }
        skipDialogObject.SetActive(false);
    }

    public void OpenItemList(bool isChoice = false)
    {
        if (isChoice)
        {
            state = State.ChoiceItemList;
        }
        else
        {
            state = State.ItemList;
        }
        if (isSEOn)
        {
            AudioManager.Instance.PlaySE("page1");
        }

        itemListObject.SetActive(true);
    }

    public void CloseItemList()
    {
        if (state == State.ItemList || state == State.ChoiceItemList)
        {
            state = State.Playing;
        }
        itemListController.ResetScale();
        itemListObject.SetActive(false);
    }

    public void OpenBackLog()
    {
        state = State.BackLog;
        backLogObject.SetActive(true);
    }

    public void CloseBackLog()
    {
        if (state == State.BackLog)
        {
            state = State.Playing;
        }
        backLogObject.SetActive(false);
    }

    public void SkipScenario()
    {
        for (int i = 0; i < 1000; i++)
        {
            if (csvDatas[nowId].type == "choice" || csvDatas[nowId].type == "choice_item")
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


    public void SetVoiceOnOff()
    {
        bool isOn = GameObject.Find("ToggleVoiceOn").GetComponent<Toggle>().isOn;
        StaticController.SetVoiceOnOff(isOn);
    }
    public void SetSEOnOff()
    {
        bool isOn = GameObject.Find("ToggleSEOn").GetComponent<Toggle>().isOn;
        StaticController.SetSEOnOff(isOn);
    }
    public void SetBGMOnOff()
    {
        bool isOn = GameObject.Find("ToggleBGMOn").GetComponent<Toggle>().isOn;
        isBGMOn = isOn;
        StaticController.SetBGMOnOff(isOn);

        if (isOn && nowBGM != "")
        {
            AudioManager.Instance.FadeInBGM(nowBGM, 0.05f, true);
        }
        else
        {
            AudioManager.Instance.StopBGM();
        }
    }


    public bool FlagCheck(string targetFlag)
    {
        return flags.ContainsKey(targetFlag) && flags[targetFlag];
    }

    public void StartEnding()
    {
        state = State.Ending;
        canvasAnimator.SetBool("isEnding", true);
        StaticController.SetClear(true);
    }

}
