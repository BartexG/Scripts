using UnityEngine;

public class BuildingUI : MonoBehaviour
{

    [SerializeField] private GameObject mark;

    FinishedBuilding targetBuilding;

    public void OnSpawn(FinishedBuilding newTargetBuilding)
    {
        targetBuilding = newTargetBuilding;
    }

    public void Clicked()
    {
        if(FindAnyObjectByType<Resources>().getMoney() >= targetBuilding.getBuildingCost())
        {
            mark.SetActive(false);
            FindAnyObjectByType<BuildingSystem>().StartBuilding(targetBuilding);
        }
    }

    public void MouseOver()
    {
        mark.SetActive(true);
        FindAnyObjectByType<BuildMenu>().ShowBuildingInfo(targetBuilding);
    }

    public void MouseExit()
    {
        mark.SetActive(false);
        FindAnyObjectByType<BuildMenu>().HideBuildingInfo();
    }

}
