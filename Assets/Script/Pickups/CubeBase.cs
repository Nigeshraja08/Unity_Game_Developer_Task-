using UnityEngine;

public abstract class CubeBase : MonoBehaviour
{
    const string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            OnPlayerTrigger(other.gameObject);

        }
    }

   public abstract void OnPlayerTrigger(GameObject other);
}
