using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttemptController : MonoBehaviour
{
    public Image[] attemptsImgs;
    [SerializeField] Sprite attemptImg;
    [SerializeField] Sprite usedAttemptImg;

    public void AttemptsImgsController(int attempt)
    {
        int difference = attemptsImgs.Length - attempt;

        for(int i = 0; i < difference; i++)
        {
            attemptsImgs[i].sprite = usedAttemptImg;
        }

        for(int i = difference; i < attemptsImgs.Length; i++)
        {
            attemptsImgs[i].sprite = attemptImg;
        }
    }
}
