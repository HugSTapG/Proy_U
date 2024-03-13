using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    [SerializeField]protected float damage;
    protected void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.tag == "Player")
        {
            hit.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
