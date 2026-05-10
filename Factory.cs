using UnityEngine;

public class Factory : FinishedBuilding
{
    [SerializeField] private float assemblySpeed = 2f;

    private float timer = 0;

    private GameObject recruitedUnit;

    [SerializeField] private Transform newUnitSpawn;

    protected override void AfterSpawn(Tile tile)
    {
        recruitedUnit = null;
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
        recruitedUnit = newUnit;
        timer = 0;
    }

    public void RecruitUnit()
    {
        GameObject newUnit = Instantiate(recruitedUnit, newUnitSpawn.position, newUnitSpawn.rotation);
        newUnit.GetComponent<FriendlyUnit>().OnSpawn();
        newUnit.GetComponent<FriendlyUnit>().MoveForward(1);
        recruitedUnit = null;
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
        return recruitedUnit != null;
    }
}
