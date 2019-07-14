using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBookController : MonoBehaviour
{
    GameController gameController;
    GameObject noteBookOverallObject;
    GameObject mapObject;
    GameObject familyObject;
    float time;

    // Use this for initialization
    void Start()
    {
        time = 0;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        noteBookOverallObject = GameObject.Find("NoteBookOverall");
        familyObject = GameObject.Find("Family");
        mapObject = GameObject.Find("MapImage");
        familyObject.SetActive(gameController.FlagCheck("display_correlation_diagram"));
        mapObject.SetActive(gameController.FlagCheck("display_map"));
    }

    // Update is called once per frame
    void Update()
    {
        familyObject.SetActive(gameController.FlagCheck("display_correlation_diagram"));
        mapObject.SetActive(gameController.FlagCheck("display_map"));
        time += Time.deltaTime * 5;
        float[] scales = new float[2] { time, 1.0f };
        float scale = Mathf.Min(scales);
        noteBookOverallObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void CloseNoteBook()
    {
        gameController.CloseNoteBook();
    }

    public void ResetScale()
    {
        time = 0;
        noteBookOverallObject.transform.localScale = new Vector3(0, 0, 0);
    }
}
