using UnityEngine;
using UnityEngine.UI;

public enum endEffect {START_GAME,EXIT_GAME,SHOW_END_GAME_INFO};

public class FadingScreen : MonoBehaviour
{
    public endEffect onEndEffect;

    float timer;
    bool effectActive;

    byte a = 0;
    const int d = 8;

    public void StartEffect()
    {
        a = 0;
        effectActive = true;
        timer = 0;
    }

    void Update()
    {
        if(effectActive)
        {
            timer += Time.deltaTime;

            if(timer >= 0.04f)
            {
                timer = 0;
                gameObject.GetComponent<Image>().color = new Color32(0,0,0,a);
                
                int tmp = a;
                if(tmp + d >= 255)
                {
                    a=255;
                    gameObject.GetComponent<Image>().color = new Color32(0,0,0,255);
                    effectActive = false;
                    ExecuteEndEffect();
                }
                else
                {
                    a+=d;
                }    
            }
        }
    }

    public void ExecuteEndEffect()
    {
        switch(onEndEffect)
        {
            case endEffect.START_GAME:
                FindAnyObjectByType<MainMenu>().StartGame();
            break;
            case endEffect.EXIT_GAME:
                FindAnyObjectByType<MainMenu>().ExitGame();
            break;
            case endEffect.SHOW_END_GAME_INFO:
                FindAnyObjectByType<GameManage>().FadeEnd();
            break;
        }
    }

    public void SetEndEffect(endEffect newEndEffect)
    {
        onEndEffect = newEndEffect;
    }
}
