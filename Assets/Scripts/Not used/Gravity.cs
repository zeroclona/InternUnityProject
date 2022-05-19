using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
  [SerializeField]
  private Transform groundTransform;
  private Collider groundCollider;
  private Rigidbody rb;

  public float AttractionStrength;

  void Start()
  {
    var groundObject = GameObject.Find("GroundCube");
    groundTransform = groundObject.transform;
    groundCollider = groundObject.GetComponent<Collider>();
    Debug.Log(groundCollider);
    rb = GetComponent<Rigidbody>();
    if (rb)
      if (rb.useGravity)
        rb.useGravity = false;
    AttractionStrength = 100f;
  }

  void FixedUpdate()
  {
    if (!rb)
      return;

    var offset = groundTransform.position - transform.position;
    var direction = offset.normalized;

    Ray ray = new Ray(transform.position, direction);
    RaycastHit hitInfo;
    if (groundCollider.Raycast(ray, out hitInfo, 1000f)){
      var projectionDistSqr = Vector3.Dot(offset, -hitInfo.normal);   //Расстояние до центральной оси гравикуба
      projectionDistSqr *= projectionDistSqr;
      rb.AddForce((- hitInfo.normal * AttractionStrength / projectionDistSqr) * rb.mass);
    }
  }
}
