using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] int sizeX;
    [SerializeField] int sizeZ;

    public int getSizeX()
    {
        return sizeX;
    }

    public int getSizeZ()
    {
        return sizeZ;
    }
}
