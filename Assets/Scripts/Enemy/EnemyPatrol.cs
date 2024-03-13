using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [Header("Movement Parameters")]
    [SerializeField] private float enemySpeed;
    private Vector3 initScale;
    private bool movL;
    [Header("Idle Behaviour")]
    [SerializeField] private float idleDur;
    private float idleTimer;
    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void Update()
    {
        if(movL)
        {
            if(enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if(enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }
    private void DirectionChange()
    {
        idleTimer += Time.deltaTime;
        if(idleTimer > idleDur)
        {
            movL = !movL;
        }
    }
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        //Face dir
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x * _direction),
         initScale.y, initScale.z);

        //Move dir
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * enemySpeed,
        enemy.position.y, enemy.position.z);

    }
}
