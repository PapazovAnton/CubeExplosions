using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _splitChance;

    private string _layerCubeName = "Cube";
    private string _layerWallsName = "Walls";
    
    private void OnMouseDown()
    {
        int layerMask = ~LayerMask.GetMask(_layerWallsName);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject == gameObject)
            {
                Split();
            }
        }
    }

    private void Split()
    {
        int minRandomCubes = 2;
        int maxRandomCubes = 7;
        int scaleReduce = 2;
        int splitReduce = 2;
        int explosionForce = 500;
        int explosionRadius = 5;

        if (Random.value <= _splitChance)
        {
            int newCubesCount = Random.Range(minRandomCubes, maxRandomCubes);

            for (int i = 0; i < newCubesCount; i++)
            {
                GameObject newCube = Instantiate(_cubePrefab, transform.position, Quaternion.identity);

                newCube.layer = LayerMask.NameToLayer(_layerCubeName);
                newCube.transform.position = transform.position;
                newCube.transform.localScale = transform.localScale / scaleReduce;

                Color randomColor = new Color(Random.value, Random.value, Random.value);
                newCube.GetComponent<Renderer>().material.color = randomColor;

                newCube.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);

                Cube cubeScript = newCube.GetComponent<Cube>();
                cubeScript._cubePrefab = _cubePrefab;
                cubeScript._splitChance = _splitChance / splitReduce;
            }
        }

        Destroy(gameObject);
    }
}