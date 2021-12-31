using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CameraMovement : MonoBehaviour
{

  [SerializeField] private PlayerManager manager;

  [SerializeField] private float heightFactor;

  private void Start()
  {
    transform.position = this.targetPosition();
  }
  private void Update()
  {
    this.followPlayers();
  }
  private void followPlayers() => transform.position = Vector3.Lerp(transform.position, this.targetPosition(), 0.1f);

  private Vector3 targetPosition()
  {
    Vector3 targetPosition = this.findAveragePosition();

    targetPosition.y += this.heightFactor;
    targetPosition.z = transform.position.z;

    return targetPosition;
  }

  private List<Transform> playerPositions() => this.manager.getPlayers().Select(player => player.transform).ToList();

  private Vector3 findAveragePosition() =>
    this.playerPositions().Aggregate(Vector3.zero, (origin, newTransform) => origin + newTransform.position) /
                                     (this.playerPositions().Count > 0 ? this.playerPositions().Count : 1);
}