using System.Collections.Generic;
using UnityEngine;

public class CactusSpike : MonoBehaviour
{
    public float damage;
    public float speed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyShield")
        {
            List<IEnemyHealth> enemyHealths = new List<IEnemyHealth>();
            RandomUtils.GetInterfaces<IEnemyHealth>(out enemyHealths, collision.gameObject);
            if (enemyHealths.Count > 0)
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

    public void LaunchSpike()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }
}
