using System.Collections.Generic;
using UnityEngine;

public class AlarmBeacon : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material alarmActiveMaterial;
    [SerializeField] private Material alarmActiveMaterial2;

    private bool active = false;
    private MeshRenderer meshRenderer;
    float timer;
    bool even = false;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    public bool isActive()
    {
        return active;
    }

    public void SwitchActiveState(bool value)
    {
        active = value;
        timer = 0;
        even = false;
        
        if(active)
        {
            SwitchMaterial(alarmActiveMaterial);
        }
        else
        {
            SwitchMaterial(defaultMaterial);
        }
    }

    void Update()
    {
        if(isActive())
        {
            timer += Time.deltaTime;

            if(timer >= 0.5f)
            {
                timer = 0;

                if(even)
                {
                    SwitchMaterial(alarmActiveMaterial2);
                }
                else
                {
                    SwitchMaterial(alarmActiveMaterial);
                }
                even = !even;
            }
        }
    }

    public void SwitchMaterial(Material newMaterial)
    {
        List<Material> newMaterials = new List<Material>();
        newMaterials.Add(meshRenderer.materials[0]);
        newMaterials.Add(newMaterial);

        meshRenderer.SetMaterials(newMaterials);
    }
}
