using UnityEngine;

public class FireTicDamage : MonoBehaviour
{
    public float fireTicDamage;
    public float timeBetweenFireTics;
    public float fireDuration;
    public IEnemyHealth enemyHealth;

    float fireTicCooldown = 0;

    // Update is called once per frame
    void Update()
    {
        fireDuration -= Time.deltaTime;
        if(fireDuration < 0)
        {
            Destroy(this);
        }

        if(fireTicCooldown <= 0)
        {
            enemyHealth.TakeDamage(fireTicDamage);
            fireTicCooldown = timeBetweenFireTics;
        }
        else
        {
            fireTicCooldown -= Time.deltaTime;
        }
    }
}
