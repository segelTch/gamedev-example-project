using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
  [SerializeField] private List<PlayerController> players;
  [SerializeField] private float respawnTime;
  [SerializeField] private UnityEvent<PlayerController> respawnPlayerEvent;
  public List<PlayerController> getPlayers() => this.players.Where(player => player != null).ToList();

  public void playerDied(PlayerController player)
  {
    this.players.Remove(player);
    StartCoroutine(respawnPlayer(player, this.respawnTime));
  }

  IEnumerator<WaitForSeconds> respawnPlayer(PlayerController player, float delay)
  {
    yield return new WaitForSeconds(delay);

    this.respawnPlayerEvent.Invoke(player);
    this.players.Add(player);
  }
}