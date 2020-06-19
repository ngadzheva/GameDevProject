using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
	public static KeyCode attackKey = KeyCode.E;
	public static string HorizontalMovementAxis = "Horizontal";
	public static string VerticalMovementAxis = "Vertical";

	[SerializeField]
	private KeyCode attackKeyBinding = attackKey;

	private void OnValidate() {
		attackKey = attackKeyBinding;
	}
}
