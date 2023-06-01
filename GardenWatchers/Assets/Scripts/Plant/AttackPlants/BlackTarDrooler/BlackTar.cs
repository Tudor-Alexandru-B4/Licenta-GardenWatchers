using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackTar : MonoBehaviour
{
    public float blackTarDuration;
    public float blackTarTicDamage;
    public float timeBetweenBlackTarTics;
    public float blackTarSlowDebuf;

    private void Start()
    {
        gameObject.GetComponent<StickyNectarTrap>().enemySpeedDebuf = blackTarSlowDebuf;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" || other.gameObject.tag == "EnemyShield")
        {
            List<IEnemyHealth> enemyHealths = new List<IEnemyHealth>();
            RandomUtils.GetInterfaces<IEnemyHealth>(out enemyHealths, other.gameObject);
            if (enemyHealths.Count > 0)
            {
                if (enemyHealths[0].GetComponent<TarTicDamage>() == null)
                {
                    var blackTarTic = enemyHealths[0].AddComponent<TarTicDamage>();
                    blackTarTic.blackTarDuration = blackTarDuration;
                    blackTarTic.blackTarTicDamage = blackTarTicDamage;
                    blackTarTic.timeBetweenBlackTarTics = timeBetweenBlackTarTics;
                    blackTarTic.enemyHealth = enemyHealths[0];
                }
            }
        }
    }

}
