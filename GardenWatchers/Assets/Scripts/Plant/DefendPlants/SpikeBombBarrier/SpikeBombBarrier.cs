using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBombBarrier : MonoBehaviour
{
    public float spikeDamage;
    public float spikeSpeed;
    public float spikeCooldown;
    public List<CactusSpike> spikeList = new List<CactusSpike>();

    GameObject spikes;
    WaterLife waterLife;
    float currentCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spikes = transform.Find("Spikes").gameObject;
        waterLife = GetComponent<WaterLife>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }

        if(waterLife.activeDrainSpeed > 0f && currentCooldown <= 0f)
        {
            FireSpikes();
        }
    }

    [Button]
    void FireSpikes()
    {
        spikeList = new List<CactusSpike>();

        currentCooldown = spikeCooldown;
        var spikesObject = Instantiate(spikes, transform.position, Quaternion.identity);
        foreach (Transform spike in spikesObject.transform)
        {
            var spikeScript = spike.GetComponent<CactusSpike>();
            spikeScript.damage = spikeDamage;
            spikeScript.speed = spikeSpeed;
            spikeList.Add(spikeScript);
        }
        spikesObject.transform.DetachChildren();
        Destroy(spikesObject);

        foreach(var spike in spikeList)
        {
            spike.LaunchSpike();
        }

    }
}
