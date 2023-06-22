using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IEnemyHealth : MonoBehaviour
{
    public Material damageMaterial = null;
    public float maxHp;
    public float currentHp;
    public GameObject seedprefab;

    SeedManager seedManager;
    List<char> materialQueue = new List<char>();

    private void Start()
    {
        currentHp = maxHp;
        seedManager = GameObject.Find("SeedManager").GetComponent<SeedManager>();
    }

    [Button("TestDeath")]
    public void TakeDamage()
    {
        currentHp -= 1000;
        if (currentHp < 0)
        {
            TryToDropSeed();
            Destroy(gameObject);
        }
    }
    
    public void TakeDamage(float damage)
    {
        TakeVisualDamage();
        currentHp -= damage;
        if (currentHp <= 0)
        {
            TryToDropSeed();
            Destroy(gameObject);
        }
    }

    void TryToDropSeed()
    {
        var randInt = Random.Range(1, 101);
        if(randInt <= seedManager.currentDropChance)
        {
            Instantiate(seedprefab, transform.position, Quaternion.identity);
        }
    }

    void TakeVisualDamage()
    {
        if(damageMaterial == null || materialQueue.Any())
        {
            return;
        }

        var renderer = gameObject.GetComponent<Renderer>();
        if (renderer)
        {
            materialQueue.Add('.');
            StartCoroutine(VisualDamage(renderer));
        }

        foreach (Renderer child in gameObject.transform.GetComponentsInChildren<Renderer>())
        {
            if(child.tag != "EnemyShield")
            {
                materialQueue.Add('.');
                StartCoroutine(VisualDamage(child));
            }
        }
    }

    IEnumerator VisualDamage(Renderer obj)
    {
        var material = obj.material;
        obj.material = damageMaterial;
        yield return new WaitForSeconds(0.1f);
        obj.material = material;
        materialQueue.RemoveAt(0);
    }

}
