using UnityEngine;
using UnityEngine.AI;

public class EnemyUnit : Unit
{
    public bool active = true;
    NavMeshAgent agent;

    Vector3 mainBasePosition;

    public override void OnSpawn()
    {
        base.OnSpawn();
        agent = GetComponent<NavMeshAgent>();
        if(active)
        {
            mainBasePosition = FindAnyObjectByType<GridGenerator>().getSpawnedHub().transform.position;
            agent.SetDestination(mainBasePosition);
        }        
    }

    public void Stop()
    {
        if(agent.velocity.magnitude > 0)
        {
            agent.SetDestination(transform.position + transform.forward);
        }
    }

    public void ContinueMovement()
    {
        if(agent.velocity.magnitude == 0) 
        {
            agent.SetDestination(mainBasePosition);
        }
    }

    public override void OnDeath()
    {
        FindAnyObjectByType<EnemySpawner>().ChangeSpawnedEnemies(-1);
    }
}
