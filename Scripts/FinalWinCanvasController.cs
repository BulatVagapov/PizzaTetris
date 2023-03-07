using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalWinCanvasController : MonoBehaviour
{
    [SerializeField] Image pizzaImg;
    [SerializeField] Sprite[] pizzaPictures; 
    [SerializeField] Text absoluteWinText;
    [SerializeField] Text winText;
    [SerializeField] Text tittleText;
    private string pizzaName;
    
    /// <summary>
    /// Настройка экрана победы в зависимости от того сколько и какие пиццы были собраны
    /// </summary>
    /// <param name="pizzaIsreaddy"></param>
    /// <param name="pizzaIndex"></param>
    /// <param name="counterOfReadyPizzas"></param>
    public void SetWinCanvas(int pizzaIndex)
    {
        switch (pizzaIndex)
        {
            case 0:
                pizzaName = "Пепперони";
                pizzaImg.sprite = pizzaPictures[0];
                break;
            case 1:
                pizzaName = "Фантастическая 4-ка";
                pizzaImg.sprite = pizzaPictures[1];
                break;
            case 2:
                pizzaName = "Цезарь";
                pizzaImg.sprite = pizzaPictures[2];
                break;
        }

            winText.text = "Пицца " + $"{pizzaName}" + " готова! ";
            winText.enabled = true;
        
    }
}
