using UnityEngine;
using System;
using static UnityEngine.Mathf;
using static Controlls;
using static PlayerWeaponInventory;
using static PlayerGun;

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
  private SpriteRenderer spriteRenderer;
  public AmmoType playerAmmoType;

  void Start()
  {
    rigidbody = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = transform.GetComponent<SpriteRenderer>();
    PlayerGun playerGun = transform.Find("Weapon").GetComponent<PlayerGun>();
    playerAmmoType = playerGun.ammoType;
  }

  private void FixedUpdate()
  {
    ResolveLookDirection();
    Move();

    PlayerGun playerGun = transform.Find("Weapon").GetComponent<PlayerGun>();
    playerAmmoType = playerGun.ammoType;

    if (Input.GetKeyDown(fireKey) && HasAmmo(playerAmmoType) && !animator.GetBool("IsDying"))
    {
      animator.SetTrigger("ShouldAttack");
    }

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
  }

  public void DoVerticalMove()
  {
    float verticalMoveDirection = Input.GetAxisRaw(VerticalMovementAxis);
    SetVerticalMoveDirection(verticalMoveDirection);
    animator.SetFloat("VerticalMovement", Abs(verticalMoveDirection));
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
      spriteRenderer.flipX = velocity.x < 0;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    GameObject other = collision.gameObject;

    if (other.CompareTag("Bullet"))
    {
      AmmoAdd(AmmoType.pistolAmmo, 5);
      Destroy(other);
    }
    else if (other.CompareTag("Bullet1"))
    {
      AmmoAdd(AmmoType.rifleAmmo, 5);
      Destroy(other);
    }
    else if (other.CompareTag("Bullet2"))
    {
      AmmoAdd(AmmoType.shotgunAmmo, 5);
      Destroy(other);
    }
  }
}
