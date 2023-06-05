using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public float soundVolume;
    public float musicVolume;
    public int defender = 1;
    public int attacker = 2;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
