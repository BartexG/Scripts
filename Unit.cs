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

        if(gameObject.GetComponent<FriendlyUnit>().getUnitType() == FriendlyUnit.friendlyUnitType.MILITARY) 
        {
            UnitAttackSystem uas = gameObject.GetComponent<UnitAttackSystem>();
            desc += uas.getDescription();
        }
        else if(gameObject.GetComponent<FriendlyUnit>().getUnitType() == FriendlyUnit.friendlyUnitType.MINING)
        {
            desc += gameObject.GetComponent<MiningUnit>().getMiningUnitDescription();
        }

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
