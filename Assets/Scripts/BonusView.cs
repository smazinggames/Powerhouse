using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class BonusView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bonusWinText;
    [SerializeField] private TextMeshProUGUI _youWonText;
    [SerializeField] private TextMeshProUGUI _totalScoreText;
    [SerializeField] private TextMeshProUGUI _timerText;

    public const float UpdateDuration = 0.75f;
    private const int _divisions = 10;

    public void SetAward(TextMeshProUGUI awardText, float value) {
        awardText.transform.parent.gameObject.SetActive(true);
        IncrementUpdate(awardText, 0, value);
    }

    private string DollarFormat(float value) {
        if(value == 0)
            return ("$0");
        return string.Format("${0:#.00}", Convert.ToDecimal(value));
    }

    public void UpdateBonus(float value) {
        _bonusWinText.text = DollarFormat(value);
    }

    public void UpdateYouWon(float value) {
        _youWonText.text = DollarFormat(value);
    }

    public void UpdateTotalScore(float value) {
        _totalScoreText.text = DollarFormat(value);
    }

    private void IncrementUpdate(TextMeshProUGUI text, float startValue, float endValue)
    {
        IEnumerator IncrementUpdate()
        {
            float delta = UpdateDuration / _divisions;
            float increment = (endValue - startValue) / _divisions;
            for (int i = 0; i < _divisions; i++)
            {
                yield return new WaitForSeconds(delta);
                startValue += increment;
                text.text = DollarFormat(startValue);
            }
        }
        StartCoroutine(IncrementUpdate());
    }


    public void IncrementUpdateBonus(float startValue, float endValue) {
        IncrementUpdate(_bonusWinText, startValue, endValue);
    }

    public void IncrementUpdateTotalScore(float startValue, float endValue) {
        IncrementUpdate(_totalScoreText, startValue, endValue);
    }

    public void UpdateTimer(int value) {
        _timerText.text = value.ToString();
        _timerText.gameObject.SetActive(true);
    }
    public void HideTimer() {
        _timerText.gameObject.SetActive(false);
    }
}
