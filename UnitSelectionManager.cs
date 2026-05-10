using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    private List<GameObject> selectedUnits = new List<GameObject>();
    public List<GameObject> allUnits = new List<GameObject>();

    private Camera cam;
    [SerializeField] private LayerMask clickable;
    [SerializeField] private LayerMask ground;


    void Start()
    {
        selectedUnits = new List<GameObject>();
        cam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
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
    }

    public void RemoveUnit(GameObject unit)
    {
        if(selectedUnits.Contains(unit)) selectedUnits.Remove(unit);

        allUnits.Remove(unit);
    }

    public List<GameObject> getAllUnits()
    {
        return allUnits;
    }
}
