using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _target;

    private MeshRenderer _meshRendrer;

    public event Action<Enemy> Despawned;

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Target target))
            Despawned?.Invoke(this);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        transform.LookAt(_target);
    }
}