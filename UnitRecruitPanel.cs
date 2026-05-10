using UnityEngine;

public class UnitRecruitPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField] private TMPro.TextMeshProUGUI priceText;

    public void Open()
    {
        if(!FindAnyObjectByType<BuildingInspectPanel>().getTargetBuilding().GetComponent<Factory>().isRecruiting()) 
        {
            panel.SetActive(true);
            CloseInfoPanel();
        }
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    public void OpenInfoPanel(Unit targetUnit)
    {
        infoPanel.SetActive(true);
        nameText.text = targetUnit.getUnitName();
        descriptionText.text = targetUnit.getDescription();
        priceText.text = targetUnit.getPrice().ToString();
    }

    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
    }
}
