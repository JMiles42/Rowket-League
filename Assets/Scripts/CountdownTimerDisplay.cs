using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class CountdownTimerDisplay : JMilesBehaviour
{
    public Text textToUpdate;
    public string timeFormat;

    void OnEnable()
    {
        GameManager.Instance.onGameCountdown += UpdateDisplay;
        GameManager.Instance.onGameStartCountdown += ShowCounter;
    }

    void OnDisable()
    {
        if (!GameManager.Instance) return;
        GameManager.Instance.onGameCountdown -= UpdateDisplay;
        GameManager.Instance.onGameStartCountdown -= ShowCounter;
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

    IEnumerator FlashThenHideUi(float flashInterval, int times = 3)
    {
        for (int i = 0; i < times; i++)
        {
            var fade1 = textToUpdate.DOFade(0, flashInterval);
            yield return fade1.WaitForCompletion();
            textToUpdate.DOFade(1, flashInterval/2);
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