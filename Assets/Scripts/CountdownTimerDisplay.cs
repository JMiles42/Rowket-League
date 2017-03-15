using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimerDisplay : JMilesBehaviour
{
    public Text textToUpdate;
    public string timeFormat;

    void OnEnable()
    {

        if (GameManager.Instance)
        {
            GameManager.Instance.onGameCountdown += UpdateDisplay;
            GameManager.Instance.onGameStartCountdown += ShowCounter;
        }
    }

    void OnDisable()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.onGameCountdown -= UpdateDisplay;
            GameManager.Instance.onGameStartCountdown -= ShowCounter;
        }
    }

    void UpdateDisplay(float time)
    {
        ShowCounter();
        if (time > 0) textToUpdate.text = time.ToString(timeFormat);
        else
        {
            textToUpdate.text = "GO!";
            StartGoUi();
        }
    }

    void StartGoUi()
    {
        StartCoroutine(FlashThenHideUi(0.2f, 5));
    }

    IEnumerator FlashThenHideUi(float flashintervel, int times = 3)
    {
        for (int i = 0; i < times; i++)
        {
            yield return WaitForTimes.GetWaitForTime(flashintervel);
            HideCounter();
            for (int j = 0; j < 5; j++)
                yield return null;
            ShowCounter();
        }
        HideCounter();
    }

    void ShowCounter()
    {
        textToUpdate.enabled = true;
    }

    void HideCounter()
    {
        textToUpdate.enabled = false;
    }
}