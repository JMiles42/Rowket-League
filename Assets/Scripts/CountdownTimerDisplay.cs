using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class CountdownTimerDisplay : JMilesBehaviour
{
    public Text textToUpdate;
    public string timeFormat;

    private void OnEnable()
    {
        GameManager.Instance.onGameCountdown += UpdateDisplay;
        GameManager.Instance.onGameStartCountdown += ShowCounter;
    }

    private void OnDisable()
    {
#if !UNITY_EDITOR
        GameManager.Instance.onGameCountdown -= UpdateDisplay;
        GameManager.Instance.onGameStartCountdown -= ShowCounter;
    #endif
    }

    private void UpdateDisplay(float time)
    {
        ShowCounter();
        if (time > 0) textToUpdate.text = time.ToString(timeFormat);
        else
        {
            textToUpdate.text = "GO!";
            StartGoUi();
        }
    }

    private void StartGoUi()
    {
        StartCoroutine(FlashThenHideUi(0.2f, 5));
    }

    private IEnumerator FlashThenHideUi(float flashInterval, int times = 3)
    {
        //Fade and show the counter UI
        for (int i = 0; i < times; i++)
        {
            var fade1 = textToUpdate.DOFade(0, flashInterval);
            yield return fade1.WaitForCompletion();
            textToUpdate.DOFade(1, flashInterval/2);
        }
        HideCounter();
    }

    private void ShowCounter()
    {
        textToUpdate.enabled = true;
    }

    private void HideCounter()
    {
        textToUpdate.enabled = false;
    }
}