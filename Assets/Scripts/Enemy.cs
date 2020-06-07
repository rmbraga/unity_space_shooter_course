using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        Utils.CheckIfGameObjectIsNull(_player);
    }

    // Update is called once per frame
    void Update()
    {
        // mover 4mts/s pra baixo
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // se chegar no final da tela, renascer no topo da tela com uma nova posição randomica na posição x
        if (transform.position.y <= MapBordersConstants.yBottom)
        {
            transform.position = new Vector3(x: Random.Range(-9.45f, 9.45f), y: 7f);
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

            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            
            Destroy(this.gameObject);
        }
    }
}
