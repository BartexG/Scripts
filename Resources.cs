using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{

    [SerializeField] private int starterMoney = 1000;
    int money = 0;
    [SerializeField] private TMPro.TextMeshProUGUI moneyText;

    [SerializeField] private int starterOil = 100;
    int oil = 0;
    [SerializeField] private TMPro.TextMeshProUGUI oilText;

    int yellowOre = 0;
    [SerializeField] private TMPro.TextMeshProUGUI yellowOreText;
    int redOre = 0;
    [SerializeField] private TMPro.TextMeshProUGUI redOreText;
    int blueOre = 0;
    [SerializeField] private TMPro.TextMeshProUGUI blueOreText;

    [SerializeField] private int resourceLimit = 100;

    void Start()
    {
        money = starterMoney;
        oil = starterOil;
        redOre = 0;
        yellowOre = 0;
        blueOre = 0;

        UpdateUI();
    }

    public void UpdateUI()
    {
        moneyText.text = money + "$";
        oilText.text = oil + "/\n" + resourceLimit;
        redOreText.text = redOre + "/\n" + resourceLimit;
        yellowOreText.text = yellowOre + "/\n" + resourceLimit;
        blueOreText.text = blueOre + "/\n" + resourceLimit;
    }

    public int getMoney()
    {
        return money;
    }

    public void ChangeMoney(int value)
    {
        money += value;
        UpdateUI();
    }

    //Zmienić resID na enum
    public void ChangeResource(int resID, int value)
    {
        switch(resID)
        {
            case 1: //oil
                oil += value;
                if(oil > resourceLimit) oil = resourceLimit;
            break; 
            case 2: //yellow
                yellowOre += value;
                if(yellowOre > resourceLimit) yellowOre = resourceLimit;
            break;
            case 3: //red
                redOre += value;
                if(redOre > resourceLimit) redOre = resourceLimit;
            break;
            case 4: //blue
                blueOre += value;
                if(blueOre > resourceLimit) blueOre = resourceLimit;
            break;
        }

        UpdateUI();
    }

    public bool CheckForResource(int resID, int value)
    {
        switch(resID)
        {
            case 1:
                return oil >= value;
            case 2:
                return yellowOre >= value;
            case 3:
                return redOre >= value;
            case 4:
                return blueOre >= value;
        }
        return false;
    }

    public int getOil()
    {
        return oil;
    }

    public void SellResource(int resID, float price)
    {
        float sellValue = 0;
        int count = 0;

        switch(resID)
        {
            case 2: //yellow
                sellValue = yellowOre * price;
                count = yellowOre;
            break;
            case 3: //red
                sellValue = redOre * price;
                count = redOre;
            break;
            case 4: //blue
                sellValue = blueOre * price;
                count = blueOre;
            break;
        }

        sellValue = Mathf.RoundToInt(sellValue);

        if(sellValue > 0)
        {
            ChangeMoney((int)sellValue);
            ChangeResource(resID,-count);
        }

        
    }

}
