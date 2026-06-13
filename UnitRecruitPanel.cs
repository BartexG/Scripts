using UnityEngine;

public class UnitRecruitPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField] private TMPro.TextMeshProUGUI priceText;
    [SerializeField] private UnitRecruitButton[] unitRecruitButtons;

    public void Open()
    {
        Factory targetFactory = FindAnyObjectByType<BuildingInspectPanel>().getTargetBuilding().GetComponent<Factory>();
        panel.SetActive(true);
        CloseInfoPanel();

        for(int i = 0; i < targetFactory.getAvailableUnits().Length; i++)
        {
            unitRecruitButtons[i].gameObject.SetActive(true);
            unitRecruitButtons[i].SetUnit(targetFactory.getAvailableUnits()[i]);
        }

        for(int i = targetFactory.getAvailableUnits().Length; i < unitRecruitButtons.Length; i++)
        {
            unitRecruitButtons[i].gameObject.SetActive(false);
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
