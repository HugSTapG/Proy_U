using System.Collections;
using UnityEngine;

public class Shoot_Anim : MonoBehaviour
{
    [Header("Animation")]
    private SpriteRenderer shootS;
    public Sprite[] shS;
    private int index = 0;
    private void Start()
    {
        shootS = GetComponent<SpriteRenderer>();
        StartCoroutine(ShootCoRoutine());
    }
    IEnumerator ShootCoRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        shootS.sprite = shS[index];
        index++;
        if(index == shS.Length)
        {
            index = 0;
        }
        StartCoroutine(ShootCoRoutine());
    }
}
