using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]private float healthValue;
    private void OnTriggerEnter2D(Collider2D pick)
    {
        if(pick.tag == "Player")
        {
            pick.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
