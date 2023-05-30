using System.Collections.Generic;
using UnityEngine;

public class SpikeRange : MonoBehaviour
{
    SpikeBombBarrier spikeBomb;

    private void Start()
    {
        spikeBomb = transform.parent.GetComponent<SpikeBombBarrier>();
    }

    private void OnTriggerExit(Collider other)
    {
        var spike = other.GetComponent<CactusSpike>();
        if (spike != null && spikeBomb.spikeList.Contains(spike))
        {
            Destroy(spike.gameObject);
        }
    }
}
