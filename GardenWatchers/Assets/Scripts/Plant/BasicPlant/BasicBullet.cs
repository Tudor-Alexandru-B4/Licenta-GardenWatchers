using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            List<IEnemyHealth> enemyHealths = new List<IEnemyHealth>();
            RandomUtils.GetInterfaces<IEnemyHealth>(out enemyHealths, collision.gameObject);
            if(enemyHealths.Count > 0 )
            {
                enemyHealths[0].TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
