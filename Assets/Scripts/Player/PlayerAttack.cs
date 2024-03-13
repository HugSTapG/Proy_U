using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool canAttack = true;
    private bool isAttacking;
    private bool debuffed = false;
    private bool buffed = false;
    private float attackDistStore;
    private float attackDistance = 2f;
    private float attackTime = 0.2f;
    private float attackCooldown = 1f;
    private TrailRenderer tr;
    private Rigidbody2D body;

    void Start()
    {
        attackDistStore = attackDistance;
        tr = GetComponent<TrailRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    public bool CanAttack()
    {
        return canAttack;
    }
    public bool IsAttacking()
    {
        return isAttacking;
    }
    public bool IsDebuffed()
    {
        return debuffed;
    }
    public bool IsBuffed()
    {
        return buffed;
    }
    public void AttackDebuff(float var)
    {
        debuffed = true;
        attackDistance -= var;
    }
    public void AttackBuff(float var)
    {
        buffed = true;
        attackDistance += var;
    }
    public void AttackReset()
    {
        attackDistance = attackDistStore;
    }

    public IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;
        float ogGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2(transform.localScale.x * attackDistance, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(attackTime);
        tr.emitting = false;
        body.gravityScale = ogGravity;
        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    public IEnumerator ResetAttackDistance()
    {
        yield return new WaitForSeconds(5f);
        AttackReset();
        if (debuffed)
        {
            debuffed = false;
        }
        else if (buffed)
        {
            buffed = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isAttacking && collision.tag == "Enemy" && collision.GetComponent<Health>())
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }
}
