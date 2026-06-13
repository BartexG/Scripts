using UnityEngine;

public class ErrorInfoText : MonoBehaviour
{
    
    [SerializeField] TMPro.TextMeshPro infoText;
    [SerializeField] GameObject textObject;
    float timer = 0;
    bool active = false;
    Vector3 targetPos;

    public void ShowError(string text, Vector3 pos)
    {
        textObject.transform.position = pos;
        targetPos = textObject.transform.position + new Vector3(0,2,0);
        textObject.SetActive(true);
        infoText.text = text;
        active = true;
        timer = 0;
    }

    void Update()
    {
        if(active)
        {
            timer += Time.deltaTime;

            if(timer >= 0.02f)
            {
                textObject.transform.position += new Vector3(0,0.05f,0);
                timer = 0;
                if(textObject.transform.position.y >= targetPos.y)
                {
                    active = false;
                    textObject.SetActive(false);
                }
            }
        }
    }

}
