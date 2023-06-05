using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusBearTrap : IPlantAttack
{
    public float stunTime;
    public float waitBeforeClose;
    public float closedAfterStunTime;
    public bool closed = false;
    public Vector3 closedScale;
    Vector3 openScale;

    float closedTime = 0;
    public float waitBeforeCloseTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        openScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        targets = FilterOutDestroiedTargets();

        if (!closed && targets.Count > 0)
        {
            if (waitBeforeCloseTimer >= waitBeforeClose)
            {
                transform.localScale = closedScale;
                StunTargets();
                closedTime = stunTime + closedAfterStunTime;
                waitBeforeCloseTimer = 0;
                closed = true;
            }
            else
            {
                waitBeforeCloseTimer += Time.deltaTime;
            }
        }

        if (closedTime <= 0 && closed)
        {
            transform.localScale = openScale;
            closed = false;
        }
        else
        {
            closedTime -= Time.deltaTime;
        }
    }

    void StunTargets()
    {
        foreach (GameObject target in targets)
        {
            if (target)
            {
                List<EnemyMovement> enemyMovements = new List<EnemyMovement>();
                RandomUtils.GetInterfaces<EnemyMovement>(out enemyMovements, target);
                if (enemyMovements.Count > 0)
                {
                    enemyMovements[0].AddStunTime(stunTime);
                    StartCoroutine(TeleportMiddle(enemyMovements[0].gameObject));
                }
            }
        }
    }

    IEnumerator TeleportMiddle(GameObject obj)
    {
        yield return new WaitForSeconds(0.1f);
        if (obj)
        {
            obj.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    }

    List<GameObject> FilterOutDestroiedTargets()
    {
        List<GameObject> existingTargets = new List<GameObject>();
        foreach (GameObject target in targets)
        {
            if (target)
            {
                existingTargets.Add(target);
            }
        }
        return existingTargets;
    }
}
