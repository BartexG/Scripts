using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private int updateTime = 3;
    float timer = 0;

    private float[] resPrices = new float[3]; //1.00-20.00
    [SerializeField] private TMPro.TextMeshProUGUI[] priceTexts = new TMPro.TextMeshProUGUI[3];

    [SerializeField] private Color32 upColor;
    [SerializeField] private Color32 downColor;
    [SerializeField] private Color32 neutralColor;


    public void Open()
    {
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= updateTime)
        {
            timer = 0;
            UpdatePrices();
        }
    }

    void UpdatePrices()
    {
        int[] changes = new int[3];

        for(int i = 0; i < resPrices.Length; i++)
        {
            int rnd = Random.Range(1,4);

            if(rnd == 1)
            {
                changes[i] = 0;
            }
            else if(rnd == 2) //up
            {
                resPrices[i] += 0.5f;
                int next = i+1;
                if(next >= resPrices.Length) next = 0;
                resPrices[next] -= 0.5f;

                changes[i] = 1;
                if(changes[next] == 1) changes[next] = 0;
                else changes[next] = -1;
                
                i++;
            }
            else //down
            {
                resPrices[i] -= 0.5f;
                int next = i+1;
                if(next >= resPrices.Length) next = 0;
                resPrices[next] += 0.5f;

                changes[i] = -1;
                if(changes[next] == -1) changes[next] = 0;
                else changes[next] = 1;

                i++;
            }
        }

        for(int i = 0; i < resPrices.Length; i++)
        {
            Color32 newColor = neutralColor;

            switch(changes[i])
            {
                case -1:
                    newColor = downColor;
                break;
                case 0: 
                    newColor = neutralColor;
                break;
                case 1:
                    newColor = upColor;
                break;
            }

            priceTexts[i].color = newColor;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        for(int i = 0; i < resPrices.Length; i++)
        {
            priceTexts[i].text = resPrices[i] + "$";
        }
    }

    void Start()
    {
        for(int i = 0; i < resPrices.Length; i++)
        {
            resPrices[i] = 10;
        }
        UpdateUI();
    }

    public void SellResource(int id)
    {
        FindAnyObjectByType<Resources>().SellResource(id+2, resPrices[id]);
    }


}
