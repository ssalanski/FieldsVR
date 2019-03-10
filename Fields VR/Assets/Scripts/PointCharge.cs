using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCharge : MonoBehaviour
{

    public float charge;   

    // Use this for initialization
    void Start()
    {
        UpdateColor();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    void UpdateColor()
    {
        float h = charge > 0 ? 0 : 0.66f;
        float x = 1 - 1 / (1 + Mathf.Abs(charge));
        float s = Mathf.Sqrt(x);
        float v = 0.2f + 0.8f * x;
        GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(h, s, v);
    }
}
