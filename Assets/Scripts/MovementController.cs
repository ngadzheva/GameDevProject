using UnityEngine;
using System;
using static UnityEngine.Mathf;
using static Controlls;
using static PlayerWeaponInventory;

public class MovementController : MonoBehaviour
{

  [SerializeField]
  [Range(0, 5)]
  private float moveSpeed = 2;

  private readonly float movementThreshold = 0.01f;

  private Vector2 velocity = Vector2.zero;
  public Vector2 Velocity { get => velocity; }

  private new Rigidbody2D rigidbody;
  private Animator animator;
  private Animator weaponAnimator;

  public UISlider healthbar;
  public UISlider power;

  void Start()
  {
    rigidbody = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    weaponAnimator = GameObject.FindWithTag("Gun").GetComponent<Animator>();
  }

  void Update()
  {
    ResolveLookDirection();
    Move();

    if (!weaponAnimator)
    {
      weaponAnimator = GameObject.FindWithTag("Gun").GetComponent<Animator>();
    }

    if (Input.GetKeyDown(fireKey))
    {
      animator.SetTrigger("ShouldAttack");
      weaponAnimator.SetTrigger("ShouldAttack");
    }
  }

  private void FixedUpdate()
  {
    DoHorizontalMove();
    DoVerticalMove();
  }

  private void Move()
  {
    Vector2 newPosition = new Vector2
    {
      x = velocity.x * moveSpeed,
      y = velocity.y * moveSpeed
    } * Time.deltaTime + rigidbody.position;
    rigidbody.MovePosition(newPosition);
  }

  public void DoHorizontalMove()
  {
    float horizontalMoveDirection = Input.GetAxisRaw(HorizontalMovementAxis);
    SetHorizontalMoveDirection(horizontalMoveDirection);
    animator.SetFloat("HorizontalMovement", Abs(horizontalMoveDirection));
    weaponAnimator.SetTrigger("ShouldWalk");
  }

  public void DoVerticalMove()
  {
    float verticalMoveDirection = Input.GetAxisRaw(VerticalMovementAxis);
    SetVerticalMoveDirection(verticalMoveDirection);
    animator.SetFloat("VerticalMovement", Abs(verticalMoveDirection));
    weaponAnimator.SetTrigger("ShouldWalk");
  }

  public void SetHorizontalMoveDirection(float amount)
  {
    velocity.x = amount;
  }

  public void SetVerticalMoveDirection(float amount)
  {
    velocity.y = amount;
  }

  private void ResolveLookDirection()
  {
    if (Abs(velocity.x) > movementThreshold)
    {
      SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();
      spriteRenderer.flipX = velocity.x < 0;
    }
  }

  public void TurnTowards(float direction)
  {
    transform.localScale = new Vector3(Sign(direction), 1, 1);
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    GameObject other = collision.gameObject;

    if (other.CompareTag("Blood"))
    {
      healthbar.SetValue(5);
    }
    else if (other.CompareTag("Power"))
    {
      power.SetValue(5);
    }
    else if (other.CompareTag("Bullet"))
    {
      AmmoAdd(AmmoType.pistolAmmo, 5);
      // update UI
    }
    else if (other.CompareTag("Bullet1"))
    {
      AmmoAdd(AmmoType.rifleAmmo, 5);
      // update UI
    }
    else if (other.CompareTag("Bullet2"))
    {
      AmmoAdd(AmmoType.shotgunAmmo, 5);
      // update UI
    }

    Destroy(other);
  }
}
