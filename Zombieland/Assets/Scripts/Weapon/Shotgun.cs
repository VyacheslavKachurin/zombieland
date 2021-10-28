using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IShootingType
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _fleshEffect;// video says instantiating takes more performance then repositioning it, check with profiler in future?
    [SerializeField] private TrailRenderer _tracerEffect;

    [SerializeField] private float _damageAmount = 15f;

    private Ray _ray;
    private RaycastHit _hitInfo;


    public void CreateShot(Vector3 aim, Vector3 gunPointPosition)
    {
        Vector3 direction = aim - gunPointPosition;
        

    }
}
