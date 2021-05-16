using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public int maxFish = 3;
    public float chanceToSpawn = 0.5f;
    public bool forceSpawnAll = false;
    private GameObject[] fish;
    private void Awake()
    {
        fish = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            fish[i] = transform.GetChild(i).gameObject;
        }
        OnDisable();
    }
    private void OnEnable()
    {
        if (Random.Range(0.0f, 1.0f) > chanceToSpawn)
            return;
        if (forceSpawnAll)
        {
            for (int i = 0; i < maxFish; i++)
            {
                if (fish[i] != null)
                    fish[i].SetActive(true);
            }
        }
        else
        {
            int r = Random.Range(0, maxFish);
            try
            {
                for (int i = 0; i <= r; i++)
                {
                    if (fish[i] != null)
                        fish[i].SetActive(true);
                }
            }
            catch
            {
                for (int i = 0; i < r; i++)
                {
                    if (fish[i] != null)
                        fish[i].SetActive(true);
                }
            }
        }
    }
    private void OnDisable()
    {
        foreach (GameObject go in fish)
            if (go!=null)
            go.SetActive(false);
    }
}
