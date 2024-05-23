using UnityEngine;

[RequireComponent(typeof(Explosion))]

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private int _minCubes = 2;
    [SerializeField] private int _maxCubes = 7;

    private Explosion _explosion;

    private void OnEnable()
    {
        Cube.Click += SpawnCubes;
    }

    private void OnDisable()
    {
        Cube.Click -= SpawnCubes;
    }

    private void Awake()
    {
        _explosion = GetComponent<Explosion>();
    }

    private void SpawnCubes(Cube cube)
    {
        Debug.Log(cube.SplitChance);

        if (Random.value <= cube.SplitChance)
        {
            int explosionForce = 50000;
            int explosionRadius = 360;
            int scaleReduce = 2;
            int splitReduce = 2;

            Vector3 position = cube.transform.position;
            Vector3 scale = cube.transform.localScale / scaleReduce;
            float newSplitChance = cube.SplitChance / splitReduce;

            int count = Random.Range(_minCubes, _maxCubes);

            for (int i = 0; i < count; i++)
            {
                new Cube(cubePrefab, position, scale, newSplitChance);

                if (cube.TryGetComponent(out Rigidbody rb) && _explosion != null)
                {
                    _explosion.ApplyExplosionForce(rb, position, explosionRadius, explosionForce);
                }
            }

        }
    }
}