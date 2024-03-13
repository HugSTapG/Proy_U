using UnityEngine;

public class EnemyTrapU : EnemyTrapBase
{
    private bool movingUp;
    private float topEdge;
    private float bottomEdge;
    protected override void Start()
    {
        base.Start();
        topEdge = transform.position.y + movementDist;
        bottomEdge = transform.position.y - movementDist;
    }

    protected void Update()
    {
        if (movingUp)
        {
            if (transform.position.y < topEdge)
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
            else
            {
                movingUp = false;
            }
        }
        else
        {
            if (transform.position.y > bottomEdge)
            {
                transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            }
            else
            {
                movingUp = true;
            }
        }
    }
}
