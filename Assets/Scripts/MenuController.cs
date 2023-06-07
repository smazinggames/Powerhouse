using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Modal _modal;

    public void SelectSequence(int index) {
        _modal.SetWinSequence(index);
        SceneManager.LoadScene("Bonus");
    }

    private void OnApplicationQuit()
    {
        _modal.Clear();
    }
}
