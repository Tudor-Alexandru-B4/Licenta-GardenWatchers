using UnityEngine;

public class Bucket_Script : MonoBehaviour
{
    public float waterValue;
    public bool isEmpty = false;
    public Color emptyColor;
    public Color fullColor;
    
    public void EmptyBucket()
    {
        gameObject.GetComponent<Renderer>().material.color = emptyColor;
        isEmpty = true;
    }

    public void RefillBucket()
    {
        gameObject.GetComponent<Renderer>().material.color = fullColor;
        isEmpty = false;
    }
}
