using UnityEngine;

public abstract class Obstacle : Drifter
{
  [SerializeField] protected int damage = 1;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Block")
    {
      this.collideWithBlock(other.GetComponentInChildren<Block>());
    }
  }

  public override bool isDrifting() => true;
  public abstract void collideWithBlock(Block block);
}