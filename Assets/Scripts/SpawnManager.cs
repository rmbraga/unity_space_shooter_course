using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;

    private bool _isPlayerAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_isPlayerAlive)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(x: Random.Range(-9.45f, 9.45f), y: 7f), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_isPlayerAlive)
        {
            Instantiate(_powerups[Random.Range(0, _powerups.Length)], new Vector3(x: Random.Range(-9.45f, 9.45f), y: 7f), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _isPlayerAlive = false;
    }
}
