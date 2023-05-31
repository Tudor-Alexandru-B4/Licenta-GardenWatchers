using System.Collections.Generic;
using UnityEngine;

public class ImpactArea : MonoBehaviour
{
    public List<IEnemyHealth> enemyHealthList = new List<IEnemyHealth>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            List<IEnemyHealth> enemyHealths = new List<IEnemyHealth>();
            RandomUtils.GetInterfaces<IEnemyHealth>(out enemyHealths, other.gameObject);
            if (enemyHealths.Count > 0 && !enemyHealthList.Contains(enemyHealths[0]))
            {
                enemyHealthList.Add(enemyHealths[0]);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            List<IEnemyHealth> enemyHealths = new List<IEnemyHealth>();
            RandomUtils.GetInterfaces<IEnemyHealth>(out enemyHealths, other.gameObject);
            if (enemyHealths.Count > 0 && enemyHealthList.Contains(enemyHealths[0]))
            {
                enemyHealthList.Remove(enemyHealths[0]);
            }
        }
    }

}
