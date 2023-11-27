using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 10.0f;

    public float timeInvincible = 2.0f;

    public int maxHealth = 5;

    public GameObject projectilePrefab;

    public int health { get { return currentHealth; } }
    int currentHealth;

    bool isInvincible;
    float InvincibleTimer;

   Rigidbody2D rigidbody2;
    float horizontal;
    float vertical;

    Animator animator; 
    Vector2 lookDirection = new Vector2(1, 0);

    AudioSource audioSource;
    public AudioClip throwSound;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
         horizontal = Input.GetAxis("Horizontal");
         vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            InvincibleTimer -= Time.deltaTime;
            if (InvincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if(hit.collider != null)
            {
               NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

    }
     void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible) 
            {
                return;
            }
            isInvincible = true;
            InvincibleTimer = timeInvincible;
            Playsound(hitSound);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth/(float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectil projectile = projectileObject.GetComponent<Projectil>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        Playsound(throwSound);
    }

    public void Playsound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
