using System.Collections.Generic;
using UnityEngine;

public class Mine : FinishedBuilding
{
    [SerializeField] private int minedResources = 1;
    [SerializeField] private float miningSpeed = 2f;

    private List<Ore> minedOres;
    private float timer = 0;

    protected override void AfterSpawn(Tile tile)
    {
        minedOres = new List<Ore>();
        Building building = getBuildingModel().GetComponent<Building>();
        GridGenerator gg = FindAnyObjectByType<GridGenerator>();
        
        for(int x = 0; x < building.getSizeX(); x++)
        {
            for(int y = 0; y < building.getSizeY(); y++)
            {
                Ore ore = gg.getTile(tile.getX() - x, tile.getY() - y).getOre();
                if(ore != null) minedOres.Add(ore);
            }
        }
    }

    void Update()
    {
        if(minedOres.Count > 0)
        {
            timer += Time.deltaTime;

            if(timer >= miningSpeed)
            {
                timer = 0;
                minedOres[0].ExtractOre(minedResources);
            }
        }
    }

    public float getTimeLeft()
    {
        return timer;
    }

    public float getMiningSpeed()
    {
        return miningSpeed;
    }
}
