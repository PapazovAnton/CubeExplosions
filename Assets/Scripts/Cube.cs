using UnityEngine;

public class Cube : MonoBehaviour
{
    private string _layerCubeName = "Cube";
    private string _layerWallsName = "Walls";
    private float _splitChance = 1.0f;

    private void OnMouseDown()
    {
        int layerMask = ~LayerMask.GetMask(_layerWallsName);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject == gameObject)
            {
                SplitCube();
            }
        }
    }

    private void SplitCube()
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
                GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                newCube.layer = LayerMask.NameToLayer(_layerCubeName);
                newCube.transform.position = transform.position;
                newCube.transform.localScale = transform.localScale / scaleReduce;

                newCube.AddComponent<Rigidbody>();
                newCube.AddComponent<Cube>()._splitChance = _splitChance / splitReduce;

                Color randomColor = new Color(Random.value, Random.value, Random.value);
                newCube.GetComponent<Renderer>().material.color = randomColor;

                newCube.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        Destroy(gameObject);
    }
}