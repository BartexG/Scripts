using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private string unitName; 
    [SerializeField] protected int maxHp = 100;
    protected int hp;
    [SerializeField] protected HpBar hpBar;

    public virtual void OnSpawn()
    {
        hp = maxHp;
        hpBar.SetValues(maxHp,hp);
    }

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

    public string getDescription()
    {
        string desc = "Hp: " + maxHp;

        UnitAttackSystem uas = gameObject.GetComponent<UnitAttackSystem>();

        desc += uas.getDescription();

        return desc;
    }

    public string getUnitName()
    {
        return unitName;
    }

    public virtual int getPrice()
    {
        return 0;
    }



}
