using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyController : ObjectiveLogic
    {
        //Random
        public float ChangeTime = 0;
        private Rigidbody2D _rigidbody2D;
        private float _timer;
        private bool _broken = true;
        private Animator _animator;
        public TextMeshProUGUI NameText;

        // <Drunk movement>
        public float DrunkPosClamp; //0.2f    
        public float DrunkSpeedClamp; //0.1f
        public float DrunkAcceleration; //0.2f
        private int _drunkDirection = 1;
        private bool _drunkVert;
        private float _drunkSpeedX;
        private float _drunkSpeedY;
        private Vector2 _drunkCenter;
        // </Drunk movement>

        //Corona
        //public int ContractionChance;
        //public bool HasCovid;
        public float TimeInvincible = 2.0f;
        private bool _isInvincible;
        private float _invincibleTimer;

        // Audio stuff
        //private AudioSource _audioSource;
        //public AudioClip TalkAudioClip;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _timer = ChangeTime;
            _animator = GetComponent<Animator>();
            _drunkCenter = _rigidbody2D.position;
            _drunkDirection = Random.value > 0.5 ? -_drunkDirection : _drunkDirection;
            NameText.text = GameController.Instance.GetName(HandlesObjective);
            Name = NameText.text;

            //_audioSource = GetComponent<AudioSource>();

            HandlesObjective = GameController.Instance.GetSpecificObjectiveNumber(Name);
            HandleDialogInit();
        }

        void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer < 0)
            {
                _drunkDirection = Random.value > 0.5 ? -_drunkDirection : _drunkDirection;
                _drunkVert = Random.value > 0.5 ? !_drunkVert : _drunkVert;
                _timer = ChangeTime;
            }

            if (_isInvincible)
            {
                _invincibleTimer -= Time.deltaTime;
                if (_invincibleTimer < 0)
                    _isInvincible = false;
            }

            HandleDialogTimerLogic();
        }

        void FixedUpdate()
        {
            if (!_broken)
            {
                return;
            }
            Vector2 position = _rigidbody2D.position;
            Drunk(ref position);

            if (_drunkVert)
            {
                _animator.SetFloat("Move X", 0);
                _animator.SetFloat("Move Y", _drunkDirection);
            }
            else
            {
                _animator.SetFloat("Move X", _drunkDirection);
                _animator.SetFloat("Move Y", 0);
            }

            _rigidbody2D.MovePosition(position);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            //PlaySound(TalkAudioClip);

            if (ShowsDialog && collider.gameObject.GetComponent<RubyController>() != null)
                HandleObjective();

            CovidLogic cl = collider.gameObject.GetComponent<CovidLogic>();
            if (cl == null)
                return;
            GetCovid(cl);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            //if (!ShowsDialog && collider.gameObject.GetComponent<RubyController>() != null)
            //    return;
            //DialogBox.SetActive(false);
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
            _drunkSpeedY = Mathf.Clamp(_drunkSpeedY + _drunkDirection * DrunkAcceleration, -DrunkSpeedClamp, DrunkSpeedClamp);
        
            _drunkSpeedX = Mathf.Clamp(_drunkSpeedX + _drunkDirection * DrunkAcceleration, -DrunkSpeedClamp, DrunkSpeedClamp);
        
            position.y = Mathf.Clamp(position.y + Time.deltaTime * _drunkSpeedY, _drunkCenter.y - DrunkPosClamp, _drunkCenter.y + DrunkPosClamp);
            position.x = Mathf.Clamp(position.x + Time.deltaTime * _drunkSpeedX, _drunkCenter.x - DrunkPosClamp, _drunkCenter.x + DrunkPosClamp);
        }
    }
}
