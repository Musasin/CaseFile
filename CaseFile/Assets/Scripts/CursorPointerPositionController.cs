using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPointerPositionController : MonoBehaviour
{

    public GameObject cursorPointerObject;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            GameObject obj = Instantiate(cursorPointerObject, mousePosition, Quaternion.identity);
            obj.transform.SetParent(this.transform);
        }
    }
}
