using UnityEngine;

public class FireSpittingPlantAttack : IPlantAttack
{
    public GameObject fireBullet;
    public float bulletSpeed;

    public float fireTicDamage;
    public float timeBetweenFireTics;
    public float fireDuration;

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
        
        var bulletComponent = bulletGameObject.GetComponent<FireSpittingBullet>();
        bulletComponent.damage = damage;
        bulletComponent.fireDuration = fireDuration;
        bulletComponent.timeBetweenFireTics = timeBetweenFireTics;
        bulletComponent.fireTicDamage = fireTicDamage;

        bulletGameObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        AddStunTime(attackCooldown);
    }
}
