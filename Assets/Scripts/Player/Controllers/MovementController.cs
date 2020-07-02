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
  [SerializeField] private float speed = 30;

  private readonly float movementThreshold = 0.01f;

  private Vector2 velocity = Vector2.zero;
  public Vector2 Velocity { get => velocity; }

  public static event Action OnPlayerMove;

  private Vector3 lastNonZeroInputDirection = Vector3.right;
  [SerializeField] private Transform playerGraphics = null;

  private GameObject weapon = null;

  private new Rigidbody2D rigidbody;
  private Animator animator;
  private SpriteRenderer spriteRenderer;

  private PlayerGun playerGun;
  public AmmoType playerAmmoType;

  void Start()
  {
    rigidbody = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = transform.GetComponent<SpriteRenderer>();
    playerGun = transform.Find("Weapon").GetComponent<PlayerGun>();

    if (playerGun == null)
    {
      Debug.LogError("No player gun found");
    }
    else
    {
      playerAmmoType = playerGun.ammoType;
    }
  }

  private void FixedUpdate()
  {
    ResolveLookDirection();
    Move();
    LookAtMouse();

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

    transform.position += CalculateVelocity() * speed * Time.deltaTime;
    OnPlayerMove?.Invoke();
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

  private void LookAtMouse()
  {
    weapon = transform.Find("Weapon").gameObject;
    if (weapon == null)
    {
      Debug.LogError("No weapon found");
    }
    else
    {
      Vector3 screenMousePosition = new Vector3(Input.mousePosition.x,
                                              Input.mousePosition.y,
                                              -Camera.main.transform.position.z);
      Vector3 mousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);
      mousePosition.z = 0;

      Vector3 vectorToMouse = (mousePosition - weapon.transform.position).normalized;
      weapon.transform.right = vectorToMouse;
    }
  }

  private Vector3 CalculateVelocity()
  {
    Vector3 inputVelocity = new Vector3(Input.GetAxisRaw("Horizontal"),
                                        Input.GetAxisRaw("Vertical"),
                                        0).normalized;
    playerGraphics.right = inputVelocity;

    return inputVelocity;
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
