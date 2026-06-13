using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject endgameScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject lostScreen;
    [SerializeField] private GameObject ui;
    [SerializeField] private FadingScreen fadingScreen;
    bool win = true;
    bool gameActive = true;

    public void Win()
    {
        ui.SetActive(false);
        FindAnyObjectByType<CameraMovement>().OnEndGame();
        win = true;
        gameActive = false;
    }

    public void Lost()
    {
        ui.SetActive(false);
        FindAnyObjectByType<CameraMovement>().OnEndGame();
        win = false;
        gameActive = false;
    }

    public void OnCameraDestReach()
    {
        fadingScreen.gameObject.SetActive(true);
        fadingScreen.SetEndEffect(endEffect.SHOW_END_GAME_INFO);
        fadingScreen.StartEffect();
    }

    public void FadeEnd()
    {
        if(win)
        {
            ShowWinScreen();
        }
        else
        {
            ShowLostScreen();
        }
    }

    public void ShowWinScreen()
    {
        gameScreen.SetActive(false);
        endgameScreen.SetActive(true);
        winScreen.SetActive(true);
    }

    public void ShowLostScreen()
    {
        gameScreen.SetActive(false);
        endgameScreen.SetActive(true);
        lostScreen.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }

    public bool isGameActive()
    {
        return gameActive;
    }
}
