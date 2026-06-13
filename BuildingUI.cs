using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{

    [SerializeField] private GameObject mark;
    [SerializeField] private Image buildingImage;


    FinishedBuilding targetBuilding;

    public void OnSpawn(FinishedBuilding newTargetBuilding)
    {
        targetBuilding = newTargetBuilding;
        buildingImage.sprite = targetBuilding.getBuildingImageSprite();
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
