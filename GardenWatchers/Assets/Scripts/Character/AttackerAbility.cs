using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerAbility : MonoBehaviour
{
    public float maxSize;
    public float increment;
    public float decrement;
    public float attackZoneWaitBeforeDestroy;
    public float damagePerTic;
    public float timeBetweenTics;

    bool deplying = true;
    float ticCooldown = 0f;
    List<IEnemyHealth> enemyHealthList = new List<IEnemyHealth>();

    // Update is called once per frame
    void Update()
    {
        if(ticCooldown <= 0f)
        {
            ticCooldown = timeBetweenTics;
            foreach(var enemy in new List<IEnemyHealth>(enemyHealthList))
            {
                if (enemy)
                {
                    enemy.TakeDamage(damagePerTic);
                }
            }
        }
        else
        {
            ticCooldown -= Time.deltaTime;
        }

        bool scaling = false;

        if (deplying)
        {
            if (transform.localScale.x < maxSize)
            {
                transform.localScale += new Vector3(increment, 0f, 0f);
                scaling = true;
            }

            if (transform.localScale.y < maxSize)
            {
                transform.localScale += new Vector3(0f, increment, 0f);
                scaling = true;
            }

            if (transform.localScale.z < maxSize)
            {
                transform.localScale += new Vector3(0f, 0f, increment);
                scaling = true;
            }

            if (!scaling)
            {
                StartCoroutine(DestroyShield());
            }
        }
        else
        {
            if (transform.localScale.x > 0.1f)
            {
                transform.localScale += new Vector3(-decrement, 0f, 0f);
                scaling = true;
            }

            if (transform.localScale.y > 0.1f)
            {
                transform.localScale += new Vector3(0f, -decrement, 0f);
                scaling = true;
            }

            if (transform.localScale.z > 0.1f)
            {
                transform.localScale += new Vector3(0f, 0f, -decrement);
                scaling = true;
            }

            if (!scaling)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DestroyShield()
    {
        yield return new WaitForSeconds(attackZoneWaitBeforeDestroy);
        deplying = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "EnemyShield")
        {
            List<IEnemyHealth> enemyHealths = new List<IEnemyHealth>();
            RandomUtils.GetInterfaces<IEnemyHealth>(out enemyHealths, other.gameObject);
            if (enemyHealths.Count > 0)
            {
                if (!enemyHealthList.Contains(enemyHealths[0]))
                {
                    enemyHealthList.Add(enemyHealths[0]);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "EnemyShield")
        {
            List<IEnemyHealth> enemyHealths = new List<IEnemyHealth>();
            RandomUtils.GetInterfaces<IEnemyHealth>(out enemyHealths, other.gameObject);
            if (enemyHealths.Count > 0)
            {
                if (enemyHealthList.Contains(enemyHealths[0]))
                {
                    enemyHealthList.Remove(enemyHealths[0]);
                }
            }
        }
    }
}
