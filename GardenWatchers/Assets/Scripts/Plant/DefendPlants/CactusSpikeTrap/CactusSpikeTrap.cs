using System.Collections.Generic;
using UnityEngine;

public class CactusSpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
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
