using UnityEngine;

public class FireSpittingPlantAttack : IPlantAttack
{
    public GameObject partToRotate;
    public GameObject firePoint;

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

        partToRotate.transform.rotation = Quaternion.LookRotation(target.transform.position - partToRotate.transform.position, transform.up);
        firePoint.transform.rotation = Quaternion.LookRotation(target.transform.position - firePoint.transform.position, transform.up);

        GameObject bulletGameObject = Instantiate(fireBullet, firePoint.transform.position, firePoint.transform.rotation);
        
        var bulletComponent = bulletGameObject.GetComponent<FireSpittingBullet>();
        bulletComponent.damage = damage;
        bulletComponent.fireDuration = fireDuration;
        bulletComponent.timeBetweenFireTics = timeBetweenFireTics;
        bulletComponent.fireTicDamage = fireTicDamage;

        bulletGameObject.gameObject.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * bulletSpeed);
        AddStunTime(attackCooldown);
    }
}
