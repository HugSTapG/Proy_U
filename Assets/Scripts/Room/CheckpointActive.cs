using System.Collections;
using UnityEngine;

public class CheckpointActive : MonoBehaviour
{
    private SpriteRenderer checkS;
    public Sprite[] actS;
    public Sprite dactS;
    private int index = 0;
    private static CheckpointActive activeCheckpoint;
    void Start()
    {
        checkS = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && this != activeCheckpoint)
        {
            StartAnimation();
            if (activeCheckpoint != null)
            {
                activeCheckpoint.StopAnimation();
            }
            activeCheckpoint = this;
        }
    }

    IEnumerator ActiveCoRoutine()
    {
        yield return new WaitForSeconds(0.1618033988f);
        checkS.sprite = actS[index];
        index++;
        if(index == actS.Length)
        {
            index = 0;
        }
        StartCoroutine(ActiveCoRoutine());
    }
    void StartAnimation()
    {
        StartCoroutine(ActiveCoRoutine());
    }

    void StopAnimation()
    {
        StopAllCoroutines();
        checkS.sprite = dactS;
    }
}
