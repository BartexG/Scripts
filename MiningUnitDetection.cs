using UnityEngine;

public class MiningUnitDetection : MonoBehaviour
{
    private MiningUnit miningUnit;

    void Start()
    {
        miningUnit = gameObject.GetComponentInParent<MiningUnit>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ore")
        {
            miningUnit.OnOreTouch(other.gameObject);
        }
        else if(other.tag == "Ally Building")
        {
            if(other.GetComponent<FinishedBuilding>().CheckBuildingType("hub")) 
            {
                miningUnit.OnHubTouch();
            }
        }
    }
}
