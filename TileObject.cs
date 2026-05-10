using UnityEngine;

public class TileObject : MonoBehaviour
{
    [SerializeField] private string objectType;

    public virtual void OnSpawn()
    {
        
    }

    public bool CheckIfObjectOfType(string type)
    {
        return objectType == type;
    }

}
