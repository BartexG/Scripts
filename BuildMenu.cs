using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private BuildingInfo buildingInfo;
    [SerializeField] private GameObject buildingUI;
    [SerializeField] private Transform buildingUISpawn;

    private List<GameObject> spawnedBuildingButtons;

    [SerializeField] private List<FinishedBuilding> unlockedBuildings;

    public void Open()
    {
        panel.SetActive(true);
        buildingInfo.Hide();

        spawnedBuildingButtons = new List<GameObject>();
        for(int i = 0; i < unlockedBuildings.Count; i++)
        {
            GameObject newBuildingUI = Instantiate(buildingUI, buildingUISpawn.position + new Vector3(i*200,0), buildingUISpawn.rotation);
            newBuildingUI.transform.SetParent(buildingUISpawn);
            newBuildingUI.GetComponent<BuildingUI>().OnSpawn(unlockedBuildings[i]);

            spawnedBuildingButtons.Add(newBuildingUI);
        }
    }
    
    public void Close()
    {
        for(int i = spawnedBuildingButtons.Count-1; i >= 0; i--)
        {
            if(spawnedBuildingButtons[i] != null) Destroy(spawnedBuildingButtons[i]);
            spawnedBuildingButtons.RemoveAt(i);
        }

        panel.SetActive(false);
        buildingInfo.Hide();
    }

    public void ShowBuildingInfo(FinishedBuilding finishedBuilding)
    {
        buildingInfo.Show(finishedBuilding);
    }

    public void HideBuildingInfo()
    {
        buildingInfo.Hide();
    }
}
