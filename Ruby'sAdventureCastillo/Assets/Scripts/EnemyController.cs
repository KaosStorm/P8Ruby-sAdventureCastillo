using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 10.0f;
    public bool vertical;
    public float changeTime = 10.0f;

    Rigidbody2D rigidbody2;

    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        timer = changeTime;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidbody2.MovePosition(position);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        NewBehaviourScript player = other.gameObject.GetComponent<NewBehaviourScript>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}