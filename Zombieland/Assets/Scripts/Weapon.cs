using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Bullet;
    public Transform GunPoint;
    public float Speed = 20f;
    public PlayerMovement PlayerMovement;
    private Coroutine _shootingCoroutine;
    public float FiringPeriod = 0.15f;
    public int CurrentAmmo = 30;
    public float ReloadingTime = 3.09f;
    public int MaxAmmo = 30;
    private bool _isReloading = false;
    private Coroutine _reloadingCoroutine;
    private Animator _animator;
    public ParticleSystem MuzzleFlash;
    private Vector3 _destination;
    // Start is called before the first frame update
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
        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(_shootingCoroutine);
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
                //   Vector3 target = PlayerMovement.Destination;
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
        Debug.Log("Reloading");
        yield return new WaitForSeconds(ReloadingTime);
        CurrentAmmo = MaxAmmo;
        Debug.Log("Reloaded");
        _isReloading = false;
        if (Input.GetMouseButton(0))
        {
            _shootingCoroutine=StartCoroutine(Shoot());
            yield return null;
        }
        StopCoroutine(_reloadingCoroutine);
    }
    public void TakeMousePosition(Vector3 mousePosition)
    {
        _destination = mousePosition;
    }
}
