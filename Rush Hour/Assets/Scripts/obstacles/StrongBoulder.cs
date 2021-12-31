using UnityEngine;
public class StrongBoulder : Obstacle
{
  public override void collideWithBlock(Block block) => block.getHit(this.damage);
}