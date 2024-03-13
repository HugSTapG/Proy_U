using System.Collections;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private SpriteRenderer coinS;
    public Sprite[] rotS;
    private int index = 0;
    void Start()
    {
        coinS = GetComponent<SpriteRenderer>();
        StartCoroutine(BlinkCoRoutine());
    }
    IEnumerator BlinkCoRoutine()
    {
        yield return new WaitForSeconds(0.1618033988f);
        coinS.sprite = rotS[index];
        index++;
        if(index == rotS.Length)
        {
            index = 0;
        }
        StartCoroutine(BlinkCoRoutine());
    }
  
}
