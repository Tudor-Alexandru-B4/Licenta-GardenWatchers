using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DefenderAbility : MonoBehaviour
{
    public float maxSize;
    public float increment;
    public float decrement;
    public float shieldWaitBeforeDestroy;
    public float enemyStunTime;

    bool deplying = true;

    // Update is called once per frame
    void Update()
    {
        bool scaling = false;

        if (deplying)
        {
            if (transform.localScale.x < maxSize)
            {
                transform.localScale += new Vector3(increment, 0f, 0f);
                scaling = true;
            }

            if (transform.localScale.z < maxSize)
            {
                transform.localScale += new Vector3(0f, 0f, increment);
                scaling = true;
            }

            if (!scaling)
            {
                StartCoroutine(DestroyShield());
            }
        }
        else
        {
            if (transform.localScale.x > 0.1f)
            {
                transform.localScale += new Vector3(-decrement, 0f, 0f);
                scaling = true;
            }

            if (transform.localScale.z > 0.1f)
            {
                transform.localScale += new Vector3(0f, 0f, -decrement);
                scaling = true;
            }

            if (!scaling)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DestroyShield()
    {
        yield return new WaitForSeconds(shieldWaitBeforeDestroy);
        deplying = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyShield")
        {
            other.GetComponent<SphereCollider>().isTrigger = true;
            return;
        }

        var movement = other.GetComponent<EnemyMovement>();
        if (movement != null)
        {
            movement.AddStunTime(enemyStunTime);
            if(other.GetComponent<Rigidbody>() == null)
            {
                var rb = other.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyShield")
        {
            other.GetComponent<SphereCollider>().isTrigger = false;
            return;
        }

        var movement = other.GetComponent<EnemyMovement>();
        if (movement != null)
        {
            var rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }
        }
    }
}
