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
    [SerializeField] private GameObject[] ores; //red,yellow,blue
    [SerializeField] private int[] orePosX;
    [SerializeField] private int[] orePosY;


    [SerializeField] private Transform obstaclesSpawn;

    [SerializeField] private Building hub;

    private GameObject spawnedHub;

    void Start()
    {
        GenerateGrid();
        SwitchGridVisibility(false);
        SpawnOres();

        spawnedHub = FindAnyObjectByType<BuildingSystem>().SpawnStarterBuilding(hub, getTile(25,25), this);
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

            //gridTiles[i].SetActive(value);
            //gridTiles[i].GetComponent<Tile>().UpdateMaterial();
        }
    }

    public void SpawnOres()
    {
        for(int i = 0; i < orePosX.Length; i++)
        {
            int a = i % 3;
            //SpawnObject(ores[a], getTile(orePosX[a], orePosY[a]));
            GenerateOre(orePosX[a],orePosY[a],ores[a],3);
        }
    }

    void GenerateOre(int x, int y, GameObject ore, int maxRange)
    {
        if(getTile(x,y).getTileObject() == null)
        {
            SpawnObject(ore, getTile(x, y));
        }

        if(maxRange > 1)
        {
            if(x - 1 >= 0)
            {
                if(RandomizeOre()) GenerateOre(x-1,y,ore,maxRange-1);
            }

            if(x + 1 < gridSizeX)
            {
                if(RandomizeOre()) GenerateOre(x+1,y,ore,maxRange-1);
            }

            if(y - 1 >= 0)
            {
                if(RandomizeOre()) GenerateOre(x,y-1,ore,maxRange-1);
            }

            if(y + 1 < gridSizeY)
            {
                if(RandomizeOre()) GenerateOre(x,y+1,ore,maxRange-1);
            }
        }
    }

    bool RandomizeOre()
    {
        int rnd = Random.Range(1,101);
        if(rnd <= 50) return true;
        return false;
    }

    public void SpawnObject(GameObject targetObject, Tile tile)
    {
        GameObject newObject = Instantiate(targetObject, tile.transform.position, tile.transform.rotation);
        //newObject.transform.SetParent(tile.transform);
        tile.SetTileObject(targetObject);
    }

    public void UpdateGridWithObstacles()
    {
        Obstacle[] obstacles = obstaclesSpawn.GetComponentsInChildren<Obstacle>();

        //for(int i = 0; i < obstacles.Length; i++)
        //{
            
        //}
    }



}
