using UnityEngine;

public class EnemyTrapS : EnemyTrapBase
{
    private bool movingL;
    private float leftEdge;
    private float rightEdge;
    protected override void Start()
    {
        base.Start();
        leftEdge = transform.position.x - movementDist;
        rightEdge = transform.position.x + movementDist;
    }

    protected void Update()
    {
        if (movingL)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }
            else
            {
                movingL = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
            else
            {
                movingL = true;
            }
        }
    }
}
