using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Planting_Script : MonoBehaviour
{
    public GameObject waterLevelDisplayPrefab;
    private GameObject planter;
    private Action_Script playerAction;

    // Start is called before the first frame update
    void Start()
    {
        playerAction = gameObject.transform.parent.GetComponent<Action_Script>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "FarmLand" && planter == null && planter != other.gameObject)
        {
            planter = other.gameObject;

            UpdateShadow();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FarmLand" && planter == null)
        {
            planter = other.gameObject;

            UpdateShadow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FarmLand")
        {
            DestroyShadow();

            planter = null;
        }
    }

    public bool TryToPlant(GameObject prefab)
    {
        if(!IsSeedAvailable())
        {
            return false;
        }

        if(planter != null && (planter.transform.childCount == 0 || planter.gameObject.transform.Find("shadowPlant")))
        {
            DestroyShadow();

            Vector3 spawnPosition = planter.gameObject.transform.position;
            spawnPosition.y = prefab.transform.position.y;
            GameObject plant = Instantiate(prefab, spawnPosition, Quaternion.identity);
            plant.transform.parent = planter.gameObject.transform;
            plant.gameObject.tag = "Plant";

            if (plant.GetComponent<WaterLife>().canBeAttacked)
            {
                AddNavObstacle(plant);
            }

            Instantiate(waterLevelDisplayPrefab, planter.transform);
            plant.gameObject.GetComponent<WaterLife>().planted = true;

            List<IPlantAttack> plantAttacks = new List<IPlantAttack>();
            RandomUtils.GetInterfaces<IPlantAttack>(out plantAttacks, plant);
            if (plantAttacks.Count > 0)
            {
                plantAttacks[0].planted = true;
            }

            return true;
        }
        return false;
    }

    public void UpdateShadow()
    {
        if (!IsSeedAvailable())
        {
            DestroyShadow();
            return;
        }

        if (planter != null && (planter.transform.childCount == 0 || planter.gameObject.transform.Find("shadowPlant")))
        {
            DestroyShadow();

            GameObject prefab = playerAction.seedList[playerAction.seedIndex];
            Vector3 spawnPosition = planter.gameObject.transform.position;
            spawnPosition.y = prefab.transform.position.y;
            GameObject plantShowing = Instantiate(prefab, spawnPosition, Quaternion.identity);

            plantShowing.gameObject.name = "shadowPlant";
            var oldColor = plantShowing.GetComponent<Renderer>().material.color;
            plantShowing.GetComponent<Renderer>().material.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);

            foreach(Transform child in plantShowing.transform)
            {
                if (!child.gameObject.GetComponent<Renderer>())
                {
                    continue;
                }
                oldColor = child.gameObject.GetComponent<Renderer>().material.color;
                child.gameObject.GetComponent<Renderer>().material.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
            }

            plantShowing.transform.parent = planter.gameObject.transform;
        }
    }

    private bool IsSeedAvailable()
    {
        if (playerAction.pickUp != null && playerAction.pickUp.gameObject.name.StartsWith("Seed"))
        {
            return true;
        }
        return false;
    }
    
    private void DestroyShadow()
    {
        if(planter == null)
        {
            return;
        }

        var foundShadow = planter.gameObject.transform.Find("shadowPlant");
        if (foundShadow != null)
        {
            Destroy(foundShadow.gameObject);
        }
    }

    private void AddNavObstacle(GameObject plant)
    {
        var navObstacle = plant.AddComponent<NavMeshObstacle>();
        navObstacle.shape = NavMeshObstacleShape.Capsule;
        navObstacle.radius = 1;
        navObstacle.carving = true;
        navObstacle.carveOnlyStationary = false;
    }
}
