using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    [SerializeField] private float _speed;

    private Vector3 _targetPosition;

    private int _currentWaypoint;

    private void Start()
    {
        UpdateTargetPosition();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

        if (transform.position == _targetPosition)
        {
            _currentWaypoint = ++_currentWaypoint % _waypoints.Length;

            UpdateTargetPosition();
        }
    }

    private void UpdateTargetPosition()
    {
        _targetPosition = _waypoints[_currentWaypoint].position;
        _targetPosition.y = transform.position.y;
    }
}
