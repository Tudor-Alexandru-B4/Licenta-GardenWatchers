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

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        well = GameObject.FindGameObjectWithTag("Well");
    }

    // Update is called once per frame
    void Update()
    {
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
            agent.SetDestination(target.transform.position);
        }
    }

    GameObject ComputeClosestPlant(List<GameObject> plants)
    {
        float minDistance = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject p in plants)
        {
            float distance = Mathf.Abs(Vector3.Distance(transform.position, p.transform.position));
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = p;
            }
        }
        return closest;
    }
}
