using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 10.0f;
    public bool vertical;
    public float changeTime = 10.0f;

    public ParticleSystem smokeEffect;

    Rigidbody2D rigidbody2;

    bool broken = true;

    float timer;
    int direction = 1;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    void FixedUpdate()
    {
        if (!broken)
        {
            return;

        }
        Vector2 position = rigidbody2.position;
        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
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
    public void Fix()
    {
        broken = false;
        rigidbody2.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
}
