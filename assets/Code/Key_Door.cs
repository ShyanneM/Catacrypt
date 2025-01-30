using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hasKey = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            TryUnlockDoor();
        }
    }

    void TryUnlockDoor ()
    {
        if (hasKey)
        {
            GameObject door = GameObject.FindWithTag("Door");
            if (door)
            {
                Destroy(door);
                hasKey = false;
            }
        }
    }
}