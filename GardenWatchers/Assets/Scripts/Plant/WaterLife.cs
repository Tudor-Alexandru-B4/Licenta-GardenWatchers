using System.Collections;
using UnityEngine;

public class WaterLife : MonoBehaviour
{
    public float maxWater;
    public float currentWater;
    public float passiveDrainSpeed;
    public float activeDrainSpeed = 0;
    public bool planted = false;

    WaterLevelDisplay levelDisplay = null;

    // Start is called before the first frame update
    void Start()
    {
        currentWater = maxWater;
    }

    // Update is called once per frame
    void Update()
    {
        if (planted)
        {
            currentWater -= (passiveDrainSpeed + activeDrainSpeed) * Time.deltaTime;

            if(levelDisplay == null)
            {
                levelDisplay = GetWaterLevelDisplay();
            }
            else
            {
                levelDisplay.UpdateValue(currentWater, maxWater);
            }

            if (currentWater < 0)
            {
                Destroy(levelDisplay.gameObject);
                Destroy(gameObject);
            }
        }
    }

    public void AddWater(float water)
    {
        if(currentWater + water < maxWater)
        {
            currentWater += water;
        }
        else
        {
            currentWater = maxWater;
        }
    }

    public void AddToActiveDrain(float value)
    {
        activeDrainSpeed += value;
    }

    public void RemoveFromActiveDrain(float value)
    {
        activeDrainSpeed -= value;
    }

    public void AddToActiveDrainTimed(float value, float time)
    {
        StartCoroutine(AddDrainTimed(value, time));
    }

    IEnumerator AddDrainTimed(float value, float time)
    {
        activeDrainSpeed += value;
        yield return new WaitForSeconds(time);
        activeDrainSpeed -= value;
    }

    WaterLevelDisplay GetWaterLevelDisplay()
    {
        foreach(Transform child in transform.parent)
        {
            if (child.name.StartsWith("WaterLevelDisplay"))
            {
                return child.GetComponent<WaterLevelDisplay>();
            }
        }

        return null;
    }
}
