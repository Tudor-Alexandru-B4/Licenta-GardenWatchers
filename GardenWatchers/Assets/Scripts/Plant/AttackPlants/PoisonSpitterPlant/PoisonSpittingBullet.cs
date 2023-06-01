using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonSpittingBullet : MonoBehaviour
{
    public float damage;
    public float poisonTicDamage;
    public float timeBetweenPoisonTics;
    public float poisonDuration;
    public float poisonDebufPercent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            List<IEnemyHealth> enemyHealths = new List<IEnemyHealth>();
            RandomUtils.GetInterfaces<IEnemyHealth>(out enemyHealths, collision.gameObject);
            if (enemyHealths.Count > 0)
            {
                enemyHealths[0].TakeDamage(damage);
                if (enemyHealths[0].GetComponent<PoisonTicDamage>() == null)
                {
                    var poisonTic = enemyHealths[0].AddComponent<PoisonTicDamage>();
                    poisonTic.poisonDuration = poisonDuration;
                    poisonTic.poisonTicDamage = poisonTicDamage;
                    poisonTic.timeBetweenPoisonTics = timeBetweenPoisonTics;
                    poisonTic.enemyHealth = enemyHealths[0];
                }

                if (enemyHealths[0].GetComponent<PoisonAttackDebuf>() == null)
                {
                    var poisonDebuf = enemyHealths[0].AddComponent<PoisonAttackDebuf>();
                    poisonDebuf.poisonDebufPercent = poisonDebufPercent;
                    poisonDebuf.poisonDuration = poisonDuration;
                }
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "EnemyShield")
        {
            List<IEnemyHealth> enemyHealths = new List<IEnemyHealth>();
            RandomUtils.GetInterfaces<IEnemyHealth>(out enemyHealths, collision.gameObject);
            if (enemyHealths.Count > 0)
            {
                enemyHealths[0].TakeDamage(damage);
                if (enemyHealths[0].GetComponent<PoisonTicDamage>() == null)
                {
                    var poisonTic = enemyHealths[0].AddComponent<PoisonTicDamage>();
                    poisonTic.poisonDuration = poisonDuration;
                    poisonTic.poisonTicDamage = poisonTicDamage;
                    poisonTic.timeBetweenPoisonTics = timeBetweenPoisonTics;
                    poisonTic.enemyHealth = enemyHealths[0];
                }
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
