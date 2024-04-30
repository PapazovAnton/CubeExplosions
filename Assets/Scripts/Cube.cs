using UnityEngine;

public class Cube : MonoBehaviour
{
    private LayerMask _layerCube;
    private string _layerCubeName = "Cube";
    private float _splitChance = 1.0f;

    private void Awake()
    {
        _layerCube = LayerMask.GetMask(_layerCubeName);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerCube))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    SplitCube();
                }
            }
        }
    }

    private void SplitCube()
    {
        if (Random.value <= _splitChance)
        {
            int newCubesCount = Random.Range(2, 7);

            for (int i = 0; i < newCubesCount; i++)
            {
                GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                newCube.layer = LayerMask.NameToLayer(_layerCubeName);
                newCube.transform.position = transform.position;
                newCube.transform.localScale = transform.localScale / 2;

                newCube.AddComponent<Rigidbody>();
                newCube.AddComponent<Cube>()._splitChance = _splitChance / 2;

                Color randomColor = new Color(Random.value, Random.value, Random.value);
                newCube.GetComponent<Renderer>().material.color = randomColor;

                newCube.GetComponent<Rigidbody>().AddExplosionForce(500, transform.position, 5);
            }
        }

        Destroy(gameObject);
    }
}
