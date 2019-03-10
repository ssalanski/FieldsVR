using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFieldArray : MonoBehaviour
{
    public GameObject arrowPrefab;

    public GameObject chargedParticle;

    private GameObject vector;

    // Use this for initialization
    void Start()
    {
        vector = Instantiate(arrowPrefab);
        vector.transform.parent = this.transform;
        vector.transform.localPosition = new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePosition = vector.transform.position - chargedParticle.transform.position;
        Vector3 electricField = relativePosition.normalized / relativePosition.sqrMagnitude * chargedParticle.GetComponent<PointCharge>().charge;
        vector.transform.rotation = getQuaternion(electricField);
        float vectorLength = electricField.magnitude * 25;
        vector.transform.localScale = new Vector3(vectorLength, 1, 1);
    }

    private Quaternion getQuaternion(Vector3 pos)
    {
        float alpha = Mathf.Atan2(pos.z, pos.y);
        float beta = Mathf.Atan2(Mathf.Sqrt(pos.z * pos.z + pos.y * pos.y), pos.x);
        return Quaternion.Euler(Mathf.Rad2Deg * alpha, 0, Mathf.Rad2Deg * beta);
    }

}