using UnityEngine;

public class UnitRecruitButton : MonoBehaviour
{
    [SerializeField] private GameObject targetUnit;

    public void MouseOver()
    {
        FindAnyObjectByType<UnitRecruitPanel>().OpenInfoPanel(targetUnit.GetComponent<Unit>());
    }

    public void Clicked()
    {
        int price = targetUnit.GetComponent<FriendlyUnit>().getPrice();
        if(FindAnyObjectByType<Resources>().getOil() >= price)
        {
            FindAnyObjectByType<Resources>().ChangeResource(1,-price);
            FindAnyObjectByType<BuildingInspectPanel>().getTargetBuilding().GetComponent<Factory>().StartRecruitingUnit(targetUnit);
            FindAnyObjectByType<UnitRecruitPanel>().Close();
        }
    }

    public void MouseExit()
    {
        FindAnyObjectByType<UnitRecruitPanel>().CloseInfoPanel();
    }
}
