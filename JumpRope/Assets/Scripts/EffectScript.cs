using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    [SerializeField] ParticleSystem _r_system;
    [SerializeField] Light _leftPointLight;
    [SerializeField] Light _rightPointLight;
    [SerializeField] Light _spotLight;
    [SerializeField] Material _ropeMaterial;
    [SerializeField] Material _colorProtector;
    [SerializeField] ParticleSystem _comboSystem;
    [SerializeField] GameObject _player;

    PlayerScoreCounter _playerScoreCounter;
    Renderer _renderer;
    AudioSource groundHit;


    void Start()
    {
        _playerScoreCounter = _player.GetComponent<PlayerScoreCounter>();
        groundHit = GetComponent<AudioSource>();
        
        _leftPointLight.color = _colorProtector.color;
        _rightPointLight.color = _colorProtector.color;
        _spotLight.color = _colorProtector.color;
        _ropeMaterial.color = _colorProtector.color;
        _ropeMaterial.SetColor("_EmissionColor", _colorProtector.color);
        _comboSystem.startColor = _colorProtector.color;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rope"))
        {
            _r_system.Play();
            groundHit.Play();
        }
    }


}
