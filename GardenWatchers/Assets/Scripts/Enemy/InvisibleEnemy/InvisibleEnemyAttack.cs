using System.Collections.Generic;
using UnityEngine;

public class InvisibleEnemyAttack : IEnemyAttack
{
    public float firstHitMultiplier = 3f;
    bool damageDone = false;
    bool redused = false;

    CapsuleCollider collider;
    List<MeshRenderer> mesh = new List<MeshRenderer>();

    private void Start()
    {
        collider = gameObject.GetComponent<CapsuleCollider>();
        mesh = new List<MeshRenderer>(gameObject.GetComponentsInChildren<MeshRenderer>());
        collider.enabled = false;
        foreach(var m in mesh)
        {
            m.enabled = false;
        }
        waterDrainDone *= firstHitMultiplier;
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
        foreach (var m in mesh)
        {
            m.enabled = true;
        }
        if (target.tag == "Well")
        {
            target.GetComponent<WellLife>().AddToActiveDrainTimed(waterDrainDone, waterDrainTime);
            damageDone = true;
        }
        else
        {
            target.GetComponent<WaterLife>().AddToActiveDrainTimed(waterDrainDone, waterDrainTime);
            damageDone = true;
        }

        if(!redused && damageDone)
        {
            redused = true;
            waterDrainDone /= firstHitMultiplier;
        }

        AddStunTime(attackCooldown);
        TrySelfDamage();
    }
}
