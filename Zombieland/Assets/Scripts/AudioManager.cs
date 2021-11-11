using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioClip _mainTheme;
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
       AudioSource.PlayClipAtPoint(effect, transform.position);
    }
}
