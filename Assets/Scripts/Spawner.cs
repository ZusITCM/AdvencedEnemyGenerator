using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;

    [SerializeField] private Transform _target;

    private readonly int _defaultCapacityPool = 5;
    private readonly int _maxSizePool = 10;

    private ObjectPool<Enemy> _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: (enemy) => ActivateEnemy(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: _defaultCapacityPool,
            maxSize: _maxSizePool
            );
    }

    public void Spawn()
    {
        _enemyPool.Get();
    }

    private void Release(Enemy enemy)
    {
        enemy.Despawned -= Release;

        _enemyPool.Release(enemy);
    }

    private void ActivateEnemy(Enemy enemy)
    {
        enemy.Despawned += Release;
        enemy.transform.position = transform.position;
        enemy.SetTarget(GetTargetPosition());
        enemy.gameObject.SetActive(true);
    }

    private Transform GetTargetPosition()
    {
        return _target;
    }
}
    