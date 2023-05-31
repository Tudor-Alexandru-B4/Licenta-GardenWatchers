using System.Collections.Generic;
using UnityEngine;

public class IPlantAttack : MonoBehaviour
{
    public bool planted = false;

    public float attackCooldown;
    public float damage;

    public bool canAttack = true;
    public float attackStunTime;

    public List<GameObject> targets = new List<GameObject>();

    LayerMask mask;
    private void Start()
    {
        var layermask1 = 1 << 9; //Wall
        mask = layermask1;
    }
    public void AddStunTime(float time)
    {
        if (attackStunTime < time)
        {
            attackStunTime = time;
        }
    }

    public virtual void TryToAttack(GameObject enemy) { }

    public GameObject ComputeClosestEnemy(List<GameObject> enemies, bool checkLOS = true)
    {
        float minDistance = Mathf.Infinity;
        GameObject closest = null;
        List<GameObject> enemiesTemp = new List<GameObject>(enemies);

        foreach (GameObject e in enemiesTemp)
        {
            if (!e)
            {
                if (enemies.Contains(e))
                {
                    enemies.Remove(e);
                }
                continue;
            }

            if (checkLOS)
            {
                if (Physics.Linecast(transform.position, e.transform.position, mask))
                {
                    continue;
                }
            }

            float distance = Mathf.Abs(Vector3.Distance(transform.position, e.transform.position));
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = e;
            }
        }
        return closest;
    }

    public GameObject ComputeFarthestEnemy(List<GameObject> enemies, bool checkLOS = true)
    {
        float maxDistance = -Mathf.Infinity;
        GameObject farthest = null;
        List<GameObject> enemiesTemp = new List<GameObject>(enemies);

        foreach (GameObject e in enemiesTemp)
        {
            if (!e)
            {
                if (enemies.Contains(e))
                {
                    enemies.Remove(e);
                }
                continue;
            }

            if (checkLOS)
            {
                if (Physics.Linecast(transform.position, e.transform.position, mask))
                {
                    continue;
                }
            }

            float distance = Mathf.Abs(Vector3.Distance(transform.position, e.transform.position));
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthest = e;
            }
        }
        return farthest;
    }
}
