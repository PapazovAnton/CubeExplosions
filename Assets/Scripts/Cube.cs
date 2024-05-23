using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    public float SplitChance { get; private set; }

    public static event Action<Cube> Click;

    private void OnMouseDown()
    {
        Click?.Invoke(this);
        Destroy(gameObject);
    }

    public Cube(Cube cubePrefab, Vector3 position, Vector3 scale, float splitChance)
    {
        Cube newCube = Instantiate(cubePrefab, position, Quaternion.identity);
        newCube.transform.localScale = scale;
        newCube.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
        newCube.SplitChance = splitChance;
    }
}