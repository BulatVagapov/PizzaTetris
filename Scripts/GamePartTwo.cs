using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePartTwo : MonoBehaviour
{
    [SerializeField] Item[] pepperoni;
    [SerializeField] Item[] fantasticFour;
    [SerializeField] Item[] caesar;
    [SerializeField] AttemptController attenptController;
    private GameManager gameManager;
    private Item[] loadedItems;
    public int itemIndex;

    [SerializeField] int maxAttempt;
    private int attempt;


    /// <summary>
    /// Выбор загружаемых индексов в зависимости от выбора игрока
    /// </summary>
    /// <param name="pizza"></param>
    public void SelectionOfDownloadableItems(int pizza)
    {
        switch (pizza)
        {
            case 0:
                loadedItems = pepperoni;
                break;
            case 1:
                loadedItems = fantasticFour;
                break;
            case 2:
                loadedItems = caesar;
                break;
        }
    }
    
    /// <summary>
    /// включает и выключает массив ингридиентов выбранной пиццы
    /// </summary>
    public void HideAndShowLoadedItems(bool on)
    {
        for(int i = 0; i < loadedItems.Length; i++)
        {
            loadedItems[i].gameObject.SetActive(on);
        }
    }

    /// <summary>
    /// Устанавливает стартовую прозрачность ингридиенам выбранной пиццы
    /// </summary>
    public void RestartItemPlaces()
    {
        for(int i = 0; i < loadedItems.Length; i++)
        {
            loadedItems[i].ResetItemPlace();
        }
    }


    /// <summary>
    /// определение объекта на котором кликнули мышкой
    /// </summary>
    /// <returns></returns>
    private GameObject FindItem()
    {
        GameObject item = null;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                item = hit.collider.gameObject;
            }
        }

        return item;
    }

    /// <summary>
    /// в случае ыбора игроком правильного итема место под нужный итем меняет прозрачность, объект который выбрал игрок уничтожается,
    /// если выбран ингридиент, которого нет в пицце уменьшается количество попыток
    /// </summary>
    /// <param name="item"></param>
    private void CorrectItem(GameObject item)
    {
        if (item != null)
        {
            if (itemIndex < loadedItems.Length)
            {
                int flag = itemIndex; 

                for(int i = 0; i < loadedItems.Length; i++)
                {
                    if(item.tag == loadedItems[i].NameOfItem && !loadedItems[i].found)
                    {
                        loadedItems[i].found = true;
                        itemIndex++;
                        break;
                    }
                }

                if(flag == itemIndex)
                {
                    attempt--;
                }
            }
            Destroy(item);
        }
    }
    
    /// <summary>
    /// Сброс попыток
    /// </summary>
    public void ResetAttempt()
    {
        attempt = maxAttempt;
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        CorrectItem(FindItem());

        attenptController.AttemptsImgsController(attempt);

        if (attempt <= 0 && itemIndex < loadedItems.Length)
        {
            gameManager.PartTwoOver(false);
        }

        if (itemIndex == loadedItems.Length)
        {
            gameManager.PartTwoOver(true);
        }
    }
}
