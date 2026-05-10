using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected int maxHp = 100;
    protected int hp;
    [SerializeField] protected HpBar hpBar;

    void Start()
    {
        hp = maxHp;
        hpBar.SetValues(maxHp,hp);
        OnSpawn();
    }

    public virtual void OnSpawn() {}

    public void TakeDamage(int value)
    {
        hp -= value;
        

        if(hp <= 0)
        {
            Death();
        }
        else
        {
            hpBar.SetHp(hp);
        }
    }

    public void Death()
    {
        OnDeath();
        Destroy(gameObject);
    }

    public virtual void OnDeath(){}

}
