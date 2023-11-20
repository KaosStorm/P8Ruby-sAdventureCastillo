using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 10.0f;

    public float timeInvincible = 2;

    public int maxHealth = 5;

    public int health { get { return currentHealth; } }
    int currentHealth;

    bool isInvincible;
    float InvincibleTimer;

   Rigidbody2D rigidbody2D;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
         horizontal = Input.GetAxis("Horizontal");
         vertical = Input.GetAxis("Vertical");

        if (isInvincible)
        {
            InvincibleTimer -= Time.deltaTime;
            if( InvincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

    }
    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

       rigidbody2D.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if(amount > 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            InvincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
