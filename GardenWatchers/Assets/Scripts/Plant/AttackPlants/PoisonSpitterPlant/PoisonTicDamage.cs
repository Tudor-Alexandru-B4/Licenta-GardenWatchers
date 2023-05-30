using UnityEngine;

public class PoisonTicDamage : MonoBehaviour
{
    public float poisonTicDamage;
    public float timeBetweenPoisonTics;
    public float poisonDuration;
    public IEnemyHealth enemyHealth;

    float poisonTicCooldown = 0;

    // Update is called once per frame
    void Update()
    {
        poisonDuration -= Time.deltaTime;
        if (poisonDuration < 0)
        {
            Destroy(this);
        }

        if (poisonTicCooldown <= 0)
        {
            enemyHealth.TakeDamage(poisonTicDamage);
            poisonTicCooldown = timeBetweenPoisonTics;
        }
        else
        {
            poisonTicCooldown -= Time.deltaTime;
        }
    }
}
