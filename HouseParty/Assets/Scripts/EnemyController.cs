using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 0;
    public ParticleSystem smokeEffect;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    private bool broken = true;

    private Animator animator;
    // <Drunk movement>
    int drunk_direction = 1;
    bool drunk_vert;
    float drunk_speed_x;
    float drunk_speed_y;
    Vector2 drunk_center;
    public float drunk_pos_clamp; //0.2f

    public float drunk_speed_clamp; //0.1f

    public float drunk_acceleration; //0.2f
    // </Drunk movement>

    private Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        drunk_center = rigidbody2D.position;
        drunk_direction = Random.value > 0.5 ? -drunk_direction : drunk_direction;
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
            //position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", drunk_direction);
        }
        else
        {
            //position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", drunk_direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        smokeEffect.Stop();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Drunk(ref Vector2 position)
    {


        drunk_speed_y = Mathf.Clamp(drunk_speed_y + drunk_direction * drunk_acceleration, -drunk_speed_clamp, drunk_speed_clamp);
        
        drunk_speed_x = Mathf.Clamp(drunk_speed_x + drunk_direction * drunk_acceleration, -drunk_speed_clamp, drunk_speed_clamp);
        
        position.y = Mathf.Clamp(position.y + Time.deltaTime * drunk_speed_y, drunk_center.y - drunk_pos_clamp, drunk_center.y + drunk_pos_clamp);
        position.x = Mathf.Clamp(position.x + Time.deltaTime * drunk_speed_x, drunk_center.x - drunk_pos_clamp, drunk_center.x + drunk_pos_clamp);
    }
}
