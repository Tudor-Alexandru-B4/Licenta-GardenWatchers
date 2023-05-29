using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target = null;
    GameObject well;
    public bool canMove = true;

    float stunTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        well = GameObject.FindGameObjectWithTag("Well");
    }

    // Update is called once per frame
    void Update()
    {
        if(stunTime <= 0)
        {
            canMove = true;
        }
        else
        {
            stunTime -= Time.deltaTime;
            canMove = false;
        }

        if (!canMove)
        {
            target = null;
            agent.SetDestination(transform.position);
            return;
        }

        NavMeshPath navMeshPath = new NavMeshPath();
        if (agent.CalculatePath(well.transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetPath(navMeshPath);
        }
        else
        {
            List<GameObject> plants = GameObject.FindGameObjectsWithTag("Plant").ToList<GameObject>();
            if (plants.Count <= 0 || (target != null && !plants.Contains(target)))
            {
                target = null;
                agent.SetDestination(transform.position);
                return;
            }

            target = ComputeClosestPlant(plants);
            if(target != null)
            {
                agent.SetDestination(target.transform.position);
            }
        }
    }

    GameObject ComputeClosestPlant(List<GameObject> plants)
    {
        float minDistance = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject p in plants)
        {
            if (!p.GetComponent<WaterLife>().canBeAttacked)
            {
                continue;
            }

            float distance = Mathf.Abs(Vector3.Distance(transform.position, p.transform.position));
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = p;
            }
        }
        return closest;
    }

    public void AddStunTime(float time)
    {
        if(stunTime < time)
        {
            stunTime = time;
        }
    }
}
