using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public string[] tagsOfPushingObj;

    [SerializeField] Texture2D cursorOnPushingObj;
    [SerializeField] Texture2D defaultCursorImg;
    
    
    public void SetCursor(bool pushingObj)
    {
        if (pushingObj)
        {
            Cursor.SetCursor(cursorOnPushingObj, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(defaultCursorImg, Vector2.zero, CursorMode.Auto);
        }
    }

    private void Start()
    {
        SetCursor(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            SetCursor(false);
        }
    }
}
