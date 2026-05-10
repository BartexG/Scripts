using UnityEngine;
using UnityEngine.AI;

public class FriendlyUnit : Unit
{
    NavMeshAgent agent;

    [SerializeField] private GameObject selectionMark;

    [SerializeField] private bool militaryUnit = false;

    [SerializeField] private int price = 10;

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
        if(militaryUnit)
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
        agent.SetDestination(transform.position + transform.forward);
    }

    public void SetDestination(Vector3 point)
    {
        agent.SetDestination(point);
        if(militaryUnit)
        {
            gameObject.GetComponent<UnitAttackSystem>().SetAttackTarget(null, "");
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
}
