using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDestroyer : MonoBehaviour
{

    /// <summary>
    /// уничтожение объектов попавших в триггер
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

}
