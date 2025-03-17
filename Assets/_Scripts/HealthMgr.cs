using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthMgr : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerHealth playerHealth;
    List<HealthHeart> hearts = new List<HealthHeart>();

    public void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();
        //determine how many hearts to draw based on max hp
        float maxHealthReamainder = playerHealth.maxHealth % 2;
        int heartsToMake = (int)((playerHealth.maxHealth / 2) + maxHealthReamainder);

        for(int i =0; i<heartsToMake; i++)
        {
            CreateEmptyHeart(); //make hearts needed
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int hearStatusRemainder = Mathf.Clamp(playerHealth.playerHealth - (i * 2), 0, 2);
            hearts[i].setHeartImage((HeartStatus)hearStatusRemainder);
        }

    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.setHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    } 

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);

        }
        hearts = new List<HealthHeart>();
    }
}
