using System.Collections;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField]private float attackCooldown;
    [SerializeField]private float range;
    [SerializeField]private int damage;
    [Header("Collider Parameters")]
    [SerializeField]private float colliderDist;
    [SerializeField]private BoxCollider2D bxc;
    [Header("Player Layer")]
    [SerializeField]private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private PlayerAttack playerAttack;
    private EnemyPatrol enemyPatrol;
    private Health playerHealth;
    [Header("Animation")]
    [SerializeField]private float aframesL;
    [SerializeField]private int numFlashes;
    private SpriteRenderer eneM;
    public Sprite[] enemS;
    private int index = 0;
    private void Awake()
    {
        bxc = GetComponent<BoxCollider2D>();
        eneM = GetComponent<SpriteRenderer>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        StartCoroutine(MeleeCoRoutine());
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
        RaycastHit2D sight = Physics2D.BoxCast(bxc.bounds.center + transform.right * range * transform.localScale.x * colliderDist,
        new Vector3(bxc.bounds.size.x * range, bxc.bounds.size.y * range, bxc.bounds.size.z),
         0, Vector2.left, 0, playerLayer);
        if(sight.collider != null)
        {
            playerHealth = sight.transform.GetComponent<Health>();
        }
        return sight.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bxc.bounds.center + transform.right * range * transform.localScale.x * colliderDist,
         new Vector3(bxc.bounds.size.x * range, bxc.bounds.size.y * range, bxc.bounds.size.z));
    }
    private void DamagePlayer()
    {
        //Si esta en rango y no esta atacando toma dmg
        if(PlayerInsight() && !playerAttack.IsAttacking())
        {
            playerHealth.TakeDamage(damage);
        }
    }
    private IEnumerator AttackCoroutine()
    {
        DamagePlayer();
        StartCoroutine(EFlash(156, 255));
        yield return new WaitForSeconds(attackCooldown);
    }
    IEnumerator MeleeCoRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        eneM.sprite = enemS[index];
        index++;
        if(index == enemS.Length)
        {
            index = 0;
        }
        StartCoroutine(MeleeCoRoutine());
    }
    public IEnumerator EFlash(int va1, int va2)
    { 
        for(int i = 0; i < numFlashes; i++)
        {
            eneM.color = new Color(va1, 0, va2, .5f);
            yield return new WaitForSeconds(aframesL/(numFlashes*2));
            eneM.color = Color.white;
            yield return new WaitForSeconds(aframesL/(numFlashes*2));
        }
    }


}
