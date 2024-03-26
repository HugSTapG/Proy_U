using System.Collections;
using UnityEngine;
public class Swarm : MonoBehaviour
{
    public Vector2 center;
    public Vector2 size;
    public GameObject[] collectibles;
    private int index = 0;

    void Awake()
    {
        Spawncollectibles();
    }

    public void Spawncollectibles()
    {
        StartCoroutine(GenCoRoutine());
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector3(center.x, center.y, 0), new Vector3(size.x, size.y, 1));
    }

    private IEnumerator GenCoRoutine()
    {
        yield return new WaitForSeconds(1f);
        
        Vector3 pos = new Vector3(
            center.x + Random.Range(-size.x / 2, size.x / 2),
            center.y + Random.Range(-size.y / 2, size.y / 2),
            0
        );

        GameObject collectible = Instantiate(collectibles[index], pos, Quaternion.identity);
        index++;

        if (index == collectibles.Length)
        {
            index = 0;
        }
        Destroy(collectible, 3f);
        StartCoroutine(GenCoRoutine());
    }
}
