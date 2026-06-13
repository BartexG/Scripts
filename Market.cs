using UnityEngine;
using UnityEngine.UI;

class DeliveryRequest
{
    public int quantity;
    public int bounty;
    public int resID; 

    public DeliveryRequest(int minQ,int maxQ,int price,int newResID)
    {
        quantity = Random.Range(minQ, maxQ);
        bounty = quantity * price;
        resID = newResID;
    }
}

public class Market : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Color32[] resColors;
    [SerializeField] private TMPro.TextMeshProUGUI[] deliveryQuantityTexts;
    [SerializeField] private TMPro.TextMeshProUGUI[] deliveryBountyTexts;
    [SerializeField] private Image[] deliveryResImages;
    [SerializeField] private Button[] deliveryButtons;

    private int[] resMinQ = new int[3];
    private int[] resMaxQ = new int[3];

    public int startingPrices = 10;
    private int[] resPrices = new int[3];

    DeliveryRequest[] deliveryRequests = new DeliveryRequest[2];

    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            resMinQ[i]=20;
            resMaxQ[i]=30;
            resPrices[i]=startingPrices;
        }

        for(int i = 0; i < deliveryRequests.Length; i++)
        {
            //2-y,3-r,4-b
            int randomRes = Random.Range(0,3);
            deliveryRequests[i] = new DeliveryRequest(resMinQ[randomRes],resMaxQ[randomRes],resPrices[randomRes],randomRes+2);
        }
    }
    
    public void UpdateUI()
    {
        for(int i = 0; i < deliveryRequests.Length; i++)
        {
            deliveryQuantityTexts[i].text = "[" + deliveryRequests[i].quantity + "]";
            deliveryBountyTexts[i].text = "Reward: " + deliveryRequests[i].bounty + "$";

            deliveryResImages[i*2].color = resColors[deliveryRequests[i].resID-2];
            deliveryResImages[i*2+1].color = resColors[deliveryRequests[i].resID-2];
            deliveryButtons[i].GetComponent<Image>().color = resColors[deliveryRequests[i].resID-2];
        }
    }

    public void Deliver(int id)
    {
        Resources resources = FindAnyObjectByType<Resources>();
        int resID = deliveryRequests[id].resID;
        if(resources.CheckForResource(resID,deliveryRequests[id].quantity))
        {
            resources.ChangeResource(resID,-deliveryRequests[id].quantity);
            resources.ChangeMoney(deliveryRequests[id].bounty);

            resMinQ[resID-2]+=5;
            resMaxQ[resID-2]+=10;
            resPrices[resID-2]+=1;

            int randomRes = Random.Range(0,3);
            deliveryRequests[id] = new DeliveryRequest(resMinQ[randomRes],resMaxQ[randomRes],resPrices[randomRes],randomRes+2);
            UpdateUI();
        }
    }

    public void Open()
    {
        panel.SetActive(true);
        UpdateUI();
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    


}
