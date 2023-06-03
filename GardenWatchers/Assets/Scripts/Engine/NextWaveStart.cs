using System.Collections.Generic;
using UnityEngine;

public class NextWaveStart : MonoBehaviour
{
    public float timeToStart;
    public bool waveOngoing = false;

    public float waitTime = 0f;
    public int characters = 0;
    MeshRenderer meshRenderer;
    List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        var spawnerGameObject = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemySpawner"));
        foreach (var enemySpawner in spawnerGameObject)
        {
            var spawner = enemySpawner.GetComponent<EnemySpawner>();
            enemySpawners.Add(spawner);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waveOngoing)
        {
            meshRenderer.enabled = false;
        }
        else
        {
            meshRenderer.enabled = true;
            if(characters == 2)
            {
                if(waitTime < timeToStart)
                {
                    waitTime += Time.deltaTime;
                }
                else
                {
                    waveOngoing = true;
                    waitTime = 0f;
                    foreach(var spawner in enemySpawners)
                    {
                        spawner.SpawnWave();
                    }
                }
            }
            else
            {
                waitTime = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            characters++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            characters--;
        }
    }
}
