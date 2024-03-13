using System.Collections;
using UnityEngine;

public class ShootTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;
    private float cooldownTimer;
    private void Attack()
    {
        cooldownTimer  = 0;

        projectiles[FindProjectile()].transform.position = firePoint.position;
        projectiles[FindProjectile()].GetComponent<EnemyProjectile>().ActivateProjectile();

    }
    private int FindProjectile()
    {
        for(int i = 0; i < projectiles.Length; i++)
        {
            if(!projectiles[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        
        if(cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
