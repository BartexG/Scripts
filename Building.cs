using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{    
    [Header("Model")]
    [SerializeField] private float yMod = 1f;
    [SerializeField] private int sizeX = 1;
    [SerializeField] private int sizeY = 1;
    [SerializeField] private bool simple = false;
    [SerializeField] private GameObject targetBuildingModel;

    public bool canBuildOnOres = false;

    private int collidingObjects = 0;

    private float gridSize = 1f;

    void Start()
    {
        collidingObjects = 0;
    }

    public float getYMod()
    {
        return yMod;
    }

    //Zwraca wektor będący różnicą punktu zaczepienia(lewa górna kratka) i środka modelu
    public Vector3 getSnapVector()
    {
        return new Vector3(-(sizeX-1)*gridSize/2,0,-(sizeY-1)*gridSize/2);
    }

    public void MoveToPos(Tile tile)
    {
        if(FindAnyObjectByType<GridGenerator>().CheckIfInBounds(sizeX,sizeY, tile.getX(), tile.getY())) 
        {
            Vector3 snapVector = getSnapVector();

            float newX = tile.transform.position.x + snapVector.x;
            float newY = tile.transform.position.y + getYMod();
            float newZ = tile.transform.position.z + snapVector.z; 
                
            Vector3 newPos = new Vector3(newX,newY,newZ);
            transform.position = newPos; 

            //Check if can build
            UpdateMaterial(tile);
        }
    }

    public bool CheckIfTilesAreEmpty(Tile tile)
    {
        return FindAnyObjectByType<GridGenerator>().CheckIfTilesAreEmpty(getSizeX(), getSizeY(), tile.getX(), tile.getY());
    }

    public void UpdateMaterial(Tile tile)
    {
        if(collidingObjects == 0) {
            if(CheckIfTilesAreEmpty(tile))
            {
                ChangeMaterial(FindAnyObjectByType<BuildingSystem>().buildingCorrect);
            }
            else
            {
                ChangeMaterial(FindAnyObjectByType<BuildingSystem>().buildingWrong);
            }
        }
    }

    public void ChangeMaterial(Material targetMaterial)
    {
        Material[] materials = null;
        if(simple) materials = gameObject.GetComponentInChildren<MeshRenderer>().materials;
        else materials =  gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials;

        List<Material> newMaterials = new List<Material>();

        for(int i = 0; i < materials.Length; i++)
        {
            newMaterials.Add(targetMaterial);
        }

        if(simple) gameObject.GetComponentInChildren<MeshRenderer>().SetMaterials(newMaterials);
        else gameObject.GetComponentInChildren<SkinnedMeshRenderer>().SetMaterials(newMaterials);
        
    }

    public int getSizeX()
    {
        return sizeX;
    }

    public int getSizeY()
    {
        return sizeY;
    }

    public GameObject getTargetBuildingModel()
    {
        return targetBuildingModel;   
    }

    public float getAvgRadius()
    {
        if(sizeX > sizeY)
        {
            return 1.5f*sizeX;
        }
        else
        {
            return 1.5f*sizeY;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ally" || other.tag == "Obstacle")
        {
            collidingObjects++;
            ChangeMaterial(FindAnyObjectByType<BuildingSystem>().buildingWrong);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ally" || other.tag == "Obstacle")
        {
            collidingObjects--;
            if(collidingObjects <= 0)
            {
                collidingObjects = 0;
                ChangeMaterial(FindAnyObjectByType<BuildingSystem>().buildingWrong);
            }
        }
    }


    public bool isBuildingLocked()
    {
        return collidingObjects > 0;
    }

}
