using UnityEngine;
using UnityEngine.Events;

public class LifeBlock : Block
{
  [SerializeField] private UnityEvent lifeBlockDestroyed;

  private void OnDestroy()
  {
    this.lifeBlockDestroyed.Invoke();
  }

  public void summonPlayer(PlayerController player)
  {
    player.respawn(transform.position + new Vector3(0, 0, Grid.size));
    this.standOnBlock(player);
  }
}