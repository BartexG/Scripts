using UnityEngine;

//Skrypt wykrywający jednostki i budynki zbudowane przez gracza
public class EnemyUnitDetection : MonoBehaviour
{
    private UnitAttackSystem uas;

    void Start()
    {
        uas = gameObject.GetComponentInParent<UnitAttackSystem>();
    }

    void OnTriggerStay(Collider other)
    {
        if(uas.getAttackTarget() == null) 
        {
            if(other.tag == "Ally")
            {
                uas.GetComponent<EnemyUnit>().Stop();
                uas.SetAttackTarget(other.gameObject,"unit");
            }
            else if(other.tag == "Ally Building")
            {
                uas.GetComponent<EnemyUnit>().Stop();
                uas.SetAttackTarget(other.gameObject,"building");
            }
        }

    }
}
