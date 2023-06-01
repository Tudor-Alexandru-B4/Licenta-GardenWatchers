using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantCheckerRanged : MonoBehaviour
{
    IEnemyAttack attack;
    EnemyMovement movement;

    LayerMask mask;

    private void Start()
    {
        movement = transform.parent.GetComponent<EnemyMovement>();

        List<IEnemyAttack> enemyAttacks = new List<IEnemyAttack>();
        RandomUtils.GetInterfaces<IEnemyAttack>(out enemyAttacks, transform.parent.gameObject);
        if (enemyAttacks.Count > 0)
        {
            attack = enemyAttacks[0];
        }

        var layermask1 = 1 << 9; //Wall
        mask = layermask1;
    }

    private void OnTriggerStay(Collider other)
    {
        if (movement.target == other.gameObject)
        {
            if(!Physics.Linecast(transform.position, other.transform.position, mask))
            {
                movement.agent.SetDestination(transform.position);
                attack.TryToAttack(other.gameObject);
            }
        }
    }
}
