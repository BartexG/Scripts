using UnityEngine;

public class Ore : TileObject
{
    public int oreID;

    private bool mined = false;

    public override void OnSpawn()
    {
        mined = false;
        if(oreID == 1)
        {
            objectType = "oil";
        }
        else 
        {
            objectType = "ore";
        }
    }

    public void ExtractOre(int value)
    {
        if(FindAnyObjectByType<GameManage>().isGameActive()) 
        {
            FindAnyObjectByType<Resources>().ChangeResource(oreID,value);
        }
    }

    public void ChangeMinedStatus(bool value)
    {
        mined = value;
    }

    public int getOreID()
    {
        return oreID;
    }

    public bool isMined()
    {
        return mined;
    }

}
