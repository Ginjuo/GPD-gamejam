using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Random
    public float changeTime = 0;
    public string Name;
    public Canvas nameText;
    private Rigidbody2D rigidbody2D;
    private float timer;
    private bool broken = true;
    private Animator animator;

    // <Drunk movement>
    public float drunk_pos_clamp; //0.2f    
    public float drunk_speed_clamp; //0.1f
    public float drunk_acceleration; //0.2f
    private int drunk_direction = 1;
    private bool drunk_vert;
    private float drunk_speed_x;
    private float drunk_speed_y;
    private Vector2 drunk_center;
    // </Drunk movement>

    //Corona
    public int Contraction_chance;
    public bool HasCOVID;
    public float timeInvincible = 2.0f;
    private bool isInvincible;
    private float invincibleTimer;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        drunk_center = rigidbody2D.position;
        drunk_direction = Random.value > 0.5 ? -drunk_direction : drunk_direction;
        nameText.GetComponentInChildren<TextMeshProUGUI>().text = Name;

    }

    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            drunk_direction = Random.value > 0.5 ? -drunk_direction : drunk_direction;
            drunk_vert = Random.value > 0.5 ? !drunk_vert : drunk_vert;
            timer = changeTime;
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        Vector2 position = rigidbody2D.position;
        Drunk(ref position);

        if (drunk_vert)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", drunk_direction);
        }
        else
        {
            animator.SetFloat("Move X", drunk_direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    private void GetCOVID(Collider2D collider)
    {
        if (HasCOVID)
            return;

        EnemyController NPC = collider.gameObject.GetComponent<EnemyController>();
        RubyController player = collider.gameObject.GetComponent<RubyController>();
        string transmitterName = null;
        if (NPC != null && NPC.HasCOVID)
            transmitterName = NPC.Name;

        if (player != null && player.HasCOVID)
            transmitterName = "player";

        if (transmitterName != null && Random.Range(0, 101) <= Contraction_chance)
        {
            HasCOVID = true;
            Debug.Log($"{Name} got COVID from: " + transmitterName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GetCOVID(collider);
    }

    //void OnTriggerStay2D(Collider2D collider)
    //{
    //    if (isInvincible)
    //        return;

    //    isInvincible = true;
    //    invincibleTimer = timeInvincible;

    //    GetCOVID(collider);

    //}

    public void Drunk(ref Vector2 position)
    {

        drunk_speed_y = Mathf.Clamp(drunk_speed_y + drunk_direction * drunk_acceleration, -drunk_speed_clamp, drunk_speed_clamp);
        
        drunk_speed_x = Mathf.Clamp(drunk_speed_x + drunk_direction * drunk_acceleration, -drunk_speed_clamp, drunk_speed_clamp);
        
        position.y = Mathf.Clamp(position.y + Time.deltaTime * drunk_speed_y, drunk_center.y - drunk_pos_clamp, drunk_center.y + drunk_pos_clamp);
        position.x = Mathf.Clamp(position.x + Time.deltaTime * drunk_speed_x, drunk_center.x - drunk_pos_clamp, drunk_center.x + drunk_pos_clamp);
    }
}
