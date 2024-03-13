using UnityEngine;

public class EnemyProjHolder : MonoBehaviour
{
    [SerializeField]private Transform enemy;
    private void Update()
    {
        transform.localScale = enemy.localScale;
    }
}
