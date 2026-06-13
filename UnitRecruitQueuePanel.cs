using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitRecruitQueuePanel : MonoBehaviour
{
    [SerializeField] private Image[] unitImages;
    public void UpdateQueue(List<GameObject> recruitedUnits)
    {
        for(int i = 0; i < recruitedUnits.Count; i++)
        {
            unitImages[i].gameObject.SetActive(true);
            unitImages[i].sprite = recruitedUnits[i].GetComponent<FriendlyUnit>().getUnitSprite();
        }

        for(int i = recruitedUnits.Count; i < unitImages.Length; i++)
        {
            unitImages[i].gameObject.SetActive(false);
        }
    }
}
