using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    private List<GameObject> selectedUnits = new List<GameObject>();
    public List<GameObject> allUnits = new List<GameObject>();

    private Camera cam;
    [SerializeField] private LayerMask clickable;
    [SerializeField] private LayerMask ground;

    public int miningUnitsLimit = 3;
    private int miningUnits;
    public int militaryUnitsLimit = 5;
    private int militaryUnits;

    [SerializeField] private TMPro.TextMeshProUGUI miningUnitLimitText;
    [SerializeField] private TMPro.TextMeshProUGUI militaryUnitLimitText;

    void Start()
    {
        miningUnits = 0;
        militaryUnits = 0;
        selectedUnits = new List<GameObject>();
        cam = Camera.main;
        UpdateUnitLimitTexts();
    }

    public void UpdateUnitLimitTexts()
    { 
        miningUnitLimitText.text = "Mining drones: " + miningUnits + "/" + miningUnitsLimit;
        militaryUnitLimitText.text = "Tanks: " + militaryUnits + "/" + militaryUnitsLimit;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !FindAnyObjectByType<BuildingSystem>().isBuildingActive())
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickable) && hit.transform.tag == "Ally")
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }
            }
            else
            {
                DeselectAll();
            }
        }

        else if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickable) && hit.transform.tag == "Enemy")
            {
                SetTargetSelectedUnits(hit.collider.gameObject);
            }
            else if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickable) && hit.transform.tag == "Ore")
            {
                if(selectedUnits.Count == 1 && !hit.transform.gameObject.GetComponent<Ore>().isMined()) 
                {
                    SetTargetOreForMiningUnits(hit.collider.gameObject);
                }
                else
                {
                    FindAnyObjectByType<ErrorInfoText>().ShowError("Max one unit per crystal!",hit.transform.position);
                }
            }
            else if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                SetDestinationSelectedUnits(hit.point);
            }
        }
    }

    public void DeselectAll()
    {
        SwitchSelectedUnitsMovement(false);
        selectedUnits.Clear();
    }

    public void MultiSelect(GameObject clickedObject)
    {
        selectedUnits.Add(clickedObject);
        SwitchSelectedUnitsMovement(true);
    }

    public void DragSelect(GameObject unit)
    {
        if(!selectedUnits.Contains(unit))
        {
            selectedUnits.Add(unit);
            SwitchSelectedUnitsMovement(true);
        }
    }

    public void SelectByClicking(GameObject clickedObject)
    {
        DeselectAll();

        selectedUnits.Add(clickedObject);
        SwitchSelectedUnitsMovement(true);
    }

    public void SwitchSelectedUnitsMovement(bool value)
    {
        for(int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].GetComponent<FriendlyUnit>().ChangeUnitSelect(value);
        }
    }

    public void SetTargetOreForMiningUnits(GameObject target)
    {
        for(int i = 0; i < selectedUnits.Count; i++)
        {
            if(selectedUnits[i].GetComponent<FriendlyUnit>().getUnitType() == FriendlyUnit.friendlyUnitType.MINING) 
            {
                selectedUnits[i].GetComponent<MiningUnit>().SetMiningTarget(target);
            }
        }
    }

    public void SetTargetSelectedUnits(GameObject target)
    {
        for(int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].GetComponent<FriendlyUnit>().SetAttackTarget(target);
        }
    }

    public void SetDestinationSelectedUnits(Vector3 point)
    {
        for(int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].GetComponent<FriendlyUnit>().SetDestination(point);
        }
    }

    public void AddUnit(GameObject unit)
    {
        allUnits.Add(unit);
        if(unit.GetComponent<FriendlyUnit>().getUnitType() == FriendlyUnit.friendlyUnitType.MILITARY)
        {
            militaryUnits++;
        }
        else
        {
            miningUnits++;
        }
        UpdateUnitLimitTexts();
    }

    public void RemoveUnit(GameObject unit)
    {
        if(selectedUnits.Contains(unit)) selectedUnits.Remove(unit);

        allUnits.Remove(unit);

        if(unit.GetComponent<FriendlyUnit>().getUnitType() == FriendlyUnit.friendlyUnitType.MILITARY)
        {
            militaryUnits--;
        }
        else
        {
            miningUnits--;
        }
        UpdateUnitLimitTexts();
    }

    public List<GameObject> getAllUnits()
    {
        return allUnits;
    }

    public bool canRecruitMiningUnit()
    {
        return miningUnits < miningUnitsLimit;
    }

    public bool canRecruitMilitaryUnit()
    {
        return militaryUnits < militaryUnitsLimit;
    }
}
