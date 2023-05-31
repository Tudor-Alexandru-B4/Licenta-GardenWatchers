using UnityEngine;

public class DirtTosserAttack : IPlantAttack
{
    public GameObject bullet;
    public float bulletSpeed;
    public float impactRadius = 2.5f;

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
            TryToAttack(ComputeFarthestEnemy(targets, false));
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
        var dirtTosser = bulletGameObject.GetComponent<DirtTosserBullet>();
        dirtTosser.damage = damage;
        dirtTosser.impactRadius = impactRadius;
        dirtTosser.target = target;
        dirtTosser.bulletSpeed = bulletSpeed;
        AddStunTime(attackCooldown);
    }
}
