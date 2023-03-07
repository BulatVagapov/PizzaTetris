using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICursorController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        FindObjectOfType<CursorController>().SetCursor(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        FindObjectOfType<CursorController>().SetCursor(false);
    }
}
