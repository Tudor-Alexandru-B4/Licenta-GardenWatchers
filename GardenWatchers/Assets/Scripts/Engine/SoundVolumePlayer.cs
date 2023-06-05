using UnityEngine;

public class SoundVolumePlayer : MonoBehaviour
{
    private void Start()
    {
        var audioPlayer = GetComponent<AudioSource>();
        audioPlayer.volume = GameObject.Find("Settings").GetComponent<SettingsScript>().soundVolume;
    }
}
