using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Explosion))]

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private int _minCubes = 2;
    [SerializeField] private int _maxCubes = 7;

    private Explosion _explosion;

    private void Start()
    {
        _explosion = GetComponent<Explosion>();
    }

    public void SpawnCubes(Vector3 position, Vector3 scale, float newSplitChance)
    {
        int explosionForce = 500;
        int explosionRadius = 5;

        int count = Random.Range(_minCubes, _maxCubes);

        for (int i = 0; i < count; i++)
        {
            Cube newCube = Instantiate(cubePrefab, position, Quaternion.identity);
            newCube.transform.localScale = scale;
            newCube.SplitChance = newSplitChance;
            newCube.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);

            if(newCube.TryGetComponent<Rigidbody>(out Rigidbody rb) && _explosion != null)
            {
                _explosion.ApplyExplosionForce(rb, position, explosionRadius, explosionForce);
            }
        }
    }
}
