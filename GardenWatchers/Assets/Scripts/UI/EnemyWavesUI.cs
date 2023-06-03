using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StringPair
{
    public string key;
    public int value;

    public StringPair(string key, int value)
    {
        this.key = key;
        this.value = value;
    }
}

public class EnemyWavesUI : MonoBehaviour
{
    List<StringPair> enemiesInWave;
    List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

    int maxWaves = -1;
    TextMeshProUGUI counterText;
    TextMeshProUGUI enemiesText;

    bool gotNext = false;

    private void Start()
    {
        var spawnerGameObject = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemySpawner"));
        foreach(var enemySpawner in spawnerGameObject)
        {
            var spawner = enemySpawner.GetComponent<EnemySpawner>();
            enemySpawners.Add(spawner);

            int wave = spawner.enemyWaves.waves.Count;
            if(wave > maxWaves)
            {
                maxWaves = wave;
            }
        }

        enemiesInWave = new List<StringPair>()
        {
            new StringPair("Normal", 0),
            new StringPair("Ranged", 0),
            new StringPair("Hover", 0),
            new StringPair("Shield", 0),
            new StringPair("Invisible", 0)
        };

        counterText = GameObject.Find("RemainingWaves - Text").GetComponent<TextMeshProUGUI>();
        enemiesText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        int currentWave = enemySpawners[0].currentWaveToSpawn;
        counterText.text = currentWave.ToString() + " / " + maxWaves;

        if(currentWave == maxWaves)
        {
            //implement next level or something
            return;
        }

        var spawnedEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        if(spawnedEnemies.Count > 0 )
        {
            ShowCurrentEnemies(spawnedEnemies);
            gotNext = false;
        }
        else
        {
            if (!gotNext)
            {
                ShowNextWaveEnemies();
                gotNext = true;
            }
        }
    }

    private void ShowCurrentEnemies(List<GameObject> enemies)
    {
        ClearPairs();

        foreach (var enemy in enemies)
        {
            foreach(var pair in enemiesInWave)
            {
                if(enemy.name.StartsWith(pair.key + "Enemy"))
                {
                    pair.value++;
                    break;
                }
            }
        }

        string text = "";
        foreach(var pair in enemiesInWave)
        {
            if(pair.value > 0)
            {
                text += pair.key + " - " + pair.value.ToString() + "\n";
            }
        }

        enemiesText.text = text;
    }

    private void ShowNextWaveEnemies()
    {
        ClearPairs();

        foreach (var spawner in enemySpawners)
        {
            foreach(var enemyPair in spawner.enemyWaves.waves[spawner.currentWaveToSpawn].enemyList)
            {
                foreach (var pair in enemiesInWave)
                {
                    if (enemyPair.key.name.StartsWith(pair.key + "Enemy"))
                    {
                        pair.value += enemyPair.value;
                        break;
                    }
                }
            }
        }

        string text = "Next Wave:\n";
        foreach (var pair in enemiesInWave)
        {
            if (pair.value > 0)
            {
                text += pair.key + " - " + pair.value.ToString() + "\n";
            }
        }

        enemiesText.text = text;
    }

    private void ClearPairs()
    {
        foreach(var pair in enemiesInWave)
        {
            pair.value = 0;
        }
    }
}
