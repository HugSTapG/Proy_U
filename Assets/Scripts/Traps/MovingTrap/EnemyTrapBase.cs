using System.Collections;
using UnityEngine;

public class EnemyTrapBase : MonoBehaviour
{
    [Header("Trap Parameters")]
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float movementDist;
    [Header("Animation")]
    public Sprite[] rotationSprites;
    protected SpriteRenderer rotation;
    protected int index = 0;
    protected virtual void Start()
    {
        rotation = GetComponent<SpriteRenderer>();
        StartCoroutine(RotateCoRoutine());
    }

    protected IEnumerator RotateCoRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        rotation.sprite = rotationSprites[index];
        index++;
        if(index == rotationSprites.Length)
        {
            index = 0;
        }
        StartCoroutine(RotateCoRoutine());
    }

    protected virtual void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
        {
            hit.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
