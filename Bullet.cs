using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1;

    private int damage;

    private GameObject target;
    private string attackTargetType;

    public void OnSpawn(GameObject newTarget, int newDamage, string targetType)
    {
        target = newTarget;
        damage = newDamage;
        transform.LookAt(target.transform);
        attackTargetType = targetType;
    }

    Vector3 getTargetPos()
    {
        return new Vector3(target.transform.position.x, target.transform.position.y + 0.2f, target.transform.position.z);
    }

    void Update()
    {
        if(target != null) {
            transform.position = Vector3.MoveTowards(transform.position, getTargetPos(), Time.deltaTime*speed);
            transform.LookAt(target.transform);

            if((transform.position - getTargetPos()).sqrMagnitude <= 0.1f)
            {
                if(attackTargetType == "unit")
                {
                    target.GetComponent<Unit>().TakeDamage(damage);
                }
                else
                {
                    target.GetComponent<FinishedBuilding>().TakeDamage(damage);
                }
                
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
