using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Transform gridSpawn;
    [SerializeField] private GameObject tile;
    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeY;
    [SerializeField] private float gridSpace = 1;

    private List<GameObject> gridTiles;

    [Header("Ores")]
    [SerializeField] private GameObject[] ores; //red,yellow,blue,oil
    [SerializeField] private int[] orePosX;
    [SerializeField] private int[] orePosY;
    [SerializeField] private int distinctOreCount = 2;

    [SerializeField] private int[] oilPosX;
    [SerializeField] private int[] oilPosY;

    [SerializeField] private Building hub;
    [SerializeField] private Building teleporter;

    private GameObject spawnedHub;

    private List<GameObject> spawnedOres;

    void Start()
    {
        GenerateGrid();
        SwitchGridVisibility(false);
        SpawnOres();
        spawnedHub = FindAnyObjectByType<BuildingSystem>().SpawnStarterBuilding(hub, getTile(40,40), this);
        GameObject spawnedTeleport = FindAnyObjectByType<BuildingSystem>().SpawnStarterBuilding(teleporter, getTile(38,34), this);
        spawnedTeleport.transform.Rotate(new Vector3(0,180,0));
    }

    public GameObject getSpawnedHub()
    {
        return spawnedHub;
    }

    void GenerateGrid()
    {
        gridTiles = new List<GameObject>();

        for(int y = 0; y < gridSizeY; y++)
        {
            for(int x = 0; x < gridSizeX; x++)
            {
                Vector3 spawnPos = new Vector3(gridSpawn.position.x + x * gridSpace, gridSpawn.position.y, gridSpawn.position.z + y * gridSpace);
                GameObject newTile = Instantiate(tile, spawnPos, gridSpawn.rotation);
                newTile.transform.SetParent(gridSpawn);
                newTile.GetComponent<Tile>().OnSpawn(x,y);
                gridTiles.Add(newTile);
            }
        }
    }

    public GameObject getFirstTile()
    {
        return gridTiles[0];
    }

    public bool CheckIfInBounds(int sizeX, int sizeY, int posX, int posY)
    {
        if(posX - sizeX + 1 < 0) return false;
        if(posY - sizeY + 1 < 0) return false;

        return true;
    }

    public Tile getTile(int x, int y)
    {
        return gridTiles[y * gridSizeX + x].GetComponent<Tile>();
    }

    public void SwitchTilesEmptyState(FinishedBuilding targetBuilding, int sizeX, int sizeY, int posX, int posY)
    {
        for(int x = 0; x < sizeX; x++)
        {
            for(int y = 0; y < sizeY; y++)
            {
                getTile(posX - x, posY - y).SwitchEmptyState(targetBuilding);
            }
        }
    }

    public bool CheckIfTilesAreEmpty(int sizeX, int sizeY, int posX, int posY)
    {
        for(int x = 0; x < sizeX; x++)
        {
            for(int y = 0; y < sizeY; y++)
            {
                if(!getTile(posX - x, posY - y).canBuildOn()) return false;
            }
        }

        return true;
    }

    public void SwitchGridVisibility(bool value)
    {
        for(int i = 0; i < gridTiles.Count; i++)
        {
            if(!value)
            {
                gridTiles[i].GetComponent<Tile>().MakeInvisible();
            }
            else
            {
                gridTiles[i].GetComponent<Tile>().UpdateMaterial();
            }
        }
    }

    public void SpawnOres()
    {
        spawnedOres = new List<GameObject>();

        List<int> xPositions = new List<int>();
        List<int> yPositions = new List<int>();

        for(int i = 0; i < orePosX.Length; i++)
        {
            xPositions.Add(orePosX[i]);
            yPositions.Add(orePosY[i]);
        }

        for(int i = 0; i < distinctOreCount*3; i++)
        {
            int a = i % 3;
            int rndPos = Random.Range(0, xPositions.Count);

            GameObject spawnedOre = GenerateOre(xPositions[rndPos],yPositions[rndPos],ores[a]);
            xPositions.RemoveAt(rndPos);
            yPositions.RemoveAt(rndPos);
            if(spawnedOre != null) spawnedOres.Add(spawnedOre);
        }

        for(int i = 0; i < oilPosX.Length; i++)
        {
            GameObject newOilDepostit = GenerateOre(oilPosX[i], oilPosY[i], ores[3]);
            if(newOilDepostit != null) spawnedOres.Add(newOilDepostit);
        }
    }

    GameObject GenerateOre(int x, int y, GameObject ore)
    {
        if(getTile(x,y).getTileObject() == null)
        {
            return SpawnObject(ore, getTile(x, y),true);
        }
        return null;
    }

    public GameObject SpawnObject(GameObject targetObject, Tile tile, bool ore = false)
    {
        GameObject newObject = Instantiate(targetObject, tile.transform.position, tile.transform.rotation);
        tile.SetTileObject(newObject);
        
        if(ore) {
            newObject.transform.Rotate(new Vector3(-90,0,0));
            newObject.transform.position += new Vector3(0,0.05f,0);
        }

        newObject.GetComponent<TileObject>().OnSpawn();
        
        return newObject;
    }
}
