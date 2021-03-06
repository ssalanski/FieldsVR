﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFieldArray : MonoBehaviour
{
    public GameObject arrowPrefab;

    public GameObject[] chargedParticles;
    
    public const int width = 7;
    public const int length = 7;
    public const int height = 4;
    private GameObject[,,] vectors = new GameObject[width, length, height];

    public float spacing = 1f;
    public float startHue = 0.675f;
    public float hueChange = -1.0f;

    // Use this for initialization
    void Start()
    {
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                for (int l = 0; l < length; l++)
                {
                    vectors[w, l, h] = Instantiate(arrowPrefab);
                    vectors[w, l, h].transform.parent = this.transform;
                    vectors[w, l, h].transform.localPosition = new Vector3((w - width / 2) * spacing, (h) * spacing, (l - length / 2) * spacing);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                for (int l = 0; l < length; l++)
                {
                    Vector3 electricField = Vector3.zero;
                    foreach (GameObject particle in chargedParticles)
                    {
                        Vector3 relativePosition = vectors[w, l, h].transform.position - particle.transform.position;
                        electricField = electricField + relativePosition.normalized / relativePosition.sqrMagnitude * particle.GetComponent<PointCharge>().charge;
                    }
                    vectors[w, l, h].transform.rotation = getQuaternion(electricField);
                    float vectorLength = Mathf.Min(electricField.magnitude * 25, spacing);
                    vectors[w, l, h].transform.localScale = new Vector3(vectorLength, 1, 1);
                    float hue = startHue + (1 - 1 / (electricField.magnitude + 1)) * hueChange;
                    Color x = Color.HSVToRGB(hue % 1.0f, 1, 1);
                    vectors[w, l, h].GetComponent<LineRenderer>().endColor = x;
                }
            }
        }
    }

    private Quaternion getQuaternion(Vector3 pos)
    {
        float alpha = Mathf.Atan2(pos.z, pos.y);
        float beta = Mathf.Atan2(Mathf.Sqrt(pos.z * pos.z + pos.y * pos.y), pos.x);
        return Quaternion.Euler(Mathf.Rad2Deg * alpha, 0, Mathf.Rad2Deg * beta);
    }

}