using UnityEngine;

public class TarTicDamage : MonoBehaviour
{
    public float blackTarTicDamage;
    public float timeBetweenBlackTarTics;
    public float blackTarDuration;
    public IEnemyHealth enemyHealth;

    float blackTarTicCooldown = 0;

    // Update is called once per frame
    void Update()
    {
        blackTarDuration -= Time.deltaTime;
        if (blackTarDuration < 0)
        {
            Destroy(this);
        }

        if (blackTarTicCooldown <= 0)
        {
            enemyHealth.TakeDamage(blackTarTicDamage);
            blackTarTicCooldown = timeBetweenBlackTarTics;
        }
        else
        {
            blackTarTicCooldown -= Time.deltaTime;
        }
    }
}
