using UnityEngine;

public class RangedEnemyAttack : IEnemyAttack
{
    public GameObject firePoint;

    public GameObject bulletPrefab;
    public float bulletSpeed;

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
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position, transform.up);
        firePoint.transform.rotation = Quaternion.LookRotation(target.transform.position - firePoint.transform.position, firePoint.transform.up);

        GameObject bulletGameObject = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        bulletGameObject.GetComponent<RangedEnemyBullet>().waterDrainDone = waterDrainDone;
        bulletGameObject.GetComponent<RangedEnemyBullet>().waterDrainTime = waterDrainTime;
        bulletGameObject.gameObject.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * bulletSpeed);
        //target.GetComponent<WaterLife>().AddToActiveDrainTimed(waterDrainDone, waterDrainTime);
        AddStunTime(attackCooldown);
        TrySelfDamage();
    }
}
