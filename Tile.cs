using UnityEngine;

public class Tile : MonoBehaviour
{
    private int x;
    private int y;

    private BuildingSystem buildingSystem;

    private GameObject tileObject;
    private FinishedBuilding targetBuilding;

    [SerializeField] private Material correct;
    [SerializeField] private Material wrong;
    [SerializeField] private Material invisible;

    public void OnSpawn(int newX, int newY)
    {
        x = newX;
        y = newY;
        targetBuilding = null;
        tileObject = null;
        buildingSystem = FindAnyObjectByType<BuildingSystem>();
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public bool canBuildOn()
    {
        if(targetBuilding != null) return false;

        if(tileObject != null)
        {
            if(buildingSystem.CheckIfCanBuildOnOres() && tileObject.GetComponent<TileObject>().CheckIfObjectOfType("oil")) return true;
            else return false;
        }
        else if(buildingSystem.CheckIfCanBuildOnOres()) return false;

        return true;
    }

    public void SwitchEmptyState(FinishedBuilding finishedBuilding)
    {
        targetBuilding = finishedBuilding;
    }

    public void SetTileObject(GameObject newObject)
    {
        tileObject = newObject;
    }

    void OnMouseOver()
    {
        if(buildingSystem.isBuildingActive())
        {
            buildingSystem.SnapToGrid(this);

            if(Input.GetMouseButtonDown(0) && canBuildOn())
            {
                if(buildingSystem.CheckIfCanBuild(this)) 
                {
                    buildingSystem.FinishBuilding(this);
                }
            }
        }
        else if(Input.GetMouseButtonDown(0) && targetBuilding != null)
        {
            FindAnyObjectByType<BuildingInspectPanel>().OpenPanel(targetBuilding);
        }
    }

    public void UpdateMaterial()
    {
        if(canBuildOn())
        {
            gameObject.GetComponent<MeshRenderer>().material = correct;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = wrong;
        }
    }

    public void MakeInvisible()
    {
        gameObject.GetComponent<MeshRenderer>().material = invisible;
    }

    public GameObject getTileObject()
    {
        return tileObject;
    }

    public Ore getOre()
    {
        if(tileObject != null)
        {
            string type = tileObject.GetComponent<TileObject>().getObjectType();
            if(type == "ore" || type == "oil")
            {
                return tileObject.GetComponent<Ore>();
            }
        }

        return null;
    }
}
