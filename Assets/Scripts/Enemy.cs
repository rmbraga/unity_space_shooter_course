using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // mover 4mts/s pra baixo
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // se chegar no final da tela, renascer no topo da tela com uma nova posição randomica na posição x
        if (transform.position.y <= -5.38f)
        {
            transform.position = new Vector3(x: Random.Range(-9.45f, 9.45f), y: 7f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
