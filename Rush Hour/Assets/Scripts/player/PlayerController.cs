using UnityEngine;
public class PlayerController : MonoBehaviour, IOnBlock
{
  [SerializeField] private int playerNumber;
  [SerializeField] private PlayerManager manager;
  [SerializeField] private float speed;
  [SerializeField] private KeySet keySet;
  private Vector2 movePosition;
  private Target target;
  private Vector2 facingDirection;
  private Block heldBlock;
  private bool isDestroyed;

  void Awake()
  {
    this.movePosition = transform.position;
    this.target = GetComponentInChildren<Target>();
    this.facingDirection = new Vector2(0f, 1f);
    this.target.changeDirection(this.facingDirection);
    this.isDestroyed = false;
  }

  void Update()
  {
    this.move();
    this.handleBlock();

    if (this.isDestroyed)
    {
      this.manager.playerDied(this);
      GameObject.Destroy(this.heldBlock);
      gameObject.SetActive(false);
    }
  }

  private void move()
  {
    transform.position = Vector3.MoveTowards(transform.position, this.movePosition, this.speed * Time.deltaTime);

    if (Vector3.Distance(transform.position, this.movePosition) <= 0.1f)
    {
      if (Input.GetKey(this.keySet.up))
      {
        this.moveInDirection(new Vector2(0f, 1f));
      }
      else if (Input.GetKey(this.keySet.down))
      {
        this.moveInDirection(new Vector2(0f, -1f));
      }
      else if (Input.GetKey(this.keySet.right))
      {
        this.moveInDirection(new Vector2(1f, 0f));
      }
      else if (Input.GetKey(this.keySet.left))
      {
        this.moveInDirection(new Vector2(-1f, 0f));
      }
    }
  }

  private void moveInDirection(Vector2 direction)
  {
    if (!direction.Equals(this.facingDirection))
    {
      this.facingDirection = direction;
      this.target.changeDirection(direction);
    }
    if (this.canMoveTowardsTarget())
    {
      this.movePosition += direction * Grid.size;
    }
  }

  private bool canMoveTowardsTarget()
  {
    return !Input.GetKey(this.keySet.pivot) && this.target.isOnBlockOnRaft() &&
           (this.target.lookAtBlock().canMoveOnto() || !this.hasBlock());
  }

  private void handleBlock()
  {
    if (Input.GetKeyDown(this.keySet.use))
    {
      if (this.hasBlock())
      {
        this.heldBlock = this.target.putDownBlock(this.heldBlock) ? null : this.heldBlock;
      }
      else
      {
        // Check that player isn't moving into block
        if (this.target.lookAtBlock() != null)
        {
          this.movePosition = Grid.positionOnGrid(transform.position);
        }
        this.heldBlock = this.target.pickUpBlock(transform);
      }
    }
  }

  private bool hasBlock() => this.heldBlock != null;
  public void blockDestroyed() => this.isDestroyed = true;
  public bool canPickUp() => false;
  public bool canMoveOnto() => !this.hasBlock();

  public void respawn(Vector3 newPosition)
  {
    gameObject.SetActive(true);
    this.isDestroyed = false;
    transform.position = newPosition;
    this.movePosition = newPosition;
  }
}
