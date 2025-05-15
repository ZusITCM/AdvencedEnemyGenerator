using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _targetPosition;

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

    public void SetTargetPosition(Vector3 position)
    {
        _targetPosition = position;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        transform.LookAt(_targetPosition);
    }
}