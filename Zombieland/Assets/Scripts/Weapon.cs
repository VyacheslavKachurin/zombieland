using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Weapon : MonoBehaviour
{

    public event Action<int> OnBulletAmountChanged;

    public GameObject Bullet;
    public Transform GunPoint;
    public float Speed = 20f;
    public Player PlayerMovement;
    private Coroutine _shootingCoroutine;
    public float FiringPeriod = 0.15f;
    public int CurrentAmmo = 30;
    public float ReloadingTime = 1.5f;
    public int MaxAmmo = 30;
    private bool _isReloading = false;
    private Coroutine _reloadingCoroutine;
    private Animator _animator;
    public ParticleSystem MuzzleFlash;
    private Vector3 _destination;

    private void Start()
    {
        _animator = PlayerMovement.GetComponent<Animator>();   
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!_isReloading)
        {
           _shootingCoroutine= StartCoroutine(Shoot());
        }
        if (Input.GetMouseButtonUp(0)/*&&!_isPaused*/)
        {
            if (_shootingCoroutine != null)
            {
                StopCoroutine(_shootingCoroutine);
            }
        }
    }
    private IEnumerator Shoot()
    {       
        while (true)
        {
            if (CurrentAmmo > 0)
            {
                MuzzleFlash.Play();
                CurrentAmmo--;
                OnBulletAmountChanged(CurrentAmmo);
                Vector3 target = _destination;
                target.y = GunPoint.position.y;
                Vector3 aim = target - GunPoint.position;
                GameObject bulletPrefab = Instantiate(Bullet, GunPoint.position, Quaternion.LookRotation(aim));
                bulletPrefab.GetComponent<Rigidbody>().velocity = aim.normalized * Speed;


                yield return new WaitForSeconds(FiringPeriod);
            }
            else
            {
                _reloadingCoroutine=StartCoroutine(Reload());
                yield return null;
            }
        }
    }
    private IEnumerator Reload()
    {
            _animator.SetTrigger("Reloading");
            _isReloading = true;
            StopCoroutine(_shootingCoroutine);

            yield return new WaitForSeconds(ReloadingTime);
            CurrentAmmo = MaxAmmo;

            OnBulletAmountChanged(CurrentAmmo);
            _isReloading = false;
            if (Input.GetMouseButton(0))
            {
                _shootingCoroutine = StartCoroutine(Shoot());
                yield return null;
            }
            StopCoroutine(_reloadingCoroutine);
    }
    public void TakeMousePosition(Vector3 mousePosition)
    {
        _destination = mousePosition;
    }

}
