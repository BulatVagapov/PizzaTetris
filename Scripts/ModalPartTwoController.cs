using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalPartTwoController : MonoBehaviour
{
    [SerializeField] GameObject[] pizzasBlocks;
    
    
    public void SetModalPartTwo(int pizzaIndex)
    {
        switch(pizzaIndex)
        {
            case 0:
                pizzasBlocks[0].SetActive(true);
                pizzasBlocks[1].SetActive(false);
                pizzasBlocks[2].SetActive(false);
                break;
            case 1:
                pizzasBlocks[1].SetActive(true);
                pizzasBlocks[0].SetActive(false);
                pizzasBlocks[2].SetActive(false);
                break;
            case 2:
                pizzasBlocks[2].SetActive(true);
                pizzasBlocks[0].SetActive(false);
                pizzasBlocks[1].SetActive(false);
                break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
