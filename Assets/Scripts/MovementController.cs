using UnityEngine;
using System;
using static UnityEngine.Mathf;
using static Controls;

public class MovementController : MonoBehaviour {

	[SerializeField]
	[Range(0, 5)]
	private float moveSpeed = 2;

	[SerializeField]
	[Range(0, 2)]
	private float xScale = 0.5f;

	[SerializeField]
	[Range(0, 2)]
	private float yScale = 0.5f;
	
	[SerializeField]
	[Range(0, 2)]
	private float zScale = 0.5f;

	private readonly float movementThreshold = 0.01f;

	private Vector2 velocity = Vector2.zero;
	public Vector2 Velocity { get => velocity; }

	private new Rigidbody2D rigidbody;
	private Animator animator;

	void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	void Update() {
		ResolveLookDirection();
		Move();

		if (Input.GetKeyDown(attackKey)) {
			animator.SetTrigger("ShouldAttack");
		}
	}

	private void FixedUpdate() {
		DoHorizontalMove();
		DoVerticalMove();
	}

	private void Move() {
		Vector2 newPosition = new Vector2 {
			x = velocity.x * moveSpeed,
			y = velocity.y * moveSpeed
		} * Time.deltaTime + rigidbody.position;
		rigidbody.MovePosition(newPosition);
	}

	public void DoHorizontalMove() {
		float horizontalMoveDirection = Input.GetAxisRaw(HorizontalMovementAxis);
		SetHorizontalMoveDirection(horizontalMoveDirection);
		animator.SetFloat("HorizontalMovement", Abs(horizontalMoveDirection));
	}

	public void DoVerticalMove() {
		float verticalMoveDirection = Input.GetAxisRaw(VerticalMovementAxis);
		SetVerticalMoveDirection(verticalMoveDirection);
		animator.SetFloat("VerticalMovement", Abs(verticalMoveDirection));
	}

	public void SetHorizontalMoveDirection(float amount) {
		velocity.x = amount;
	}

	public void SetVerticalMoveDirection(float amount) {
		velocity.y = amount;
	}

	private void ResolveLookDirection() {
		if (Abs(velocity.x) > movementThreshold) {
			transform.localScale = new Vector3(Sign(velocity.x) * xScale, yScale, zScale);
		}
	}

	public void TurnTowards(float direction) {
		transform.localScale = new Vector3(Sign(direction) * xScale, yScale, zScale);
	}

	private void OnCollisionEnter2D(Collision2D collision) {

	}
}
