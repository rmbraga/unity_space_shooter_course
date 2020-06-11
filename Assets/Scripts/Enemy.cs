using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;
    private Animator _animator;
    private BoxCollider2D _boxCollider2d;
    [SerializeField]
    AudioSource _explosionAudioSource;
    [SerializeField]
    AudioClip _explosionAudioClip;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _boxCollider2d = GetComponent<BoxCollider2D>();
        _explosionAudioSource = GetComponent<AudioSource>();

        Utils.CheckIfGameObjectIsNull(_player);
        Utils.CheckIfGameObjectIsNull(_animator);
        Utils.CheckIfGameObjectIsNull(_boxCollider2d);
        Utils.CheckIfGameObjectIsNull(_explosionAudioSource);
    }

    // Update is called once per frame
    void Update()
    {
        // mover 4mts/s pra baixo
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // se chegar no final da tela, renascer no topo da tela com uma nova posição randomica na posição x
        if (transform.position.y <= MapBordersConstants.yBottom)
        {
            transform.position = new Vector3(x: Random.Range(-9.45f, 9.45f), y: 13f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _animator.SetTrigger("OnEnemyDeath");
            _boxCollider2d.enabled = false;
            Destroy(this.gameObject, 2.3f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }

            _animator.SetTrigger("OnEnemyDeath");
            _boxCollider2d.enabled = false;
            _explosionAudioSource.PlayOneShot(_explosionAudioClip);
            Destroy(this.gameObject, 2.3f);
        }
    }
}
