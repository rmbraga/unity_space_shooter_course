using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid2 : MonoBehaviour
{

    [SerializeField] private float _rotationSpeed = 19f;
    private Animator _animator;
    private int _shotsToDestroy = 0;
    private SpawnManager _spawnManager;

    public static event Action OnAsteroidDestroy;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        Utils.CheckIfGameObjectIsNull(_animator);
        Utils.CheckIfGameObjectIsNull(_spawnManager);
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
                OnAsteroidDestroy?.Invoke();
                _spawnManager.StartSpawning();
            }

        }
    }
}
