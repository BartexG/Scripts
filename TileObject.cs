using UnityEngine;

public class TileObject : MonoBehaviour
{
    protected string objectType;

    public virtual void OnSpawn()
    {
        
    }

    public bool CheckIfObjectOfType(string type)
    {
        return objectType == type;
    }

    public string getObjectType()
    {
        return objectType;
    }

}
