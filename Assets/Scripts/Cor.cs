using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cor : MonoBehaviour
{
    Material material;
    void Start()
    {
        material = gameObject.GetComponent<Material>();
        material.SetColor("_Color", new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(100, 255)));
    }

    // Update is called once per frame
}
