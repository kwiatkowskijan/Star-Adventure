using StarAdventure.Interface;
using UnityEngine;
using StarAdventure.Managers;

namespace StarAdventure.Player
{
    public class PlayerController : MonoBehaviour, IGameEndListener
    {
        private Rigidbody2D _rb;
        [SerializeField] private float speed;
        private float _verticalSpeed;
        private Animator _animator;
        private AudioSource _audioSource;
        [SerializeField] private AudioClip deathClip;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            _audioSource = GetComponent<AudioSource>();
            GameManager.Instance.RegisterListener(this);
        }

        private void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            _verticalSpeed = Input.GetAxis("Vertical") * speed;
            _rb.velocity = new Vector2(0, _verticalSpeed);

            if(_rb.velocity.magnitude > 0 || _rb.velocity.magnitude < 0 )
            {
                _animator.SetBool("IsMoving", true);
            }
            else
            {
                _animator.SetBool("IsMoving", false);
            }
        }

        private void DeathAnimation()
        {
            _animator.SetTrigger("Death");
        }

        private void AssignClip(AudioClip clip)
        {
            _audioSource.clip = clip;
        }

        private void PlayAudio()
        {
            _audioSource.Play();
        }

        public void OnGameEnd()
        {
            AssignClip(deathClip);
            PlayAudio();
            DeathAnimation();
        }
    }
}
