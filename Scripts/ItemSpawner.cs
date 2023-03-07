using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    private Transform leftextremePos;
    private Transform rightextremePos;
    private float currentTime;
    public float maxTime;



    /// <summary>
    /// Таймер для создания нового итема
    /// </summary>
    private void TimeToSpawn()
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0)
        {
            ItemSpawn();
            currentTime = maxTime;
        }
    }
    /// <summary>
    /// создание случайного итема
    /// </summary>
    private void ItemSpawn()
    {
            // Random Index
            int i = Random.Range(0, items.Length);

            //создвние итема на случайной позиции в рамках указанных в коде
            Instantiate(items[i], new Vector2(Random.Range(leftextremePos.position.x, rightextremePos.position.x), transform.position.y), Quaternion.identity);

    }


    // Start is called before the first frame update
    void Start()
    {
        leftextremePos = transform.Find("LeftExtremePos");
        rightextremePos = transform.Find("RightExtremePos");
    }

    // Update is called once per frame
    void Update()
    {
        TimeToSpawn();
    }
}
