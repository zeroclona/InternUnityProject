using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
  [SerializeField]
  private float rotationSpeed;
  private bool _isMoving;

  private Transform groundTransform;
  private float groundBounds;

  void Awake()
  {
    var ground = GameObject.Find("GroundCube");
    groundTransform = ground.transform;
    groundBounds = ground.transform.localScale.x / 2;
  }

  void Update() {
    if (_isMoving) return;

    if (Input.GetKey(KeyCode.W)) Assemble(Vector3.forward);
    if (Input.GetKey(KeyCode.A)) Assemble(Vector3.left);
    if (Input.GetKey(KeyCode.S)) Assemble(Vector3.back);
    if (Input.GetKey(KeyCode.D)) Assemble(Vector3.right);
  }

  private bool CheckWallExistence(Vector3 betweenFrom, Vector3 direction){
    return Physics.Raycast(betweenFrom, direction, 0.5f);
  }

  private void Assemble(Vector3 direction){
    if (_isMoving)
      return;
    if (CheckWallExistence(transform.position, direction))
      return;

    Vector3 cubeAnchor;
    var axis = Vector3.Cross(Vector3.up, direction);
    if (Vector3.Dot(transform.position, direction) + 1 > groundBounds)
      StartCoroutine(RotateGround());
    else{
      cubeAnchor = transform.position + (-Vector3.up + direction) * 0.5f;
      StartCoroutine(RotateCube(1));
    }

    IEnumerator RotateGround(){
      _isMoving = true;
      yield return RotateTransform(groundTransform, Vector3.zero, -axis, 1);
      cubeAnchor = transform.position + (Vector3.up + direction) * 0.5f;
      StartCoroutine(RotateCube(2));
    }

    IEnumerator RotateCube(int times){
      _isMoving = true;
      yield return RotateTransform(transform, cubeAnchor, axis, times);
      _isMoving = false;
      onMoveEnd();
    }
  }

  private IEnumerator RotateTransform (Transform whichTransform, Vector3 point, Vector3 axis, int times){
    for (int i = 0; i < (90 * times / rotationSpeed); i++){
      whichTransform.RotateAround(point, axis, rotationSpeed);
      yield return new WaitForSeconds(0.01f);
    }
  }

  private void onMoveEnd(){
    //Check for portal existence
    RaycastHit hitInfo;
    if(Physics.Raycast(transform.position, Vector3.down, out hitInfo, 0.5f)){
      var portal = hitInfo.collider.gameObject;
      if (portal.name != "Portal(Clone)"){
        return;
      }
      TestingGameManager.Instance.TakePortalPoint(portal);
    };
  }

}
