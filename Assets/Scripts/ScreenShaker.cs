using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour {
  public IEnumerator Shake(float duration, float magnitude) {
    Vector3 originalPosition = transform.localPosition;

    float elapsed = 0.0f;

    while (elapsed < duration) {
      float xOffset = Random.Range(-1.0f, 1.0f) * magnitude;
      float yOffset = Random.Range(-1.0f, 1.0f) * magnitude;

      transform.localPosition = new Vector3(xOffset, yOffset, originalPosition.z);

      elapsed += Time.deltaTime;

      yield return null;
    }

    transform.localPosition = originalPosition;
  }

  public void StartShake(float duration, float magnitude) {
    StartCoroutine(Shake(duration, magnitude));
  }
}
