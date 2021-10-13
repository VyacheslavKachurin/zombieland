using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour,IWeapon
{
    public event Action<bool> OnWeaponReload;
    public event Action<int> OnBulletsAmountChanged
    {
        add { 
            _onBulletsAmountChanged += value; 
            _onBulletsAmountChanged(_maxBulletAmount);
        }
        remove { _onBulletsAmountChanged -= value; }
    }

    private event Action<int> _onBulletsAmountChanged;
   
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private Sprite _weaponIcon;

    private float _bulletVelocity = 20f;
    private float _firingRate = 0.15f;
    private float _reloadingRate = 1.5f; // switch from hardcode to event/animation event
    private int _currentBulletsAmount;
    private int _maxBulletAmount=5;
    private bool _isReloading=false;
    private bool _isShooting;
    private Vector3 _aimPosition;
 
    private Coroutine _shootingCoroutine;
    private Coroutine _reloadingCoroutine;
    public void Start()
    {
        _currentBulletsAmount = _maxBulletAmount;
    }
    public void OnEnable()
    { 
        _onBulletsAmountChanged?.Invoke(_currentBulletsAmount);
    }
    public void Shoot(bool isShooting)
    {
       
            _isShooting = isShooting;
            if (_isShooting&&!_isReloading)
            {
                _shootingCoroutine = StartCoroutine(Shooting());
            }
            else
            {
                StopCoroutine(_shootingCoroutine);
            }
        
    }
    private IEnumerator Shooting()
    {
        while (true&&!_isReloading)
        {
            if (_currentBulletsAmount > 0)
            {
                _muzzleFlash.Play();
                //Invoke Recoil animation event 

                _currentBulletsAmount--;

                _onBulletsAmountChanged?.Invoke(_currentBulletsAmount);

                CreateShot(_aimPosition);

                if (_currentBulletsAmount == 0)
                {
                    _reloadingCoroutine=StartCoroutine(Reloading());
                   
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

    private void CreateShot(Vector3 aim)
    {
        aim.y = _gunPoint.position.y;
        Vector3 target =aim-_gunPoint.position;
       
        Debug.DrawRay(_gunPoint.position, target,Color.yellow,5);
        Bullet bulletInstance = Instantiate(_bullet, _gunPoint.position, Quaternion.LookRotation(target));
        bulletInstance.SetVelocity(target.normalized*_bulletVelocity);
    }

    private IEnumerator Reloading()
    {
        StopCoroutine(_shootingCoroutine);
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
}
