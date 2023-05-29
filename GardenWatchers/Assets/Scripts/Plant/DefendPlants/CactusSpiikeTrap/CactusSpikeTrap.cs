using System.Collections.Generic;
using UnityEngine;

public class CactusSpikeTrap : IPlantAttack
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
        if(!closed && targets.Count > 0)
        {
            if(waitBeforeCloseTimer >= waitBeforeClose)
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

        if(closedTime <= 0 && closed)
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
                }
            }
        }
    }

}
