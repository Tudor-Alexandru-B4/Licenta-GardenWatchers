using UnityEngine;
using UnityEngine.AI;

public class Planting_Script : MonoBehaviour
{
    private GameObject planter;
    private Action_Script playerAction;

    // Start is called before the first frame update
    void Start()
    {
        playerAction = gameObject.transform.parent.GetComponent<Action_Script>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FarmLand")
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
            AddNavObstacle(plant);
            plant.gameObject.GetComponent<WaterLife>().planted = true;
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
        navObstacle.shape = NavMeshObstacleShape.Box;
        navObstacle.size = planter.transform.localScale;
        navObstacle.carving = true;
    }
}
