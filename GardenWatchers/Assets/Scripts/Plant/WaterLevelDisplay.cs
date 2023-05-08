using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelDisplay : MonoBehaviour
{
    public float min = 9;
    public float max = 22;
    public int multiplier = -1;
    public float valueDa;
    
    float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        maxDistance = max - min;
        transform.LookAt(Camera.main.transform);
        Quaternion rotation = transform.rotation;
        transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, 0f, 0f);
        transform.Rotate(0f, 146f, 0f);
        transform.localPosition = new Vector3(1, 1, 1);
    }

    public void UpdateValue(float currentValue, float maxValue)
    {
        float percent = (100 * currentValue) / maxValue;
        float newValue = ((percent / 100) * maxDistance + min) * multiplier;
        gameObject.GetComponent<Renderer>().material.SetFloat("_waterLevel", newValue);
        valueDa = newValue;
    }
}
