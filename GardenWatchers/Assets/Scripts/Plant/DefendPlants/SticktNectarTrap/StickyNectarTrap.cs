using UnityEngine;

public class StickyNectarTrap : Planted
{
    public float enemySpeedDebuf;

    private void OnTriggerStay(Collider other)
    {
        if (!planted)
        {
            return;
        }

        if (other.tag == "Enemy")
        {
            var enemyMovement = other.GetComponent<EnemyMovement>();
            if (!enemyMovement.isSlowed)
            {
                enemyMovement.StopCollider();
                enemyMovement.agent.speed -= enemySpeedDebuf;
                enemyMovement.isSlowed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!planted)
        {
            return;
        }

        if (other.tag == "Enemy")
        {
            var enemyMovement = other.GetComponent<EnemyMovement>();
            if (enemyMovement.isSlowed)
            {
                enemyMovement.StartCollider();
                enemyMovement.agent.speed += enemySpeedDebuf;
                enemyMovement.isSlowed = false;
            }
        }
    }
}
