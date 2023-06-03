using UnityEngine;

public class RangedEnemyBullet : MonoBehaviour
{
    public float waterDrainDone;
    public float waterDrainTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Plant")
        {
            other.gameObject.GetComponent<WaterLife>().AddToActiveDrainTimed(waterDrainDone, waterDrainTime);
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Well")
        {
            other.gameObject.GetComponent<WellLife>().AddToActiveDrainTimed(waterDrainDone, waterDrainTime);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
