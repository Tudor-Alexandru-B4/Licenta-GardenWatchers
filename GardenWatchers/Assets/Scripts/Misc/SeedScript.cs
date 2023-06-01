using UnityEngine;

public class SeedScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }
}
