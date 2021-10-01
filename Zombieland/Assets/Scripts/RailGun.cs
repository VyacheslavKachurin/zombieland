using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RailGun : MonoBehaviour
{
    public TextMeshProUGUI AmmoText;
    private Vector3 _shootingOffset = new Vector3(0, 0.3f, 0);
    public Transform GunPoint;
    public PlayerMovement PlayerMovement;
    private Vector3 _aim;
    public ParticleSystem MuzzleFlash;
    public GameObject ImpactEffect;
    public float ImpactForce = 10f;
    private Coroutine _firingCoroutine;
    private Coroutine _reloadingCoroutine;
    public float FiringPeriod = 0.3f;
    public int MaxAmmo = 10;
    private int _currentAmmo;
    private float _reloadTime = 1.5f;
    public bool _isReloading = false;
    private Animator _animator;
    private bool _isShooting = false;

    private void Awake()
    {

        _currentAmmo = MaxAmmo;
        UpdateAmmo();
        _animator = PlayerMovement.GetComponent<Animator>();

    }

    private void Update()
    {
        if (PlayerMovement.IsDead)
        {
            enabled = false;
        }
        Shoot();
    }
    private void Shoot()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (_isShooting == false && _isReloading == false)
            {
                _isShooting = true;
                _firingCoroutine = StartCoroutine(FireContiniously());
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_firingCoroutine != null)
            {

                StopCoroutine(_firingCoroutine);
                _isShooting = false;
            }
        }
    }
    IEnumerator FireContiniously()
    {
        while (!_isReloading)
        {
            if (_currentAmmo > 0)
            {
                _currentAmmo--;
                UpdateAmmo();
                MuzzleFlash.Play();
                RaycastHit hit;
                _aim = PlayerMovement.Destination + _shootingOffset - GunPoint.position;
                _aim.Normalize();
                if (Physics.Raycast(GunPoint.position, _aim, out hit))
                {

                    Debug.DrawRay(GunPoint.position, hit.point - GunPoint.position, Color.red, 5f);

                    if (hit.transform.tag == "Enemy")
                    {
                        hit.transform.GetComponent<Enemy>().TakeDamage();
                    }

                }
                GameObject impact = Instantiate(ImpactEffect, hit.point, Quaternion.identity);

                Destroy(impact, 1f);
                yield return new WaitForSeconds(FiringPeriod);
            }
            else
            {
                _isReloading = true;
                _reloadingCoroutine = StartCoroutine(Reload());
            }
        }
    }
    IEnumerator Reload()
    {
        StopCoroutine(_firingCoroutine);
        _animator.SetTrigger("Reloading");
        // Debug.Log("reloading");
        yield return new WaitForSeconds(_reloadTime);
        _currentAmmo = MaxAmmo;
        UpdateAmmo();


        //Debug.Log("stop reloading");
        _isReloading = false;
        if (_isShooting == true)
        {
            _firingCoroutine = StartCoroutine(FireContiniously());
        }
        StopCoroutine(_reloadingCoroutine);

    }

    private void UpdateAmmo()
    {
        AmmoText.text = $"Ammo: {_currentAmmo}";
    }


}

