using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Weapon : MonoBehaviour, IWeapon
{
    public event Action<bool> OnWeaponReload;
    public event Action<int> OnBulletsAmountChanged
    {
        add
        {
            _onBulletsAmountChanged += value;
            _onBulletsAmountChanged(_maxBulletAmount);
        }
        remove { _onBulletsAmountChanged -= value; }
    }

    private event Action<int> _onBulletsAmountChanged;

    [SerializeField] private Transform _gunPoint;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private Sprite _weaponIcon;
    [SerializeField] private AnimationClip _weaponAnimation;
    [SerializeField] private int _maxBulletAmount;
    [SerializeField] private float _firingRate = 0.1f;
    [SerializeField] private AudioClip _shotClip;
    [SerializeField] private AnimationClip _weaponPose;

    [SerializeField] private WeaponTypes.WeaponType _weaponType;


    private AudioSource _audioSource;

    private float _reloadingRate = 1.5f; // switch from hardcode to event/animation event

    private int _currentBulletsAmount;
    private bool _isReloading = false;
    private bool _isShooting;
    private Vector3 _aimPosition;
    private IShootingType _shootingModule;

    private Coroutine _shootingCoroutine;
    private Coroutine _reloadingCoroutine;
    public void Awake()
    {
        _shootingModule = GetComponent<IShootingType>();
        _currentBulletsAmount = _maxBulletAmount;
        _audioSource = GetComponent<AudioSource>();

    }

    public void OnEnable()
    {
        _onBulletsAmountChanged?.Invoke(_currentBulletsAmount);

    }

    public void Shoot(bool isShooting)
    {
        _isShooting = isShooting;
        if (_isShooting && !_isReloading)
        {
            _shootingCoroutine = StartCoroutine(Shooting());
        }
        else
        {
            if (_shootingCoroutine != null) // maybe move to a method? used 2 times
            {
                StopCoroutine(_shootingCoroutine);
            }
        }
    }

    private IEnumerator Shooting()
    {
        while (true && !_isReloading)
        {
            if (_currentBulletsAmount > 0)
            {
                PlayShootSound(); //TODO: turn into 1 method
                _muzzleFlash.Play(); // move to ishooting module, resize particle system for different weapons and colors
                //Invoke Recoil animation event 

                _currentBulletsAmount--;

                _onBulletsAmountChanged?.Invoke(_currentBulletsAmount);

                _shootingModule.CreateShot(_aimPosition, _gunPoint.position);

                if (_currentBulletsAmount == 0)
                {

                    _reloadingCoroutine = StartCoroutine(Reloading());
                }
                yield return new WaitForSeconds(_firingRate);


            }
            else
            {
                _reloadingCoroutine = StartCoroutine(Reloading());
                yield return null;
            }
        }
    }

    private IEnumerator Reloading()
    {
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);
        }

        _isReloading = true;
        OnWeaponReload?.Invoke(_isReloading);

        yield return new WaitForSeconds(_reloadingRate);
        _currentBulletsAmount = _maxBulletAmount;

        _onBulletsAmountChanged?.Invoke(_currentBulletsAmount);

        _isReloading = false;
        OnWeaponReload?.Invoke(_isReloading);

        if (_isShooting)
        {
            _shootingCoroutine = StartCoroutine(Shooting());
            yield return null;
        }

    }

    public void Reload()
    {
        _reloadingCoroutine = StartCoroutine(Reloading());
    }

    public Sprite ReturnIcon()
    {
        return _weaponIcon;
    }

    public Sprite WeaponIcon()
    {
        return _weaponIcon;
    }

    public void ReceiveAim(Vector3 aim)
    {
        _aimPosition = aim;
    }

    public int ReturnBulletsAmount()
    {
        return _currentBulletsAmount;
    }

    public AnimationClip ReturnWeaponAnimation()
    {
        return _weaponAnimation;
    }

    public float SetOffset()
    {
        return _gunPoint.position.y;
    }

    private void PlayShootSound()
    {
        _audioSource.PlayOneShot(_shotClip);
    }

    public WeaponTypes.WeaponType WeaponType()
    {
        return _weaponType;
    }

    public void Unequip()
    {
        Destroy(gameObject);
    }


}
