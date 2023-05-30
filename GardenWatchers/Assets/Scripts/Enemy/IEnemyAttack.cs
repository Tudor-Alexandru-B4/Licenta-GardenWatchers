using System.Collections.Generic;
using UnityEngine;

public class IEnemyAttack : MonoBehaviour
{
    public bool hasThorns;
    public float thornsDamagePercent = 10;
    public float attackCooldown;
    public float waterDrainDone;
    public float waterDrainTime;

    public bool canAttack = true;
    public float attackStunTime;

    public void AddStunTime(float time)
    {
        if(attackStunTime < time)
        {
            attackStunTime = time;
        }
    }

    public void TrySelfDamage()
    {
        if (hasThorns)
        {
            List<IEnemyHealth> enemyHealths = new List<IEnemyHealth>();
            RandomUtils.GetInterfaces<IEnemyHealth>(out enemyHealths, gameObject);
            if (enemyHealths.Count > 0)
            {
                enemyHealths[0].TakeDamage(waterDrainDone * thornsDamagePercent / 100);
            }
        }
    }

    public virtual void TryToAttack(GameObject target) { }
}
