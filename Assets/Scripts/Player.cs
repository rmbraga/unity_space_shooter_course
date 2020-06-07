using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(x: 0, y: 0, z: 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        Utils.CheckIfGameObjectIsNull(_spawnManager);
        Utils.CheckIfGameObjectIsNull(_uiManager);
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

        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + (_laserPrefab.transform.up * 1.05f), Quaternion.identity);
        }
    }

    public void Damage()
    {
        if(_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives -= 1;
        _uiManager.updateLives(_lives);

        if (_lives == 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostActive()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldDownRoutine());
    }

    IEnumerator ShieldDownRoutine()
    {
        yield return new WaitForSeconds(8);
        _isShieldActive = false;
        _shieldVisualizer.SetActive(false);
    }

    public void AddScore(int points)
    {
        _score += 10;
        _uiManager.updateScore(_score);
    }
}
