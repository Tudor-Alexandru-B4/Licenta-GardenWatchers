using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public class Pair
{
    public GameObject key;
    public int value;
}

[System.Serializable]
public class EnemyList
{
    public List<Pair> enemyList;
}

[System.Serializable]
public class EnemyWaves
{
    public List<EnemyList> waves;
}

public class EnemySpawner : MonoBehaviour
{
    public EnemyWaves enemyWaves = new EnemyWaves();
    public int currentWaveToSpawn = 0;

    Bounds bounds;

    private void Start()
    {
        bounds = GetComponent<BoxCollider>().bounds;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            SpawnWave();
        }
    }

    [Button]
    public void SpawnWave()
    {
        if(currentWaveToSpawn >= enemyWaves.waves.Count)
        {
            return;
        }

        var wave = enemyWaves.waves[currentWaveToSpawn].enemyList;

        foreach(var pair in wave)
        {
            for(int i = 0; i <  pair.value; i++)
            {
                SpawnEnemy(pair.key);
            }
        }
        currentWaveToSpawn++;
    }

    public void SpawnEnemy(GameObject enemy)
    {
        float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
        float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);

        var spawnedEnemy = Instantiate(enemy, bounds.center + new Vector3(offsetX, 1f, offsetZ), Quaternion.identity);
    }
    
}
