using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class RubyController : CovidLogic
    {
        public float Speed = 3.5f;

        public float TimeInvincible = 2.0f;
        private bool _isInvincible;
        private float _invincibleTimer;

        private Animator _animator;
        private Vector2 _lookDirection = new Vector2(1, 0);

        private Rigidbody2D _rigidbody2d;
        private float _horizontal;
        private float _vertical;

        // >Drunk movement>
        public float drunk_time_x = 0f;
        public float drunk_time_y = 0f;
        public float drunk_loop_rate = 0.003f;
        public float drunk_amplitude = 0.005f;
        // </Drunk movement>

        // World boundaries
        private float minX, maxX, minY, maxY;

        // Audio stuff
        private AudioSource _audioSource;
        public AudioClip LoadClip;

        // Start is called before the first frame update

        //Corona
        //public int Contraction_chance;
        //public bool HasCOVID;
        void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
            Name = "player";

            PlaySound(LoadClip);

            // World boundaries
            float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
            Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0,0, camDistance));
            Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1,1, camDistance));
            
            minX = bottomCorner.x;
            maxX = topCorner.x;
            minY = bottomCorner.y;
            maxY = topCorner.y;
        }

        public void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        void Launch()
        {
            //GameObject projectileObject = Instantiate(projectilePrefab, _rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

            //Projectile projectile = projectileObject.GetComponent<Projectile>();
            //projectile.Launch(_lookDirection, ProjectileForce);

            _animator.SetTrigger("Launch");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                RaycastHit2D hit = Physics2D.Raycast(_rigidbody2d.position + Vector2.up * 0.2f, _lookDirection, 1.5f, LayerMask.GetMask("NPC"));
                if (hit.collider != null)
                {
                    NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                    if (character != null)
                    {
                        //character.DisplayDialog();
                    }  
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Launch();
            }

            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");

            Vector2 move = new Vector2(_horizontal, _vertical);

            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) // When someone is controlling
            {
                _lookDirection.Set(move.x, move.y);
                _lookDirection.Normalize();

                DrunkMovingUpdate();
            }
            else
            {   
                DrunkIdleUpdate();
            }

            _animator.SetFloat("Look X", _lookDirection.x);
            _animator.SetFloat("Look Y", _lookDirection.y);
            _animator.SetFloat("Speed", move.magnitude);

            if (_isInvincible)
            {
                _invincibleTimer -= Time.deltaTime;
                if (_invincibleTimer < 0)
                    _isInvincible = false;
            }

            // <World boundaries>
            // Get current position
            Vector3 pos = transform.position;
    
            // Horizontal contraint
            if(pos.x < minX) pos.x = minX;
            if(pos.x > maxX) pos.x = maxX;
    
            // vertical contraint
            if(pos.y < minY) pos.y = minY;
            if(pos.y > maxY) pos.y = maxY;
    
            // Update position
            transform.position = pos;
            // </World boundaries>
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            CovidLogic cl = collider.gameObject.GetComponent<CovidLogic>();
            if (cl == null)
                return;
            GetCovid(cl);
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
            Vector2 position = _rigidbody2d.position;
            position.x = position.x + Speed * _horizontal * Time.deltaTime;
            position.y = position.y + Speed * _vertical * Time.deltaTime;

            Drunk(ref position);

            _rigidbody2d.MovePosition(position);
        }
        public void Drunk(ref Vector2 position)
        {
            // Calculate displacement
            
            float y_add = drunk_amplitude * Mathf.Sin(drunk_time_y*2f*Mathf.PI);
            float x_add = drunk_amplitude * Mathf.Sin(drunk_time_x*2f*Mathf.PI + 0.5f*Mathf.PI);

            // Add displacement
            position.y = position.y + y_add;
            position.x = position.x + x_add;

            // Update drunk_time
            drunk_time_y = drunk_time_y + 2.4f*drunk_loop_rate; 
            drunk_time_y = drunk_time_y > 1f ? 0 : drunk_time_y;

            drunk_time_x = drunk_time_x + drunk_loop_rate; 
            drunk_time_x = drunk_time_x > 1f ? 0 : drunk_time_x;
        }
        public void DrunkMovingUpdate()
        {
            drunk_amplitude = 0.03f;
            drunk_loop_rate = 0.005f;
        }
        public void DrunkIdleUpdate()
        {
            drunk_amplitude = 0.005f;
            drunk_loop_rate = 0.003f;
        }
    }
}