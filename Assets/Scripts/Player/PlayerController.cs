using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    public float playerJumpForce = 20f;
    public float playerSpeed = 5f;
    public float playerSpeedStored;
    private bool isFacing = true;
    private bool isWalking = true;
    private bool isChangingDir = false;
    private Vector3 respawnPoint;
    [Header("Animation")]
    public Sprite[] movingSprites;
    public Sprite[] jumpingSprites;
    public Sprite[] changingSprites;
    public Sprite deadSprite;
    private int index = 0;
    [Header("Components and Layers")]
    [SerializeField]private Rigidbody2D body;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask wallLayer;
    public CoinManager cm;
    private PlayerAttack playerAttack;
    private BoxCollider2D boxC;
    private SpriteRenderer spriteR;

    void Start()
    {
        playerSpeedStored = playerSpeed;
        respawnPoint = transform.position;
        body = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        boxC = GetComponent<BoxCollider2D>();
        playerAttack = GetComponent<PlayerAttack>();
        StartCoroutine(WalkCoRoutine());
    }

    void Update()
    {
        //No hacer nada si esta atacando, voltear si toca un muro/isla mientras ataca
        // Si esta cambiando de dir no voltea
        if(playerAttack.IsAttacking())
        {
            if (!isChangingDir && (OnWall() || OnIsland("Island")))
            {
                FlipLogic();
            }
            return;
        }
        //Cambio de Direccion
        if (Input.GetKeyDown(KeyCode.W))
        {
            Change();
        }
        //Mov constante segun velocidad
        body.velocity = new Vector2(playerSpeed, body.velocity.y);
        //Salto
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
        //Detectar muros/islas frente al jugador
        if (OnWall() || OnIsland("Island"))
        {
            FlipLogic();
        }
        //Atacar
        if(Input.GetKeyDown(KeyCode.E) && playerAttack.CanAttack())
        {
            StartCoroutine(playerAttack.Attack());
        }
    }
    //Salto - Metodo
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, playerJumpForce);
        StartCoroutine(JumpCoRoutine());
    }
    //Cambio de direccion
    private void Change()
    {
        if (isChangingDir)
        {
            return;
        }
        StartCoroutine(ChangeCoRoutine());
    }
    //Detectar Piso
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxC.bounds.center,
         boxC.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    //Detectar Muro/Isla
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxC.bounds.center,
         boxC.bounds.size, 0, new Vector2(transform.localScale.x, 0),
          0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    private bool OnIsland(string tag)
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxC.bounds.center,
            boxC.bounds.size, 0, new Vector2(transform.localScale.x, 0),
            0.1f, groundLayer);
        if (raycastHit.collider != null && raycastHit.collider.CompareTag(tag))
        {
            return true;
        }
        return false;
    }
    //Voltear
    private void FlipLogic()
    {
        isFacing = !isFacing;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        playerSpeed *= -1f;
        transform.localScale = localScale;
    }
    //Colisiones
    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "ItemG":
                Destroy(collision.gameObject);
                cm.AddCoins();
                if(cm.coinCount % 10 == 0)
                {
                    playerAttack.AttackBuff(1f);
                    StartCoroutine(playerAttack.ResetAttackDistance());
                }
                break;
            case "ItemB":
                Destroy(collision.gameObject);
                if (cm.GetCoins() == 0 && !playerAttack.IsDebuffed())
                {
                    playerAttack.AttackDebuff(1f);
                    StartCoroutine(playerAttack.ResetAttackDistance());
                }
                else
                {
                    cm.RemoveCoin();
                }
                break;
            case "Checkpoint":
                respawnPoint = transform.position;
                break;
            case "Deathzone":
                GetComponent<Health>().TakeDamage(1f);
                transform.position = respawnPoint;
                break;
            default:
                //
                break;
        }
    }
    //Movimiento - Desactivar/Activar
    public void StopWalkingAnimation()
    {
        isWalking = false;
    }
    public void StartWalkingAnimation()
    {
        isWalking = true;
    }
    public void StopMovement()
    {
        StopWalkingAnimation();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    public void StartMovement()
    {
        StartWalkingAnimation();
        GetComponent<Rigidbody2D>().velocity = new Vector2(playerSpeedStored, GetComponent<Rigidbody2D>().velocity.y);
    }
    //Animacion de Movimiento
    IEnumerator WalkCoRoutine()
    {
        //Mientras la variable de camianta este activa el arreglo de sprites entra en loop
        while (isWalking)
        {
            yield return new WaitForSeconds(0.05f);
            spriteR.sprite = movingSprites[index];
            index++;
            if (index == movingSprites.Length)
            {
                index = 0;
            }
        }
    }
    IEnumerator JumpCoRoutine()
    {
    //Detiene animacion de caminata
    isWalking = false;
    //Loop del arreglo de sprites
    foreach (Sprite jumpSprite in jumpingSprites)
    {
        spriteR.sprite = jumpSprite;
        yield return new WaitForSeconds(0.1618033988f);
    }
    //Resetea la animacion de caminata
    isWalking = true;
    StartCoroutine(WalkCoRoutine());
    }
    IEnumerator ChangeCoRoutine()
    {
        isChangingDir = true;
        isWalking = false;
        FlipLogic();
        float cplayerSpeed = playerSpeed;
        playerSpeed = 0f;
        foreach (Sprite changeSprite in changingSprites)
        {
            spriteR.sprite = changeSprite;
            yield return new WaitForSeconds(0.1618033988f);
        }
        playerSpeed = cplayerSpeed;
        isWalking = true;
        StartCoroutine(WalkCoRoutine());
        isChangingDir = false;
    }
}
