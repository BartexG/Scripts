using UnityEngine;
using System;

public class MiningUnit : FriendlyUnit
{
    
    [SerializeField] private float miningSpeed = 2;
    [SerializeField] private int miningStorage = 5;
    [SerializeField] private ParticleSystem sparksEffect;
    GameObject targetOre = null;
    int minedResourceID = -1;
    bool mining = false;
    float timer = 0;

    public void SetMiningTarget(GameObject target)
    {
        CancelMining();

        targetOre = target;
        
        if(targetOre != null)
        {
            targetOre.GetComponent<Ore>().ChangeMinedStatus(true);
            agent.SetDestination(targetOre.transform.position);
        }
    }

    void Update()
    {
        if(mining)
        {
            timer += Time.deltaTime;
            if(timer >= miningSpeed)
            {
                timer = 0;
                if(FindAnyObjectByType<GameManage>().isGameActive()) agent.SetDestination(FindAnyObjectByType<GridGenerator>().getSpawnedHub().transform.position);
                minedResourceID = targetOre.GetComponent<Ore>().getOreID();
                mining = false;
                sparksEffect.Stop();
            }
        }
    }

    public void OnHubTouch()
    {
        if(!FindAnyObjectByType<GameManage>().isGameActive()) return;

        if(minedResourceID != -1) 
        {
            FindAnyObjectByType<Resources>().ChangeResource(minedResourceID,miningStorage);
            minedResourceID = -1;
            if(targetOre != null)
            {
                agent.SetDestination(targetOre.transform.position);
            }
        }
    }

    public void OnOreTouch(GameObject touchedOre)
    {
        if(targetOre != null && touchedOre.Equals(targetOre)) 
        {
            agent.SetDestination(transform.position + transform.forward*0.2f);
            transform.LookAt(targetOre.transform);
            mining = true;
            timer = 0;
            sparksEffect.Play();
        }
    }

    public string getMiningUnitDescription()
    {
        return "\nMining speed: " + Math.Round(1/miningSpeed,2) + "/s" +
        "\n" + "Mining power: " + miningStorage;
    }

    public void CancelMining()
    {
        if(targetOre != null)
        {
            timer = 0;
            mining = false;
            sparksEffect.Stop();
            targetOre.GetComponent<Ore>().ChangeMinedStatus(false);
            targetOre = null;
        }
    }
}
