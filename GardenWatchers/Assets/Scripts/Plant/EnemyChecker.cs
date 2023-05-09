using System.Collections.Generic;
using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    IPlantAttack attack;

    private void Start()
    {
        List<IPlantAttack> plantAttacks = new List<IPlantAttack>();
        RandomUtils.GetInterfaces<IPlantAttack>(out plantAttacks, transform.parent.gameObject);
        if (plantAttacks.Count > 0)
        {
            attack = plantAttacks[0];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if(!attack.targets.Contains(other.gameObject))
            {
                attack.targets.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (attack.targets.Contains(other.gameObject))
            {
                attack.targets.Remove(other.gameObject);
            }
        }
    }
}
