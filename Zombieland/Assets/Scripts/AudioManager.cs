using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioClip _mainTheme;
    [SerializeField] private AudioClip _gameTheme;
    [SerializeField] private AudioClip _buttonClick;


    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonClick(AudioClip effect)
    {
        _audioSource.PlayOneShot(effect);
    }

    public void PlayGameTheme()
    {
        _audioSource.clip = _gameTheme;
        _audioSource.Play();
    }

    public void PlayMainTheme()
    {
        _audioSource.clip = _mainTheme;
        _audioSource.Play();
    }
}
