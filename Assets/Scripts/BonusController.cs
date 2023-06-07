using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusController : MonoBehaviour
{
    [SerializeField] private BonusView _view;
    [SerializeField] private Modal _modal;
    [SerializeField] private Crab[] _crabs;
    [SerializeField] private Animator _transition;
    [SerializeField] private Animator _girl;
    [SerializeField] private GameObject _youWonSign;

    private int _index;
    private int _timer;
    private Coroutine _timerCoroutine;
    private List<Crab> _unselected;
    private IWinSequence _winSequence;

    private void Start() {
        IWinSequence win = _modal.GetWinSequence();
        RunBonusGame(win);
    }

    public void RunBonusGame(IWinSequence sequence) {
        _winSequence = sequence;
        _modal.BonusWin = 0;
        _view.UpdateBonus(0);
        _view.UpdateTotalScore(_modal.TotalScore);
        _unselected = _crabs.OfType<Crab>().ToList();
        ShowCrabs();
    }

    private void Timer() {
        IEnumerator Timer()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                _timer++;
                if (_timer == 20)
                {
                    SelectRandomCrab();
                }
                else if (_timer >= 10)
                {
                    _view.UpdateTimer(20 - _timer);
                }
            }
        }
        _timerCoroutine = StartCoroutine(Timer());
    }
    private void ResetTimer() {
        _timer = 0;
        _view.HideTimer();
    }
    private void StopTimer() {
        StopCoroutine(_timerCoroutine);
    }

    void ShowCrabs() {
        IEnumerator ShowCrabs()
        {
            yield return new WaitForSeconds(0.4f);
            foreach (Crab crab in _crabs)
            {
                yield return new WaitForSeconds(0.1f);
                crab.Appear();
            }
            EnableCrabs(true);
            Timer();
        }
        StartCoroutine(ShowCrabs());
    }

    public void SelectCrab(Crab crab) {
        IEnumerator SelectCrab()
        {
            ResetTimer();
            float[] sequence = _winSequence.GetSequence();
            float award = sequence[_index];
            _view.SetAward(crab.AwardText, award);
            crab.Hide();
            EnableCrabs(false);
            _unselected.Remove(crab);

            float final = _modal.BonusWin + award;
            _view.IncrementUpdateBonus(_modal.BonusWin, final);
            _modal.BonusWin = final;

            float total = _modal.TotalScore + award;
            _view.IncrementUpdateTotalScore(_modal.TotalScore, total);
            _modal.TotalScore = total;

            _index++;
            if (_index == sequence.Length)
            {
                StopTimer();
                EnableCrabs(false);
                yield return new WaitForSeconds(2f);
                EndGame();
            }
            else
            {
                yield return new WaitForSeconds(BonusView.UpdateDuration);
                EnableCrabs(true);
            }
        }
        StartCoroutine(SelectCrab());
    }

    private void EnableCrabs(bool value) {
        foreach(Crab crab in _unselected) {
            crab.Collider.enabled = value;
        }
    }

    private void SelectRandomCrab() {
        SelectCrab(_unselected[Random.Range(0, _unselected.Count)]);
    }

    private void EndGame()
    {
        IEnumerator EndGame()
        {
            _girl.SetTrigger("Kiss");
            yield return new WaitForSeconds(1f);
            _transition.SetTrigger("Close");
            yield return new WaitForSeconds(1f);
            _view.UpdateYouWon(_modal.BonusWin);
            _youWonSign.SetActive(true);
        }
        StartCoroutine(EndGame());
    }
    public void ReturnToMenu(float delay) {
        IEnumerator ReturnToMenu()
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene("Menu");
        }
        StartCoroutine(ReturnToMenu());
    }

    private void OnApplicationQuit()
    {
        _modal.Clear();
    }
}
