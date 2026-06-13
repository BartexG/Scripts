using UnityEngine;
using UnityEngine.AI;

public class FriendlyUnit : Unit
{
    public enum friendlyUnitType {MINING, MILITARY}; 

    protected NavMeshAgent agent;

    [SerializeField] private GameObject selectionMark;

    [SerializeField] private friendlyUnitType unitType = friendlyUnitType.MILITARY;

    [SerializeField] private int price = 10;

    [SerializeField] private Sprite unitSprite;

    bool unitSelected = false;

    [SerializeField] private bool hardSpawn = false;

    void Start()
    {
        if(hardSpawn)
        {
            OnSpawn();
        }
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        agent = GetComponent<NavMeshAgent>();
        ChangeUnitSelect(false);
        FindAnyObjectByType<UnitSelectionManager>().AddUnit(gameObject);
    }

    public void ChangeUnitSelect(bool value)
    {
        unitSelected = value;
        selectionMark.SetActive(value);
        hpBar.gameObject.SetActive(value);
    }

    public void SetAttackTarget(GameObject target)
    {
        if(unitType == friendlyUnitType.MILITARY)
        {
            gameObject.GetComponent<UnitAttackSystem>().SetAttackTarget(target, "unit");
            Stop();
        }
    }

    public void Stop()
    {
        if(agent.velocity.magnitude > 0)
        {
            agent.SetDestination(transform.position + transform.forward);
        }
    }

    public void MoveForward(float multiplier)
    {
        agent.SetDestination(transform.position + transform.forward*multiplier);
    }

    public void SetDestination(Vector3 point)
    {
        agent.SetDestination(point);
        if(unitType == friendlyUnitType.MILITARY)
        {
            gameObject.GetComponent<UnitAttackSystem>().SetAttackTarget(null, "");
        }
        else if(unitType == friendlyUnitType.MINING)
        {
            gameObject.GetComponent<MiningUnit>().CancelMining();
        }
    }

    public override void OnDeath()
    {
        FindAnyObjectByType<UnitSelectionManager>().RemoveUnit(gameObject);
    }

    public override int getPrice()
    {
        return price;
    }

    public friendlyUnitType getUnitType()
    {
        return unitType;
    }

    public Sprite getUnitSprite()
    {
        return unitSprite;
    }
}
