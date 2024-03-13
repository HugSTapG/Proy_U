using System.Collections;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField]private float attackCooldown;
    [SerializeField]private float range;
    [SerializeField]private int damage;
    [Header("Ranged Attack")]
    [SerializeField]private Transform firepoint;
    [SerializeField]private GameObject[] projectiles;
    [Header("Collider Parameters")]
    [SerializeField]private float colliderDist;
    [SerializeField]private BoxCollider2D bxc;
    [Header("Player Layer")]
    [SerializeField]private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    //References
    private EnemyPatrol enemyPatrol;
    [Header("Animation")]
    [SerializeField]private float aframesL;
    [SerializeField]private int numFlashes;
    private SpriteRenderer eneR;
    public Sprite[] enerS;
    private int index = 0;
    private void Awake()
    {
        bxc = GetComponent<BoxCollider2D>();
        eneR = GetComponent<SpriteRenderer>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        StartCoroutine(RangedCoRoutine());
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        
        if(PlayerInsight() && cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            StartCoroutine(AttackCoroutine());
        }
        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInsight();
        }
    }

    private bool PlayerInsight()
    {
        RaycastHit2D sight = Physics2D.BoxCast(bxc.bounds.center + -transform.up * range * transform.localScale.y * colliderDist,
        new Vector3(bxc.bounds.size.x * range, bxc.bounds.size.y * range, bxc.bounds.size.z),
         0, Vector2.left, 0, playerLayer);

        return sight.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bxc.bounds.center + -transform.up * range * transform.localScale.y * colliderDist,
         new Vector3(bxc.bounds.size.x * range, bxc.bounds.size.y * range, bxc.bounds.size.z));
    }

    private void RangedAttack()
    {
        cooldownTimer = 0;
        //Shoot Projectiles
        projectiles[FindProjectile()].transform.position = firepoint.position;
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


    private IEnumerator AttackCoroutine()
    {
        RangedAttack();
        StartCoroutine(EFlash(156, 255));
        yield return new WaitForSeconds(attackCooldown);
    }


    IEnumerator RangedCoRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        eneR.sprite = enerS[index];
        index++;
        if(index == enerS.Length)
        {
            index = 0;
        }
        StartCoroutine(RangedCoRoutine());
    }
    public IEnumerator EFlash(int va1, int va2)
    { 
        for(int i = 0; i < numFlashes; i++)
        {
            eneR.color = new Color(va1, 0, va2, .5f);
            yield return new WaitForSeconds(aframesL/(numFlashes*2));
            eneR.color = Color.white;
            yield return new WaitForSeconds(aframesL/(numFlashes*2));
        }
    }
}
