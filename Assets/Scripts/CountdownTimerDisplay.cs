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
        GameManager.Instance.onGameCountdown += UpdateDisplay;
        GameManager.Instance.onGameStartCountdown += ShowCounter;
        GameManager.Instance.onGameStart += StartGoUi;
    }
    void OnDisable()
    {
        GameManager.Instance.onGameCountdown -= UpdateDisplay;
        GameManager.Instance.onGameStartCountdown -= ShowCounter;
        GameManager.Instance.onGameStart -= StartGoUi;
    }
    void UpdateDisplay(float time)
    {
        if (time > 0) textToUpdate.text = time.ToString(timeFormat);
        else textToUpdate.text = "GO!";
    }
    void StartGoUi()
    {
        StartCoroutine(FlashThenHideUi(0.2f,5));
    }
    IEnumerator FlashThenHideUi(float flashintervel, int times = 3)
    {
        for(int i = 0; i < times; i++)
        {
            yield return WaitForTimes.GetWaitForTime(flashintervel);
            HideCounter();
            for(int j = 0; j < 5; j++)
                yield return null;
            ShowCounter();
        }
        HideCounter();
    }
    void ShowCounter()
    {
        textToUpdate.gameObject.SetActive(true);
    }
    void HideCounter()
    {
        textToUpdate.gameObject.SetActive(false);
    }
}
