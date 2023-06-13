using UnityEngine;

public class Bucket_Script : MonoBehaviour
{
    public float waterValue;
    public bool isEmpty = false;
    public GameObject fullBucketModel;
    public GameObject emptyBucketModel;
    
    public void EmptyBucket()
    {
        emptyBucketModel.SetActive(true);
        fullBucketModel.SetActive(false);
        isEmpty = true;
    }

    public void RefillBucket()
    {
        fullBucketModel.SetActive(true);
        emptyBucketModel.SetActive(false);
        isEmpty = false;
    }
}
