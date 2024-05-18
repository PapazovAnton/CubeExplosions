using UnityEngine;

[RequireComponent(typeof(Spawner))]

public class Cube : MonoBehaviour
{
    private Spawner _spawner;
    private float _splitChance = 1.0f;

    public float SplitChance
    {
        get { return _splitChance; }
        set { _splitChance = value; }
    }

    private void Start()
    {
        _spawner = GetComponentInParent<Spawner>();
    }

    private void OnMouseDown()
    {
        int scaleReduce = 2;
        int splitReduce = 2;

        if (_spawner != null)
        {
            if (Random.value <= _splitChance)
            {
                _spawner.SpawnCubes(transform.position, transform.localScale / scaleReduce, _splitChance / splitReduce);
            }
        }

        Destroy(gameObject);
    }
}