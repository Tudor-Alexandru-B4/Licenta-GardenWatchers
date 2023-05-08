using UnityEngine;

public class BasicEnemyAttack : IEnemyAttack
{
    // Update is called once per frame
    void Update()
    {
        if(attackStunTime > 0)
        {
            attackStunTime -= Time.deltaTime;
        }
        else
        {
            attackStunTime = 0;
        }
    }

    override
    public void TryToAttack(GameObject target)
    {
        if(attackStunTime > 0)
        {
            return;
        }

        target.GetComponent<WaterLife>().AddToActiveDrainTimed(waterDrainDone, waterDrainTime);
        AddStunTime(attackCooldown);
    }
}
