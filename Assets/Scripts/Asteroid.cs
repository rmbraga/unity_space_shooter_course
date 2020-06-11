using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float _rotationSpeed = 19f;
    private Animator _animator;
    private int _shotsToDestroy = 0;
    private SpawnManager _spawnManager;
    [SerializeField]
    AudioSource _explosionAudioSource;
    [SerializeField]
    AudioClip _explosionAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _explosionAudioSource = GetComponent<AudioSource>();

        Utils.CheckIfGameObjectIsNull(_animator);
        Utils.CheckIfGameObjectIsNull(_spawnManager);
        Utils.CheckIfGameObjectIsNull(_explosionAudioSource);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _shotsToDestroy++;
            Destroy(other.gameObject);

            if (_shotsToDestroy == 3)
            {
                _animator.SetTrigger("OnEnemyDeath");
                Destroy(this.gameObject, 2.3f);
                _explosionAudioSource.PlayOneShot(_explosionAudioClip);
                _spawnManager.StartSpawning();
            }

        }
    }
}
