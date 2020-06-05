using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(x: 0, y: 0, z: 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        // Usando a classe Input, conseguimos mapear as teclas que queremos usar no jogo
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Aplicando deltaTime, é como se estivessemos nos movendo 1 metro
        // por segundo, dada a nossa velocidade...
        transform.Translate(new Vector3(x: horizontalInput, y: verticalInput, z: 0) * _speed * Time.deltaTime);

        // limita o player entre a posição -3.97 e 0, utilizando a função Clamp da Mathf
        transform.position = new Vector3(x: transform.position.x, y: Mathf.Clamp(transform.position.y, -3.97f, 0));

        if (transform.position.x <= -11.28f)
        {
            transform.position = new Vector3(x: 11.15f, y: transform.position.y);
        }
        else if (transform.position.x >= 11.15f)
        {
            transform.position = new Vector3(x: -11.28f, y: transform.position.y);
        }

        transform.position = new Vector3(x: Mathf.Clamp(transform.position.x, -11.28f, 11.15f), y: transform.position.y);
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + (_laserPrefab.transform.up * 0.8f), Quaternion.identity);
    }
}
