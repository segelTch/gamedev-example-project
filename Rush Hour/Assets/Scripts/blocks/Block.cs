using UnityEngine;
using System.Collections.Generic;

public abstract class Block : Drifter
{
  [SerializeField] private bool isOnRaft = false;
  [SerializeField] protected int healthTotal = 1;
  [SerializeField] protected int damageReduction = 0;
  protected bool isLifted = false;
  private List<IOnBlock> standers = new List<IOnBlock>();

  public void getHit(int damage)
  {
    this.healthTotal -= (Mathf.Max(damage - this.damageReduction, 0));
    this.checkBlockStatus();
  }

  public override bool isDrifting() => !this.isOnRaft;

  public void pickUp(Transform lifter)
  {
    this.isOnRaft = true;
    this.isLifted = true;
    transform.position = lifter.position + new Vector3(0, 0, -(Grid.size * 3));
    transform.SetParent(lifter);
  }

  public void putDown(Vector3 position)
  {
    transform.position = Grid.positionOnGrid(position);
    this.isLifted = false;
    transform.SetParent(null);
  }

  private void attachToRaft()
  {
    this.isOnRaft = true;
    this.putDown(transform.position);
  }

  public bool onRaft() => this.isOnRaft;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Block" && !this.isLifted)
    {
      Block otherBlock = other.GetComponentInChildren<Block>();
      if (!otherBlock.isOnRaft)
      {
        otherBlock.attachToRaft();
      }
    }
    else if (other.tag == "Player")
    {
      this.standOnBlock(other.GetComponentInChildren<IOnBlock>());
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      this.getOffBlock(other.GetComponentInChildren<IOnBlock>());
    }
  }

  public bool canMoveOnto() => !this.standers.Exists(stander => !stander.canMoveOnto());
  protected void standOnBlock(IOnBlock newStander) => this.standers.Add(newStander);
  private bool getOffBlock(IOnBlock leaver) => this.standers.Remove(leaver);

  protected void checkBlockStatus()
  {
    if (this.healthTotal <= 0)
    {
      this.alertBlockDestroyed();
      Destroy(gameObject);
    }
  }

  private void alertBlockDestroyed()
  {
    foreach (IOnBlock stander in this.standers)
    {
      stander.blockDestroyed();
    }
  }

  public bool canPickUp()
  {
    foreach (IOnBlock stander in this.standers)
    {
      if (!stander.canPickUp())
      {
        return false;
      }
    }

    return true;
  }
}