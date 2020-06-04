using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        // representa a posição inicial do player, dada a posição 
        //x = lateral, y = altura, z = profundidade
        transform.position = new Vector3(x: 0, y: 0, z: 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Usando a classe Input, conseguimos mapear as teclas que queremos usar no jogo
        // Observar na Unity > Edit > Project Settings > Input Manager > Axes...
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Aplicando deltaTime, é como se estivessemos nos movendo 1 metro
        // por segundo, dada a nossa velocidade...
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        transform.Translate(new Vector3(x: horizontalInput, y: verticalInput, z: 0) * _speed * Time.deltaTime);

        // Se a posição do player na posição y for maior que 0 > setar y = 0
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(x: transform.position.x, y: 0);
        }
        else if (transform.position.y <= -3.97f)
        {
            transform.position = new Vector3(x: transform.position.x, y: -3.97f);
        }

        if(transform.position.x <= -11.28f)
        {
            transform.position = new Vector3(x: 11.15f, y: transform.position.y);
        } else if(transform.position.x >= 11.15f)
        {
            transform.position = new Vector3(x: -11.28f, y: transform.position.y);
        }
    }
}
