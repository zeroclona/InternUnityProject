using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  private float cameraSmooth = 5;
  private float backAngle = 0f;

  void Update()
  {
    if (Input.GetMouseButton(2)){
      var deltaAngle = Input.GetAxis("Mouse X") * cameraSmooth;
      transform.RotateAround(Vector3.zero, Vector3.up, deltaAngle);
      backAngle += deltaAngle;
    }
    else if (Mathf.Abs(backAngle) > 0.1f){

      var move = Mathf.LerpAngle(backAngle, 0f, Time.deltaTime );
      transform.RotateAround(Vector3.zero, Vector3.up, move-backAngle);

      backAngle = move;
    }
    else {
      transform.RotateAround(Vector3.zero, Vector3.up, -backAngle);
      backAngle = 0f;
    }
  }
}
