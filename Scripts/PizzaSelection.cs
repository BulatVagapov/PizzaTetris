using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PizzaSelection : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] Text pizzaName;
    [SerializeField] Sprite selectedButton;
    [SerializeField] Sprite unselectedButton;

    [SerializeField] PizzaSelection pizzaButton_1;
    [SerializeField] PizzaSelection pizzaButton_2;
    public bool selected;
    [SerializeField] int pizzaIndex;

    
    public void SelectPizza()
    {
        gameManager.pizzaIndex = pizzaIndex;
        selected = true;
        pizzaButton_1.selected = false;
        pizzaButton_2.selected = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            pizzaName.color = new Color(1f, 1f, 1f, 1f); 
            GetComponent<Image>().sprite = selectedButton;
        }
        else
        {
            pizzaName.color = new Color(0.5882f, 0.6549f, 0.6862f, 1f);
            GetComponent<Image>().sprite = unselectedButton;
        }
    }
}
