using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    private GameObject buildingToBuild;
    private bool buildingActive = false;
    private GridGenerator gridGenerator;

    public Material buildingCorrect;
    public Material buildingWrong;


    void Start()
    {
        gridGenerator = FindAnyObjectByType<GridGenerator>();
    }

    public void StartBuilding(FinishedBuilding targetBuilding)
    {
        buildingActive = true;
        buildingToBuild = Instantiate(targetBuilding.getBuildingModel(), transform.position, transform.rotation);
        gridGenerator.SwitchGridVisibility(true);
        SnapToGrid(gridGenerator.getFirstTile().GetComponent<Tile>());
    }

    public void StopBuilding()
    {
        gridGenerator.SwitchGridVisibility(false);
        buildingActive = false;
    }

    public bool isBuildingActive()
    {
        return buildingActive;
    }

    public void SnapToGrid(Tile tile)
    {
        if(buildingToBuild != null)
        {
            buildingToBuild.GetComponent<Building>().MoveToPos(tile);
        }
    }

    public void FinishBuilding(Tile tile)
    {
        Building building = buildingToBuild.GetComponent<Building>();
        GameObject newBuilding = SpawnBuilding(building, tile);
        FindAnyObjectByType<Resources>().ChangeMoney(-newBuilding.GetComponent<FinishedBuilding>().getBuildingCost());
        Destroy(buildingToBuild);
        StopBuilding();
    }

    public GameObject SpawnBuilding(Building building, Tile tile)
    {
        GameObject newBuilding = Instantiate(building.getTargetBuildingModel(), transform.position, buildingToBuild.transform.rotation);
        newBuilding.GetComponent<FinishedBuilding>().OnSpawn(tile);
        gridGenerator.SwitchTilesEmptyState(newBuilding.GetComponent<FinishedBuilding>(), building.getSizeX(), building.getSizeY(), tile.getX(), tile.getY());
        return newBuilding;
    }

    public GameObject SpawnStarterBuilding(Building building, Tile tile, GridGenerator gridGenerator)
    {
        GameObject newBuilding = Instantiate(building.getTargetBuildingModel(), transform.position, transform.rotation);
        newBuilding.GetComponent<FinishedBuilding>().OnSpawn(tile);
        gridGenerator.SwitchTilesEmptyState(newBuilding.GetComponent<FinishedBuilding>(), building.getSizeX(), building.getSizeY(), tile.getX(), tile.getY());
        return newBuilding;
    }

    void Update()
    {
        if(buildingActive && Input.GetMouseButtonDown(1))
        {
            CancelBuilding();
        }

        if(buildingActive && Input.GetKeyDown(KeyCode.R))
        {
            buildingToBuild.transform.Rotate(new Vector3(0,90,0));
        }
    }

    void CancelBuilding()
    {
        StopBuilding();
        Destroy(buildingToBuild);
        buildingToBuild = null;
    }

    public bool CheckIfCanBuild(Tile tile)
    {
        return buildingToBuild.GetComponent<Building>().CheckIfTilesAreEmpty(tile);
    }

    public bool CheckIfCanBuildOnOres()
    {
        if(buildingToBuild == null) return false;

        return buildingToBuild.GetComponent<Building>().canBuildOnOres;
    }

}
