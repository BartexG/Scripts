using UnityEngine;
using UnityEngine.UI;

public class BuildingInspectPanel : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private Image buildingImage;
    [SerializeField] private TMPro.TextMeshProUGUI hpText;
    [SerializeField] private Slider hpBar;

    [SerializeField] private GameObject panel;

    [SerializeField] private GameObject demolishButton;
    [SerializeField] private GameObject buildUnitsButton;

    FinishedBuilding targetBuilding;

    [SerializeField] private Slider progressBar;

    [SerializeField] private UnitRecruitQueuePanel unitRecruitQueuePanel;

    private bool opened = false;
    private bool miningActive = false;

    private bool isFactory = false;

    public void OpenPanel(FinishedBuilding fb)
    {
        targetBuilding = fb;
        panel.SetActive(true);

        nameText.text = fb.getName();
        hpText.text = fb.getHp() + "/" + fb.getMaxHp();
        hpBar.maxValue = fb.getMaxHp();
        hpBar.value = fb.getHp();

        buildingImage.sprite = fb.getBuildingImageSprite();

        demolishButton.SetActive(fb.isDemolishable());
        opened = true;

        if(targetBuilding.CheckBuildingType("mine"))
        {
            progressBar.gameObject.SetActive(true);
            progressBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Mining...";
            miningActive = true;
            isFactory = false;
        }
        else
        {
            isFactory = targetBuilding.CheckBuildingType("factory");
            progressBar.gameObject.SetActive(false);
            miningActive = false;
        }

        if(isFactory)
        {
            buildUnitsButton.SetActive(true);
            unitRecruitQueuePanel.gameObject.SetActive(true);
        }
        else
        {
            buildUnitsButton.SetActive(false);
            unitRecruitQueuePanel.gameObject.SetActive(false);
        }

        FindAnyObjectByType<UnitRecruitPanel>().Close();
    }

    public void ClosePanel()
    {
        FindAnyObjectByType<UnitRecruitPanel>().Close();
        panel.SetActive(false);
        opened = false;
    }

    public void DemolishBuilding()
    {
        targetBuilding.DestroyBuilding();
        ClosePanel();
    }

    void Update()
    {
        if(opened)
        {
            hpBar.value = targetBuilding.getHp();
            hpText.text = targetBuilding.getHp() + "/" + targetBuilding.getMaxHp();

            if(hpBar.value == 0)
            {
                ClosePanel();
            }

            if(miningActive) 
            {
                progressBar.maxValue = targetBuilding.GetComponent<Mine>().getMiningSpeed();
                progressBar.value = targetBuilding.GetComponent<Mine>().getTimeLeft();
            }

            if(isFactory)
            {
                if(targetBuilding.GetComponent<Factory>().isRecruiting())
                {
                    progressBar.gameObject.SetActive(true);
                    progressBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Assembling...";
                    progressBar.maxValue = targetBuilding.GetComponent<Factory>().getAssemblySpeed();
                    progressBar.value = targetBuilding.GetComponent<Factory>().getTimeLeft();
                }
                else
                {
                    progressBar.gameObject.SetActive(false);
                }
                unitRecruitQueuePanel.UpdateQueue(targetBuilding.GetComponent<Factory>().getRecruitedUnits());
            }

        }
    }

    public FinishedBuilding getTargetBuilding()
    {
        return targetBuilding;
    }
}
