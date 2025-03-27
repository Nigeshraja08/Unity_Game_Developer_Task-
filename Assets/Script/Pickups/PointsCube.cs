using UnityEngine;

public class PointsCube : CubeBase
{
    public override void OnPlayerTrigger(GameObject other)
    {
        Destroy(this.gameObject);
    }
}
