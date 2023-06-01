using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireSpittingBullet : MonoBehaviour
{
    public float damage;
    public float fireTicDamage;
    public float timeBetweenFireTics;
    public float fireDuration;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyShield")
        {
            List<IEnemyHealth> enemyHealths = new List<IEnemyHealth>();
            RandomUtils.GetInterfaces<IEnemyHealth>(out enemyHealths, collision.gameObject);
            if (enemyHealths.Count > 0)
            {
                enemyHealths[0].TakeDamage(damage);
                if (enemyHealths[0].GetComponent<FireTicDamage>() == null)
                {
                    var fireTic = enemyHealths[0].AddComponent<FireTicDamage>();
                    fireTic.fireDuration = fireDuration;
                    fireTic.fireTicDamage = fireTicDamage;
                    fireTic.timeBetweenFireTics = timeBetweenFireTics;
                    fireTic.enemyHealth = enemyHealths[0];
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
