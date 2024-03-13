using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPos;
    private void Awake()
    {
        //Save initial position of all enemies
        initialPos = new Vector3[enemies.Length];
        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
            {
                initialPos[i] = enemies[1].transform.position;   
            }
        }
    }
    public void ActivateRoom(bool _status)
    {
        //Activate or deactivate enemies on the room
        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPos[i];   
            }
        }
    }
}
