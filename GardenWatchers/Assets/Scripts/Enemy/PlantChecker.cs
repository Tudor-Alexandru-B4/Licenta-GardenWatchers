using System.Collections.Generic;
using UnityEngine;

public class PlantChecker : MonoBehaviour
{
    IEnemyAttack attack;
    EnemyMovement movement;

    private void Start()
    {
        movement = transform.parent.GetComponent<EnemyMovement>();
        
        List<IEnemyAttack> enemyAttacks = new List<IEnemyAttack>();
        RandomUtils.GetInterfaces<IEnemyAttack>(out enemyAttacks, transform.parent.gameObject);
        if (enemyAttacks.Count > 0)
        {
            attack = enemyAttacks[0];
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(movement.target == other.gameObject)
        {
            attack.TryToAttack(other.gameObject);
        }
    }
}
