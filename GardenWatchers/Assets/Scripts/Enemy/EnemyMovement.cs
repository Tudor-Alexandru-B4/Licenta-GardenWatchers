using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target = null;
    GameObject well;
    public bool canMove = true;
    public bool isSlowed = false;

    float stunTime = -0.5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        well = GameObject.FindGameObjectWithTag("Well");
    }

    // Update is called once per frame
    void Update()
    {
        if(stunTime < 0)
        {
            StartCollider();
            canMove = true;
        }
        else if(stunTime > 0)
        {
            StopCollider();
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
            target = well;
        }
        else
        {
            List<GameObject> plants = GameObject.FindGameObjectsWithTag("Plant").ToList<GameObject>();
            if (plants.Count <= 0 || (target != null && !plants.Contains(target)) || !TestPathToPlant(target))
            {
                target = null;
                agent.SetDestination(transform.position);
            }

            if(target == null)
            {
                target = ComputeClosestPlant(plants);
                if (target != null)
                {
                    agent.SetDestination(target.transform.position);
                }
            }
        }
    }

    private bool CheckTargetAvailable(List<GameObject> plants)
    {
        if(target == null)
        {
            return false;
        }
        
        if(plants.Count <= 0 || (target != null && !plants.Contains(target)))
        {
            return false;
        }

        if (!TestPathToPlant(target))
        {
            return false;
        }

        var dist = Mathf.Abs(Vector3.Distance(transform.position, target.transform.position));
        foreach (GameObject plant in plants)
        {
            if(Mathf.Abs(Vector3.Distance(transform.position, target.transform.position)) < dist)
            {
                return false;
            }
        }

        return true;
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

            if(!TestPathToPlant(p))
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

    public void StopCollider()
    {
        agent.radius = 0.1f;
    }

    public void StartCollider()
    {
        agent.radius = 0.5f;
    }

    private bool TestPathToPlant(GameObject go)
    {
        if(go == null || !go)
        {
            return false;
        }

        foreach(float pos1 in new float[] { -1f, 0, 1f })
        {
            foreach(float pos2 in new float[] { -1f, 0, 1f })
            {
                NavMeshPath navMeshPath = new NavMeshPath();
                if (agent.CalculatePath(go.transform.position + new Vector3(pos1, 0, pos2), navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
                {
                    return true;
                }
            }
        }

        return false;
    }

}
