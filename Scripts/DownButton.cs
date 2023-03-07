using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DownButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Group group;

    /// <summary>
    /// Включение форсированной скорости падения
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        group.acceleratedFall = true;
    }

    /// <summary>
    /// Отключение форсированной скорости падения
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        group.acceleratedFall = false;
    }
}
