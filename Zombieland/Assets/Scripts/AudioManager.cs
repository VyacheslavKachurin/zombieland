using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioClip _mainTheme;
    [SerializeField] private AudioClip _gameTheme;
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
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _mainTheme;
        _audioSource.Play();
    }

    public void PlayButtonClick(AudioClip effect)
    {
        //  AudioSource.PlayClipAtPoint(effect, transform.position);
        _audioSource.PlayOneShot(effect);
    }
    public void PlayGameTheme()
    {
        _audioSource.clip = _gameTheme;
        _audioSource.Play();
    }
}
