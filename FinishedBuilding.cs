using UnityEngine;

public class FinishedBuilding : MonoBehaviour
{
    
    [Header("Stats")]
    [SerializeField] private string buildingName;
    [SerializeField] [TextArea] private string buildingDescription;
    [SerializeField] private int buildingCost = 1000;
    [SerializeField] private int maxHp = 100;
    [SerializeField] private bool canBeDemolished = true;
    [SerializeField] private string buildingType;
    [SerializeField] private Sprite buildingImageSprite;
    int hp = 0;

    [Header("Model")]
    [SerializeField] private GameObject buildingModel;

    private Tile mainTile = null;

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FindAnyObjectByType<BuildingInspectPanel>().OpenPanel(this);
        }
    }

    public void OnBuild()
    {
        hp = maxHp;
    }

    public int getHp()
    {
        return hp;
    }

    public int getMaxHp()
    {
        return maxHp;
    }

    public int getBuildingCost()
    {
        return buildingCost;
    }

    public string getDescription()
    {
        return buildingDescription;
    }

    public string getName()
    {
        return buildingName;
    }

    public bool isDemolishable()
    {
        return canBeDemolished;
    }

    public bool CheckBuildingType(string type)
    {
        return buildingType == type;
    }

    public void MoveToPos(Tile tile)
    {
        Building building = buildingModel.GetComponent<Building>();
        Vector3 snapVector = building.getSnapVector();

        float newX = tile.transform.position.x + snapVector.x;
        float newY = tile.transform.position.y + building.getYMod();
        float newZ = tile.transform.position.z + snapVector.z; 
                
        Vector3 newPos = new Vector3(newX,newY,newZ);
        transform.position = newPos;
    }

    public GameObject getBuildingModel()
    {
        return buildingModel;
    }

    public void OnSpawn(Tile tile)
    {
        mainTile = tile;
        hp = maxHp;
        MoveToPos(tile);
        AfterSpawn(tile);
    }

    protected virtual void AfterSpawn(Tile tile)
    {
        
    }

    public void DestroyBuilding()
    {
        Building building = getBuildingModel().GetComponent<Building>();
        GridGenerator gg = FindAnyObjectByType<GridGenerator>();
        
        for(int x = 0; x < building.getSizeX(); x++)
        {
            for(int y = 0; y < building.getSizeY(); y++)
            {
                gg.getTile(mainTile.getX() - x, mainTile.getY() - y).SwitchEmptyState(null);
            }
        }

        if(buildingType == "hub")
        {
            FindAnyObjectByType<GameManage>().Lost();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int value)
    {
        hp -= value;

        if(hp <= 0)
        {
            DestroyBuilding();
        }
    }

    public float getAvgRadius()
    {
        return buildingModel.GetComponent<Building>().getAvgRadius();    
    }

    public Sprite getBuildingImageSprite()
    {
        return buildingImageSprite;
    }


}
