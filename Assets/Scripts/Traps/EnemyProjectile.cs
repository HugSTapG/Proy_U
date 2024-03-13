using System.Collections;
using UnityEngine;

public class EnemyProjectile : Enemy_Damage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private SpriteRenderer projS;
    public Sprite[] projSprites;
    private int index = 0;
    private void Start()
    {
        projS = GetComponent<SpriteRenderer>();
    }
    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
        StartCoroutine(ProjCoRoutine());
    }
    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(0, -movementSpeed, 0);

        lifetime += Time.deltaTime;
        if(lifetime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }
    new private void OnTriggerEnter2D(Collider2D hit)
    {
        base.OnTriggerEnter2D(hit);
        gameObject.SetActive(false);
    }
    IEnumerator ProjCoRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        projS.sprite = projSprites[index];
        index++;
        if(index == projSprites.Length)
        {
            index = 0;
        }
        StartCoroutine(ProjCoRoutine());
    }
}
