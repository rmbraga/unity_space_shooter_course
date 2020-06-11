using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    enum PowerupTypeEnum { TRIPLE_SHOT, SPEED_BOOST, SHIELD, NONE }

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private PowerupTypeEnum _powerupType;
    [SerializeField]
    AudioClip _explosionAudioClip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= MapBordersConstants.yBottom)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_explosionAudioClip, transform.position);

            if (player != null)
            {
                switch (_powerupType)
                {
                    case PowerupTypeEnum.TRIPLE_SHOT:
                        player.TripleShotActive();
                        break;
                    case PowerupTypeEnum.SPEED_BOOST:
                        player.SpeedBoostActive();
                        Debug.Log("SPEED BOOST ACTIVATED!");
                        break;
                    case PowerupTypeEnum.SHIELD:
                        player.ShieldActive();
                        Debug.Log("SHIELD ACTIVATED!");
                        break;
                    default:
                        _powerupType = PowerupTypeEnum.NONE;
                        Debug.Log("No powerup!");
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
