using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Color32 highColor;
    [SerializeField] Color32 mediumColor;
    [SerializeField] Color32 lowColor;

    [SerializeField] Slider bar;
    [SerializeField] Image fillImage;
    [SerializeField] GameObject canvas;

    void Update()
    {
        canvas.transform.LookAt(Camera.main.transform);
    }

    public void SetValues(int maxHp, int hp)
    {
        bar.maxValue = maxHp;
        SetHp(hp);
    }

    public void SetHp(int hp)
    {
        bar.value = hp;

        float fillPercent = bar.value*100/bar.maxValue;

        if(fillPercent >= 70)
        {
            fillImage.color = highColor;
        }
        else if(fillPercent>=30)
        {
            fillImage.color = mediumColor;
        }
        else
        {
            fillImage.color = lowColor;
        }
    }
}
