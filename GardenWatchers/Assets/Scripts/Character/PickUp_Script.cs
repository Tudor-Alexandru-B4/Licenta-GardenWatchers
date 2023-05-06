using UnityEngine;

public class PickUp_Script : MonoBehaviour
{
    GameObject pickUp;
    private Action_Script playerAction;

    // Start is called before the first frame update
    void Start()
    {
        playerAction = gameObject.transform.parent.GetComponent<Action_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            pickUp = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            pickUp = null;
        }
    }

    public bool TryToPickUp()
    {
        if (pickUp != null)
        {
            GameObject willDrop = null;
            if(playerAction.pickUp != null)
            {
                willDrop = playerAction.pickUp;
            }

            playerAction.pickUp = pickUp;
            playerAction.pickUp.transform.parent = playerAction.transform.Find("Hand");
            playerAction.pickUp.transform.position = playerAction.transform.Find("Hand").position;
            pickUp = null;

            if(willDrop != null)
            {
                willDrop.transform.position = new Vector3(willDrop.transform.position.x, 1, willDrop.transform.position.z);
                willDrop.transform.parent = gameObject.transform.parent.parent;
            }
            return true;
        }
        return false;
    }
}
