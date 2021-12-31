using UnityEngine;
public abstract class Drifter : MonoBehaviour
{
  private void Update()
  {
    if (this.isDrifting())
    {
      this.drift();
    }

    this.checkIfLeftScreen();
  }
  public void drift()
  {
    transform.position += new Vector3(0, -River.speed * Time.deltaTime, 0);
  }

  public abstract bool isDrifting();

  private void checkIfLeftScreen()
  {
    if (transform.position.y < -20)
    {
      Destroy(gameObject);
    }
  }
}