using System;
using UnityEngine;
using UnityEngine.UIElements;

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

    public void Init(Vector3 position, Vector3 scale, float splitChance)
    {
        transform.position = position;
        transform.localScale = scale;
        GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
        SplitChance = splitChance;
    }
}