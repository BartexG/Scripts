using UnityEngine;
using UnityEngine.UI;

public class UnitRecruitButton : MonoBehaviour
{
    [SerializeField] private Image unitImage;
    [SerializeField] private GameObject mark;

    private GameObject targetUnit;

    public void SetUnit(GameObject unit)
    {
        targetUnit = unit;
        unitImage.sprite = targetUnit.GetComponent<FriendlyUnit>().getUnitSprite();
        mark.SetActive(false);
    }

    public void MouseOver()
    {
        FindAnyObjectByType<UnitRecruitPanel>().OpenInfoPanel(targetUnit.GetComponent<Unit>());
        mark.SetActive(true);
    }

    public void Clicked()
    {
        int price = targetUnit.GetComponent<FriendlyUnit>().getPrice();
        if(FindAnyObjectByType<Resources>().getOil() >= price)
        {
            if(targetUnit.GetComponent<FriendlyUnit>().getUnitType() == FriendlyUnit.friendlyUnitType.MINING)
            {
                if(!FindAnyObjectByType<UnitSelectionManager>().canRecruitMiningUnit()) return;
            }
            else
            {
                if(!FindAnyObjectByType<UnitSelectionManager>().canRecruitMilitaryUnit()) return;
            }

            FindAnyObjectByType<Resources>().ChangeResource(1,-price);
            FindAnyObjectByType<BuildingInspectPanel>().getTargetBuilding().GetComponent<Factory>().StartRecruitingUnit(targetUnit);

            if(!Input.GetKey(KeyCode.LeftControl)) FindAnyObjectByType<UnitRecruitPanel>().Close();
        }
    }

    public void MouseExit()
    {
        FindAnyObjectByType<UnitRecruitPanel>().CloseInfoPanel();
        mark.SetActive(false);
    }
}
