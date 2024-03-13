using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Health playerHealth;
    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }
    public void Respawn()
    {
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
        //Cam to checkpoint
        Camera.main.GetComponent<Camera2D>().CameraCenter(currentCheckpoint.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentCheckpoint = collision.transform;
        collision.GetComponent<Collider>().enabled = false;
    }
}
