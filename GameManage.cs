using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject endgameScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject lostScreen;

    public void Win()
    {
        gameScreen.SetActive(false);
        endgameScreen.SetActive(true);
        winScreen.SetActive(true);
    }

    public void Lost()
    {
        gameScreen.SetActive(false);
        endgameScreen.SetActive(true);
        lostScreen.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
