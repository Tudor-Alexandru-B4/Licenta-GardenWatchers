using UnityEngine;

public class IEnemyAttack : MonoBehaviour
{
    public float attackCooldown;
    public float waterDrainDone;
    public float waterDrainTime;

    public bool canAttack = true;
    public float attackStunTime;

    public void AddStunTime(float time)
    {
        if(attackStunTime < time)
        {
            attackStunTime = time;
        }
    }

    public virtual void TryToAttack(GameObject target) { }
}
