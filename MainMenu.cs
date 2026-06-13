using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public FadingScreen fadingScreen;

    public void OnStartButtonPressed()
    {
        fadingScreen.SetEndEffect(endEffect.START_GAME);
        fadingScreen.StartEffect();
    }

    public void OnExitButtonPressed()
    {
        fadingScreen.SetEndEffect(endEffect.EXIT_GAME);
        fadingScreen.StartEffect();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
