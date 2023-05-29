using UnityEngine;

public class BasicPlantAttack : IPlantAttack
{
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

        GameObject bulletGameObject = Instantiate(bullet, transform.position, transform.rotation);
        bulletGameObject.GetComponent<BasicBullet>().damage = damage;
        bulletGameObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        AddStunTime(attackCooldown);
    }
}
