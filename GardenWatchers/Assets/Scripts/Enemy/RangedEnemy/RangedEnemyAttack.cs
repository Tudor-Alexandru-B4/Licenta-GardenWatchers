using UnityEngine;

public class RangedEnemyAttack : IEnemyAttack
{
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

        GameObject bulletGameObject = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletGameObject.GetComponent<RangedEnemyBullet>().waterDrainDone = waterDrainDone;
        bulletGameObject.GetComponent<RangedEnemyBullet>().waterDrainTime = waterDrainTime;
        bulletGameObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        //target.GetComponent<WaterLife>().AddToActiveDrainTimed(waterDrainDone, waterDrainTime);
        AddStunTime(attackCooldown);
        TrySelfDamage();
    }
}
