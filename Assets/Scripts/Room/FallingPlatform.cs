using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay = 2f;
    private float respawnDelay = 1f;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(destroyDelay);

        // Disable the Rigidbody
        rb.bodyType = RigidbodyType2D.Static;

        // Reset the position after a delay
        yield return new WaitForSeconds(respawnDelay);

        // Reset position and enable Rigidbody
        transform.position = initialPosition;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }    

}
