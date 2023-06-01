using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvisibleEnemyAttack : IEnemyAttack
{
    CapsuleCollider collider;
    MeshRenderer mesh;

    private void Start()
    {
        collider = gameObject.GetComponent<CapsuleCollider>();
        mesh = gameObject.GetComponent<MeshRenderer>();
        collider.enabled = false;
        mesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackStunTime > 0)
        {
            attackStunTime -= Time.deltaTime;
        }
        else
        {
            attackStunTime = 0;
        }
    }

    override
    public void TryToAttack(GameObject target)
    {
        if (attackStunTime > 0)
        {
            return;
        }

        collider.enabled = true;
        mesh.enabled = true;
        target.GetComponent<WaterLife>().AddToActiveDrainTimed(waterDrainDone, waterDrainTime);
        AddStunTime(attackCooldown);
        TrySelfDamage();
    }
}
