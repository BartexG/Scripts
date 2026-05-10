using UnityEngine;
using UnityEngine.UI;

public class BuildingInspectPanel : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private TMPro.TextMeshProUGUI hpText;
    [SerializeField] private Slider hpBar;

    [SerializeField] private GameObject panel;

    [SerializeField] private GameObject demolishButton;
    [SerializeField] private GameObject buildUnitsButton;

    FinishedBuilding targetBuilding;

    [SerializeField] private Slider miningBar;

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

        demolishButton.SetActive(fb.isDemolishable());
        opened = true;

        if(targetBuilding.CheckBuildingType("mine"))
        {
            miningBar.gameObject.SetActive(true);
            miningBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Mining...";
            miningActive = true;
            isFactory = false;
        }
        else
        {
            isFactory = targetBuilding.CheckBuildingType("factory");

            if(isFactory)
            {
                buildUnitsButton.SetActive(true);
            }
            else
            {
                buildUnitsButton.SetActive(false);
            }

            miningBar.gameObject.SetActive(false);
            miningActive = false;
        }
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
                miningBar.maxValue = targetBuilding.GetComponent<Mine>().getMiningSpeed();
                miningBar.value = targetBuilding.GetComponent<Mine>().getTimeLeft();
            }

            if(isFactory)
            {
                if(targetBuilding.GetComponent<Factory>().isRecruiting())
                {
                    miningBar.gameObject.SetActive(true);
                    miningBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Assembling...";
                    miningBar.maxValue = targetBuilding.GetComponent<Factory>().getAssemblySpeed();
                    miningBar.value = targetBuilding.GetComponent<Factory>().getTimeLeft();
                }
                else
                {
                    miningBar.gameObject.SetActive(false);
                }
            }

        }
    }

    public FinishedBuilding getTargetBuilding()
    {
        return targetBuilding;
    }
}
