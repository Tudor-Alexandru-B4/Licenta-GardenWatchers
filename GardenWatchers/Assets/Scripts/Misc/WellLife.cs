using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WellLife : MonoBehaviour
{
    public float maxWater;
    public float currentWater;
    public float activeDrainSpeed = 0;
    Slider slider;

    void Start()
    {
        currentWater = maxWater;
        slider = GameObject.Find("WellHealth").GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = currentWater;
        currentWater -= activeDrainSpeed * Time.deltaTime;

        if(currentWater < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddWater(float water)
    {
        if (currentWater + water < maxWater)
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
}
