using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject battleMonster;

    private bool isSpawnable;
    private float spawnerTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnerTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        spawnerTimer -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        isSpawnable = true;
    }
    private void OnTriggerExit2D(Collider2D collider) 
    {
        isSpawnable = false;        
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (isSpawnable && spawnerTimer <= 0)
        {
            var position = transform.position;
            //ver uma forma de espawnar em lugares aleatórios dentro de um range
            position.y += 0.5f;

            Instantiate(battleMonster, position, Quaternion.identity);
            spawnerTimer = 5f;
        }
    }
}
