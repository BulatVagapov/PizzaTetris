using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public bool found;
    [SerializeField] private string nameOfItem;
    [SerializeField] private SpriteRenderer img;

    public string NameOfItem { get { return this.nameOfItem; } }

    public void ResetItemPlace()
    {
        found = false;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        found = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (found)
        {
            img.color = new Color(1, 1, 1, 1);
        }
        else
        {
            img.color = new Color(0, 0, 0, 0.5f);
        }
    }
}
