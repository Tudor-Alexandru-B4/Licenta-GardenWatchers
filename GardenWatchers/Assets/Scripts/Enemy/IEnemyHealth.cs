using NaughtyAttributes;
using UnityEngine;

public class IEnemyHealth : MonoBehaviour
{
    public float maxHp;
    public float currentHp;
    public GameObject seedprefab;

    SeedManager seedManager;

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

}
