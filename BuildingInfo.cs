using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    
    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private TMPro.TextMeshProUGUI costText;
    [SerializeField] private TMPro.TextMeshProUGUI maxHpText;

    [SerializeField] private TMPro.TextMeshProUGUI descriptionText;

    [SerializeField] private GameObject panel;

    public void Show(FinishedBuilding fb)
    {
        panel.SetActive(true);

        nameText.text = fb.getName();
        costText.text = "Cost: " + fb.getBuildingCost() + "$";
        maxHpText.text = "Hit points: " + fb.getMaxHp();
        descriptionText.text = fb.getDescription();
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

}
