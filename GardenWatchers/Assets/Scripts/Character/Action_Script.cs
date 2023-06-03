using System;
using System.Collections.Generic;
using UnityEngine;

public class Action_Script : MonoBehaviour
{
    public string player;
    public string plant;
    public string pick;
    public string seedRight;
    public string seedLeft;
    public string ability = "Ability";

    public Planting_Script farmLandDetector;
    public Watering_Script plantDetector;
    public PickUp_Script pickUpDetector;
    public List<GameObject> seedList;
    public int seedIndex = 0;

    public GameObject abilityPrefab;
    public GameObject ability2Prefab;
    public float abilityCooldown;
    public float ability2Cooldown;

    GameObject currentAbilityPrefab;
    [NonSerialized]
    public float currentAbilityCooldown = -1;
    [NonSerialized]
    public float cooldown = 0f;

    public GameObject pickUp = null;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name == "Player_Defensive")
        {
            currentAbilityPrefab = abilityPrefab;
            currentAbilityCooldown = abilityCooldown;
        }
        else
        {
            currentAbilityPrefab = ability2Prefab;
            currentAbilityCooldown = ability2Cooldown;
        }

        if (player == "2")
        {
            var movement = gameObject.GetComponent<Movement_Script>();
            movement.horizontal = movement.horizontal + "2";
            movement.vertical = movement.vertical + "2";
            plant = plant + "2";
            pick = pick + "2";
            seedRight = seedRight + "2";
            seedLeft = seedLeft + "2";
            ability = ability + "2";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(ability))
        {
            if(cooldown <= 0f)
            {
                var abilityObj = Instantiate(currentAbilityPrefab, transform.position, Quaternion.identity);
                abilityObj.transform.parent = transform.parent;
                cooldown = currentAbilityCooldown;
            }
        }

        if(cooldown >= 0f)
        {
            cooldown -= Time.deltaTime;
        }

        if (Input.GetButtonDown(pick))
        {
            if (pickUpDetector.TryToPickUp())
            {
                farmLandDetector.UpdateShadow();
            }
            else if(pickUp != null)
            {
                DropObject();
            }
        }

        if (Input.GetButtonDown(plant))
        {
            if (farmLandDetector.TryToPlant(seedList[seedIndex]))
            {
                Destroy(pickUp);
            }
            else
            {
                plantDetector.TryToWater();
            }
        }

        if (Input.GetButtonDown(seedLeft))
        {
            seedIndex = Math.Abs(Modulo((seedIndex - 1), seedList.Count));
            farmLandDetector.UpdateShadow();
        }

        if (Input.GetButtonDown(seedRight))
        {
            seedIndex = Math.Abs(Modulo((seedIndex + 1), seedList.Count));
            farmLandDetector.UpdateShadow();
        }
    }

    private void DropObject()
    {
        pickUp.transform.position = new Vector3(pickUp.transform.position.x, 1, pickUp.transform.position.z);
        pickUp.transform.parent = gameObject.transform.parent;
        pickUp = null;
        farmLandDetector.UpdateShadow();
    }

    private int Modulo(int a, int b)
    {
        return (a % b + b) % b;
    }
}
