using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using static UnityEditor.Experimental.GraphView.GraphView;

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
        var layermask1 = 1 << 2; //Bullet Layer
        var layermask2 = 1 << 5; //Enemy Layer
        var layermask3 = 1 << 9; //PlantLayer
        mask = layermask1 | layermask2 | layermask3;
    }
    public void AddStunTime(float time)
    {
        if (attackStunTime < time)
        {
            attackStunTime = time;
        }
    }

    public virtual void TryToAttack(GameObject enemy) { }

    public GameObject ComputeClosestEnemy(List<GameObject> enemies)
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

            if(Physics.Linecast(transform.position, e.transform.position, mask))
            {
                continue;
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
}
