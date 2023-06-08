using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CactusSpikeTrap : Planted
{
    private void OnTriggerEnter(Collider other)
    {
        if (!planted)
        {
            return;
        }

        if (other.tag == "Enemy")
        {
            List<IEnemyAttack> enemyAttacks = new List<IEnemyAttack>();
            RandomUtils.GetInterfaces<IEnemyAttack>(out enemyAttacks, other.gameObject);
            if (enemyAttacks.Count > 0)
            {
                enemyAttacks[0].hasThorns = true;
            }
        }
    }
}
