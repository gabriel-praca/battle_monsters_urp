using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour 
{
    public List<LayerMask> solidLayers;

    private float timeToChangeDirection;
    private float lifeDurationTimer;

    private Vector3 originalPosition;

    // Use this for initialization
    public void Start () 
    {
        //limit of walk
        originalPosition = transform.position;
        //self destroy after some time
        Destroy(gameObject, 15f);
    }
    
    // Update is called once per frame
    public void Update ()
    {
        timeToChangeDirection -= Time.deltaTime;

        if (timeToChangeDirection <= 0)
        {
            //remove diagonal movements with direction variable
            var targetPos = transform.position;
            int direction = Random.Range(0, 2);

            if(direction == 1) 
            {
                int directionX = Random.Range(0, 2);
                if(directionX == 1)
                {
                    if(targetPos.x + 1 > originalPosition.x + 3)
                    {
                        targetPos.x -= 1;
                    }
                    else
                    {
                        targetPos.x += 1;
                    }
                }
                else
                {
                    if(targetPos.x - 1 < originalPosition.x - 3)
                    {
                        targetPos.x += 1;
                    }
                    else
                    {
                        targetPos.x -= 1;
                    }
                }
                //targetPos.x += directionX == 1 ? 1f : -1f;
            }
            else
            {
                int directionY = Random.Range(0, 2);
                if(directionY == 1)
                {
                    if(targetPos.y + 1 > originalPosition.y + 3)
                    {
                        targetPos.y -= 1;
                    }
                    else
                    {
                        targetPos.y += 1;
                    }
                }
                else
                {
                    if(targetPos.y - 1 < originalPosition.y - 3)
                    {
                        targetPos.y += 1;
                    }
                    else
                    {
                        targetPos.y -= 1;
                    }
                }
                // targetPos.y += directionY == 1 ? 1f : -1f;
            }

            if(IsWalkable(targetPos))
            {
                StartCoroutine(Move(targetPos));
                timeToChangeDirection = 1.5f;
            }
        }
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        bool canWalk = true;
        foreach(LayerMask layer in solidLayers)
        {
            if (Physics2D.OverlapCircle(targetPos, 0.2f, layer) != null)
            {
                canWalk = false;
            }
        }
        return canWalk;
    }

    IEnumerator Move(Vector3 targetPos)
    {
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5 * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
    }
}