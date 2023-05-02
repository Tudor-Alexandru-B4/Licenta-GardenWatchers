using UnityEngine;

public class Movement_Script : MonoBehaviour
{
    public float speed;
    public string horizontal;
    public string vertical;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDirection = new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical));
        rb.velocity = movementDirection * speed * Time.fixedDeltaTime;
    }
}
