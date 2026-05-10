using UnityEngine;

public class Ore : TileObject
{
    public int oreID;
    int oreCount;

    public override void OnSpawn()
    {
        oreCount = Random.Range(500,901);
    }

    public void ExtractOre(int value)
    {
        FindAnyObjectByType<Resources>().ChangeResource(oreID,value);
        oreCount -= value;

        if(oreCount <= 0)
        {
            //...
        }
    }

}
