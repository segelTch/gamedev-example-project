using UnityEngine;
public class WeakBoulder : Obstacle
{
    public override void collideWithBlock(Block block)
    {
        block.getHit(this.damage);
        GameObject.Destroy(gameObject);
    }
}