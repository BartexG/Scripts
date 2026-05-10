using UnityEngine;
using UnityEngine.AI;

public class UnitAttackSystem : MonoBehaviour
{
    GameObject attackTarget = null;
    string attackTargetType;
    [SerializeField] private float attackCooldown = 1;
    [SerializeField] private int damage = 50;
    [SerializeField] private float attackRange = 10;

    private bool onCooldown = false;
    private float timer = 0;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootingPoint;

    [SerializeField] private bool enemy = false;

    private NavMeshAgent agent;

    private bool following = false;

    private float threshold = 0.98f;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        following = false;
    }

    void Update()
    {

        if(attackTarget != null) 
        {
            float mod = 0;

            if(enemy && attackTargetType == "building")
            {
                mod += attackTarget.GetComponent<FinishedBuilding>().getAvgRadius();
            }

            if(following)
            {
                if(Vector3.Distance(transform.position,attackTarget.transform.position) <= attackRange + mod)
                {
                    //Stop
                    agent.SetDestination(transform.position + transform.forward);
                    following = false;
                }
            }

            if(Vector3.Distance(transform.position,attackTarget.transform.position) > attackRange + mod)
            {
                agent.SetDestination(attackTarget.transform.position);
                following = true;
            }
            else 
            {
                float dot = Vector3.Dot(transform.forward, (attackTarget.transform.position - transform.position).normalized);

                if(dot >= threshold)
                {
                    if(!onCooldown)
                    {
                        ShootTarget();
                    }
                }
                else
                {
                    RotateTowardsTarget();
                }

                if(onCooldown) 
                {
                    timer += Time.deltaTime;
                    if(timer >= attackCooldown)
                    {
                        timer = 0;
                        onCooldown = false;
                    }
                }
            }
        }
        else
        {
            if(enemy)
            {
                gameObject.GetComponent<EnemyUnit>().ContinueMovement();
            }
        }
    }

    public void RotateTowardsTarget()
    {
        Vector3 targetDirection = attackTarget.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = 3 * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void ShootTarget()
    {
        GameObject newBullet = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        newBullet.GetComponent<Bullet>().OnSpawn(attackTarget,damage,attackTargetType);
        onCooldown = true;
    }

    public void SetAttackTarget(GameObject newTarget, string targetType)
    {
        attackTarget = newTarget;
        attackTargetType = targetType;
    }

    public GameObject getAttackTarget()
    {
        return attackTarget;
    }
}
