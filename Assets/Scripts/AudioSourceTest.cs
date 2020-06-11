using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceTest : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void OnEnable() => Asteroid2.OnAsteroidDestroy += PlaySound;

    private void OnDisable() => Asteroid2.OnAsteroidDestroy -= PlaySound;

    private void PlaySound() => _audioSource.Play();
}
