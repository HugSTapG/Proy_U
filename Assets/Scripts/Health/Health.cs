using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]private float startingHealth;
    private float currentHealth;
    private bool dead;
    [Header("IFrames")]
    [SerializeField]private float iframesLength;
    [SerializeField]private int numFlashes;
    private SpriteRenderer redS;
    [Header("Components")]
    [SerializeField]private Behaviour[] components;
    private UIManager uiManager;
    private bool invulnerab;
    [Header("Audio")]
    [SerializeField]private AudioClip deathSound;
    [SerializeField]private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        redS = GetComponent<SpriteRenderer>();
        uiManager = FindObjectOfType<UIManager>();
    }
    public float StartingHealth()
    {
        return startingHealth;
    }
    public float CurrentHealth()
    {
        return currentHealth;
    }
    public bool IsDead()
    {
        return dead;
    }
    public void TakeDamage(float _damage)
    {
        if(invulnerab)
        {
            return;
        }
        currentHealth = Mathf.Clamp(currentHealth - _damage, 
        0, startingHealth);
        if( currentHealth > 0)
        {
            SoundManager.instance.PlaySound(hurtSound);
            //Cambia color por x tiempo/veces, invulnerabilidad
            StartCoroutine(Hurt());
        }
        else
        {
            if(!dead)
            {
                SoundManager.instance.PlaySound(deathSound);
                //Muerte General
                foreach(Behaviour component in components)
                {
                    component.enabled = false;
                    StartCoroutine(ActDeaFade(0.5f, true));
                }
                //Jugador Muerte - Desactivacion Scripts y Game Over
                if(GetComponent<PlayerController>() != null)
                {
                    GetComponent<PlayerController>().StopMovement();
                    uiManager.GameOver();
                }
                dead = true;
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, 
        startingHealth);
    }
    public IEnumerator Hurt()
    { 
        //Animacion al recibir dmg y invencibilidad
        invulnerab = true;
        Physics2D.IgnoreLayerCollision(8, 9 ,true);
        yield return StartCoroutine(Flash(1, 0, 0));
        Physics2D.IgnoreLayerCollision(8, 9 ,false);
        invulnerab = false;
    }
    public IEnumerator Flash(float va1, float va2, float va3)
    { 
        for(int i = 0; i < numFlashes; i++)
        {
            redS.color =  new Color(va1, va2, va3, .5f);
            yield return new WaitForSeconds(iframesLength/(numFlashes*2));
            redS.color = Color.white;
            yield return new WaitForSeconds(iframesLength/(numFlashes*2));
        }
    }
    private IEnumerator FadeGradually(float fadeTime, bool fadeOut)
    {
        float timer = 0f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        
        float startAlpha = fadeOut ? 1f : 0f;
        float targetAlpha = fadeOut ? 0f : 1f;

        while (timer < fadeTime)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeTime);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }
        // Ensure the final alpha value is set correctly
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    }
    private IEnumerator ActDeaFade(float fadeTime, bool fadeOut)
    {
        if(!fadeOut)
        {
            gameObject.SetActive(true);
        }
        yield return FadeGradually(fadeTime, fadeOut);
        if (fadeOut)
        {
            gameObject.SetActive(false);
        }
    }
    public void Respawn()
    {
        StartCoroutine(ActDeaFade(0.5f, false));
        dead = false;
        AddHealth(startingHealth);
        StartCoroutine(Flash(1, 0.5f, 0));
        GetComponent<PlayerController>().StartMovement();
        foreach(Behaviour component in components)
        {
            component.enabled = true;
        }
    }
}
