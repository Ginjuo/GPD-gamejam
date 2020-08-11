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

        // <Drunk movement>
        public float ChangeTime = 0.5f; 
        private float _timer;                    
        private int _drunkDirection = 1;
        private bool _drunkVert;
        public float DrunkSpeedX;
        public float DrunkSpeedY;
        private Vector2 _drunkCenter;
        public bool CharMoveX;
        public bool CharMoveY;
        private float _drunkAcceleration;
        private float _drunkSpeedClampX;
        private float _drunkSpeedClampY;
        public float DrunkPosClamp;
        // </Drunk movement>

        private Animator _animator;
        private Vector2 _lookDirection = new Vector2(1, 0);

        private Rigidbody2D _rigidbody2d;
        private float _horizontal;
        private float _vertical;

        private AudioSource _audioSource;

        public AudioClip LaunchedClip;
        public AudioClip HitClip;
        // Start is called before the first frame update

        //Corona
        //public int Contraction_chance;
        //public bool HasCOVID;
        void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _timer = ChangeTime;                     // Drunk movement
            _drunkCenter = _rigidbody2d.position;    // Drunk movement
            _audioSource = GetComponent<AudioSource>();
            Name = "player";
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

            PlaySound(LaunchedClip);
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
                        character.DisplayDialog();
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

                // Drunk movement
                _drunkCenter = _rigidbody2d.position;
            }

            CharMoveX = !Mathf.Approximately(move.x, 0.0f);
            CharMoveY = !Mathf.Approximately(move.y, 0.0f);

            _animator.SetFloat("Look X", _lookDirection.x);
            _animator.SetFloat("Look Y", _lookDirection.y);
            _animator.SetFloat("Speed", move.magnitude);

            if (_isInvincible)
            {
                _invincibleTimer -= Time.deltaTime;
                if (_invincibleTimer < 0)
                    _isInvincible = false;
            }

            // Drunk movement
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                _drunkDirection = UnityEngine.Random.value > 0.5 ? - _drunkDirection : _drunkDirection;
                _drunkVert = UnityEngine.Random.value > 0.5 ? !_drunkVert : _drunkVert;
                _timer = ChangeTime;
            }
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
            _drunkSpeedClampX = CharMoveX ? 1.5f : 0.8f;
            _drunkSpeedClampY = CharMoveY ? 1.5f : 0.8f;

            //Debug.Log("char_move_x" + char_move_x);
            //Debug.Log("char_move_y" + char_move_y);

            if (CharMoveX || CharMoveY)
            {
                _drunkAcceleration = 0.2f;
                DrunkPosClamp = 10f; // Just large enough to not interfere
            }
            else
            {
                _drunkAcceleration = 0.05f;
                _drunkSpeedClampX = 0.2f;
                _drunkSpeedClampY = 0.2f;
                DrunkPosClamp = 0.2f;
            }
        
            if (_drunkVert)
            {
                DrunkSpeedY = DrunkSpeedY + _drunkDirection * _drunkAcceleration;
                DrunkSpeedY = Mathf.Clamp(DrunkSpeedY, _drunkSpeedClampY*-1, _drunkSpeedClampY);
            }
            else
            {
                DrunkSpeedX = DrunkSpeedX + _drunkDirection * _drunkAcceleration;
                DrunkSpeedX = Mathf.Clamp(DrunkSpeedX, -_drunkSpeedClampX, _drunkSpeedClampX);
            }

            //Debug.Log("drunk_speed_x: " + drunk_speed_x);
            //Debug.Log("drunk_speed_y: " + drunk_speed_y);
            //Debug.Log("drunk_center" + drunk_center);

            float new_pos_y = position.y + Time.deltaTime * DrunkSpeedY;
            float new_pos_x = position.x + Time.deltaTime * DrunkSpeedX;
            position.y = Mathf.Clamp(new_pos_y, _drunkCenter.y-DrunkPosClamp, _drunkCenter.y+DrunkPosClamp);
            position.x = Mathf.Clamp(new_pos_x, _drunkCenter.x-DrunkPosClamp, _drunkCenter.x+DrunkPosClamp);

        }
    }
}