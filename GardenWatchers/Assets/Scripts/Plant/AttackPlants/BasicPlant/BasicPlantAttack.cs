using UnityEngine;

public class BasicPlantAttack : IPlantAttack
{
    public GameObject partToRotate;
    public GameObject firePoint;

    public GameObject bullet;
    public float bulletSpeed;

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

        if(targets.Count > 0)
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

        GameObject bulletGameObject = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        bulletGameObject.GetComponent<BasicBullet>().damage = damage;
        bulletGameObject.gameObject.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * bulletSpeed);
        AddStunTime(attackCooldown);
    }
}
