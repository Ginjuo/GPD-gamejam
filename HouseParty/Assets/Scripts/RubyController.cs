using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    public float speed = 3.5f;
    private int _currentHealth;
    public int currentHealth => _currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    // <Drunk movement>
    public float changeTime = 0.5f; 
    float timer;                    
    int drunk_direction = 1;
    bool drunk_vert;
    public float drunk_speed_x;
    public float drunk_speed_y;
    Vector2 drunk_center;
    public bool char_move_x;
    public bool char_move_y;
    float drunk_acceleration;
    float drunk_speed_clamp_x;
    float drunk_speed_clamp_y;
    public float drunk_pos_clamp;
    // </Drunk movement>

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public int projectileForce = 300;
    public GameObject projectilePrefab;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    AudioSource audioSource;

    public AudioClip launchedClip;
    public AudioClip hitClip;
    // Start is called before the first frame update

    //Corona
    public int Contraction_chance;
    public bool HasCOVID;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        _currentHealth = maxHealth;

        timer = changeTime;                     // Drunk movement
        drunk_center = rigidbody2d.position;    // Drunk movement
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, projectileForce);

        PlaySound(launchedClip);
        animator.SetTrigger("Launch");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }  
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) // When someone is controlling
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();

            // Drunk movement
            drunk_center = rigidbody2d.position;
        }

        char_move_x = Mathf.Approximately(move.x, 0.0f) ? false : true;
        char_move_y = Mathf.Approximately(move.y, 0.0f) ? false : true;

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        // Drunk movement
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            drunk_direction = UnityEngine.Random.value > 0.5 ? - drunk_direction : drunk_direction;
            drunk_vert = UnityEngine.Random.value > 0.5 ? !drunk_vert : drunk_vert;
            timer = changeTime;
        }
    }

    private void GetCOVID(EnemyController NPC)
    {
        if (!HasCOVID && NPC != null && NPC.HasCOVID)
        {
            if (UnityEngine.Random.Range(0, 101) <= Contraction_chance)
            {
                HasCOVID = true;
                Debug.Log("Player got COVID from: " + NPC.Name);
            }
        }       
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GetCOVID(collider.gameObject.GetComponent<EnemyController>());
    }

    //void OnTriggerStay2D(Collider2D collider)
    //{
    //    if (isInvincible)
    //        return;

    //    isInvincible = true;
    //    invincibleTimer = timeInvincible;

    //    GetCOVID(collider.gameObject.GetComponent<EnemyController>());
        
    //}

    void FixedUpdate()
    {
        Console.WriteLine("FIX UPDATE PLAYER");
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        Drunk(ref position);

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            PlaySound(hitClip);
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        _currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
    public void Drunk(ref Vector2 position)
    {
        drunk_speed_clamp_x = char_move_x ? 1.5f : 0.8f;
        drunk_speed_clamp_y = char_move_y ? 1.5f : 0.8f;

        //Debug.Log("char_move_x" + char_move_x);
        //Debug.Log("char_move_y" + char_move_y);

        if (char_move_x || char_move_y)
        {
            drunk_acceleration = 0.2f;
            drunk_pos_clamp = 10f; // Just large enough to not interfere
        }
        else
        {
            drunk_acceleration = 0.05f;
            drunk_speed_clamp_x = 0.2f;
            drunk_speed_clamp_y = 0.2f;
            drunk_pos_clamp = 0.2f;
        }
        
        if (drunk_vert)
        {
            drunk_speed_y = drunk_speed_y + drunk_direction * drunk_acceleration;
            drunk_speed_y = Mathf.Clamp(drunk_speed_y, drunk_speed_clamp_y*-1, drunk_speed_clamp_y);
        }
        else
        {
            drunk_speed_x = drunk_speed_x + drunk_direction * drunk_acceleration;
            drunk_speed_x = Mathf.Clamp(drunk_speed_x, -drunk_speed_clamp_x, drunk_speed_clamp_x);
        }

        //Debug.Log("drunk_speed_x: " + drunk_speed_x);
        //Debug.Log("drunk_speed_y: " + drunk_speed_y);
        //Debug.Log("drunk_center" + drunk_center);

        float new_pos_y = position.y + Time.deltaTime * drunk_speed_y;
        float new_pos_x = position.x + Time.deltaTime * drunk_speed_x;
        position.y = Mathf.Clamp(new_pos_y, drunk_center.y-drunk_pos_clamp, drunk_center.y+drunk_pos_clamp);
        position.x = Mathf.Clamp(new_pos_x, drunk_center.x-drunk_pos_clamp, drunk_center.x+drunk_pos_clamp);

    }
}