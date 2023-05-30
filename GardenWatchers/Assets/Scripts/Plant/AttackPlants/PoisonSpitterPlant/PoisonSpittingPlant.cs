using UnityEngine;

public class PoisonSpittingPlant : IPlantAttack
{
    public GameObject fireBullet;
    public float bulletSpeed;

    public float poisonTicDamage;
    public float timeBetweenPoisonTics;
    public float poisonDuration;
    public float poisonDebufPercent;

    // Update is called once per frame
    void Update()
    {
        if (!planted)
        {
            return;
        }

        if (attackStunTime > 0)
        {
            attackStunTime -= Time.deltaTime;
        }
        else
        {
            attackStunTime = 0;
        }

        if (targets.Count > 0)
        {
            Debug.Log(targets.Count);
            TryToAttack(ComputeClosestEnemy(targets));
        }
    }

    override
    public void TryToAttack(GameObject target)
    {
        if (target == null || attackStunTime > 0)
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position, transform.up);

        GameObject bulletGameObject = Instantiate(fireBullet, transform.position, transform.rotation);

        var bulletComponent = bulletGameObject.GetComponent<PoisonSpittingBullet>();
        bulletComponent.damage = damage;
        bulletComponent.poisonDuration = poisonDuration;
        bulletComponent.timeBetweenPoisonTics = timeBetweenPoisonTics;
        bulletComponent.poisonTicDamage = poisonTicDamage;
        bulletComponent.poisonDebufPercent = poisonDebufPercent;

        bulletGameObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        AddStunTime(attackCooldown);
    }
}
