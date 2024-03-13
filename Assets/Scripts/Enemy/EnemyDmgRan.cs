using UnityEngine;

public class EnemyDmgRan : MonoBehaviour
{
    [SerializeField]private float damage;
    private PlayerAttack playerAttack;
    private void Awake()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.tag == "Player" && !playerAttack.IsAttacking())
        {
            hit.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
