using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watering_Script : MonoBehaviour
{
    private GameObject plant;
    private GameObject well;
    private Action_Script playerAction;
    private Bucket_Script bucket;

    // Start is called before the first frame update
    void Start()
    {
        playerAction = gameObject.transform.parent.GetComponent<Action_Script>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Plant")
        {
            plant = other.gameObject;
            
        }else if (other.gameObject.tag == "Well")
        {
            well = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FarmLand")
        {
            plant = null;
            
        }else if (other.gameObject.tag == "Well")
        {
            well = null;
        }
    }

    public bool TryToWater()
    {
        if(!IsBucketAvailable())
        {
            return false;
        }

        if(bucket.isEmpty)
        {
            if(well != null)
            {
                bucket.RefillBucket();
                return true;
            }
            return false;
        }

        if(plant == null)
        {
            return false;
        }

        plant.GetComponent<WaterLife>().AddWater(bucket.waterValue);
        bucket.EmptyBucket();
        return true;
    }

    private bool IsBucketAvailable()
    {
        if (playerAction.pickUp != null && playerAction.pickUp.gameObject.name.StartsWith("Bucket"))
        {
            bucket = playerAction.pickUp.GetComponent<Bucket_Script>();
            return true;
        }
        return false;
    }
}
