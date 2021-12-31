using UnityEngine;

public class Target : MonoBehaviour
{
  private Block targettedBlock;
  
  public void changeDirection(Vector2 direction) => transform.localPosition = Grid.size * direction;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Block")
    {
      this.targettedBlock = other.gameObject.GetComponent<Block>();
    }
  }

  public bool isOnBlock() =>
      (this.targettedBlock != null && this.targettedBlock.gameObject.activeSelf &&
              Vector3.Distance(Grid.positionOnGrid(transform.position),
                               this.targettedBlock.transform.position) <= 1);

  public bool isOnBlockOnRaft() => (this.isOnBlock() && this.targettedBlock.onRaft());

  public Block pickUpBlock(Transform player)
  {
    if (this.isOnBlock() && this.targettedBlock.canPickUp())
    {
      this.targettedBlock.pickUp(player);
      return this.targettedBlock;
    }

    return null;
  }

  public Block lookAtBlock()
  {
    if (this.isOnBlock())
    {
      return this.targettedBlock;
    }

    return null;
  }

  public bool putDownBlock(Block block)
  {
    if (block != null && !this.isOnBlock())
    {
      block.putDown(Grid.positionOnGrid(transform.position));
      return true;
    }
    return false;
  }
}