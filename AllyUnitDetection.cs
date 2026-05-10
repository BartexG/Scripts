using UnityEngine;

public class AllyUnitDetection : MonoBehaviour
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
            if(other.tag == "Enemy")
            {
                uas.SetAttackTarget(other.gameObject,"unit");
            }
        }

    }
}
