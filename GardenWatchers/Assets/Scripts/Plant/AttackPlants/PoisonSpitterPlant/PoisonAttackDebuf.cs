using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAttackDebuf : MonoBehaviour
{
    public float poisonDebufPercent;
    public float poisonDuration;

    IEnemyAttack enemyAttack;
    float damageDebuf;

    // Start is called before the first frame update
    void Start()
    {
        List<IEnemyAttack> enemyAttacks = new List<IEnemyAttack>();
        RandomUtils.GetInterfaces<IEnemyAttack>(out enemyAttacks, gameObject);
        if (enemyAttacks.Count > 0)
        {
            enemyAttack = enemyAttacks[0];
        }
        else
        {
            Destroy(this);
        }

        damageDebuf = enemyAttack.waterDrainDone * poisonDebufPercent / 100;
        enemyAttack.waterDrainDone -= damageDebuf;
    }

    // Update is called once per frame
    void Update()
    {
        poisonDuration -= Time.deltaTime;
        if(poisonDuration < 0)
        {
            enemyAttack.waterDrainDone += damageDebuf;
            Destroy(this);
        }
    }
}
