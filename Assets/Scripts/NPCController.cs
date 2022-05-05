using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    private float timeToChangeDirection;
    private float lifeDurationTimer;

    // Use this for initialization
    public void Start () 
    {
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
                targetPos.x += directionX == 1 ? 1f : -1f;
            }
            else
            {
                int directionY = Random.Range(0, 2);
                targetPos.y += directionY == 1 ? 1f : -1f;
            }

            StartCoroutine(Move(targetPos));
            timeToChangeDirection = 1.5f;
        }
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