using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour,IPointerEnterHandler
{
    [SerializeField] private AudioClip _hoverEffect;
    [SerializeField] private AudioClip _clickEffect;

    private AudioSource _audioSource;
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();

        _button.onClick.AddListener(PlayClickFx);


    }
    private void PlayHoverFX()
    {
        _audioSource.PlayOneShot(_hoverEffect);
    }
    private void PlayClickFx()
    {
        AudioManager.Instance.PlayButtonClick(_clickEffect);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverFX();
    }
}
