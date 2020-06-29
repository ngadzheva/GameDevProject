using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
  private GameObject objectToFollow;
  public Vector3 cameraOffset;
  public float smoothSpeed;

  void Start() {
    objectToFollow = GameObject.FindWithTag("Player");
  }

  void LateUpdate() {
    Vector3 desiredPosition = objectToFollow.transform.position + cameraOffset;
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    transform.position = smoothedPosition;
    transform.LookAt(objectToFollow.transform);
  }
}
