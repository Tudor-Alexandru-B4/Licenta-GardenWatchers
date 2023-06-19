using System.Collections.Generic;
using UnityEngine;

public class SeedManager : MonoBehaviour
{
    public int numberOfBuckets = 2;
    public float min = 9;
    public float max = 22;

    public float currentDropChance = 0;

    float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        maxDistance = max - min;
    }

    // Update is called once per frame
    void Update()
    {
        int numberOfSeeds = new List<GameObject>(GameObject.FindGameObjectsWithTag("PickUp")).Count - numberOfBuckets;
        int numberOfPlants = new List<GameObject>(GameObject.FindGameObjectsWithTag("Plant")).Count;
        int currentValue = numberOfSeeds + numberOfPlants;

        if(currentValue <= min)
        {
            currentDropChance = 100;
            return;
        }

        if(currentValue >= max)
        {
            currentDropChance = 0;
            return;
        }

        float distance = currentValue - min;
        float newValue = distance * 100 / min;
        currentDropChance = 100 - newValue;
    }
}
