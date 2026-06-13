using System.Collections.Generic;
using UnityEngine;

public class Factory : FinishedBuilding
{
    [SerializeField] private float assemblySpeed = 2f;

    private float timer = 0;

    private List<GameObject> recruitedUnits;
    const int MAX_QUEUE_SIZE = 4;
    
    [SerializeField] private Transform newUnitSpawn;

    [SerializeField] private GameObject[] availableUnits;

    protected override void AfterSpawn(Tile tile)
    {
        recruitedUnits = new List<GameObject>();
        timer = 0;
    }

    void Update()
    {
        if(isRecruiting())
        {
            timer += Time.deltaTime;

            if(timer >= assemblySpeed)
            {
                timer = 0;
                RecruitUnit();
            }
        }
    }

    public void StartRecruitingUnit(GameObject newUnit)
    {
        if(recruitedUnits.Count < MAX_QUEUE_SIZE) 
        {
            if(!isRecruiting())timer = 0;

            recruitedUnits.Add(newUnit);
        }
    }

    public void RecruitUnit()
    {
        GameObject newUnit = Instantiate(recruitedUnits[0], newUnitSpawn.position, newUnitSpawn.rotation);
        newUnit.GetComponent<FriendlyUnit>().OnSpawn();
        newUnit.GetComponent<FriendlyUnit>().MoveForward(1);
        recruitedUnits.RemoveAt(0);
    }

    public float getTimeLeft()
    {
        return timer;
    }

    public float getAssemblySpeed()
    {
        return assemblySpeed;
    }

    public bool isRecruiting()
    {
        return recruitedUnits.Count > 0;
    }

    public GameObject[] getAvailableUnits()
    {
        return availableUnits;
    }

    public List<GameObject> getRecruitedUnits()
    {
        return recruitedUnits;
    }
}
