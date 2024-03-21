using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConstraintTrigger : MonoBehaviour
{
    [SerializeField]
    CameraBehavior behavior;

    [SerializeField]
    bool disableWhenExit = true;

    [SerializeField]
    Vector2 boundsSize, offset, softBounds, softOffset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        behavior.EnableConstraints(
            new CameraBounds(
                transform.position.x+offset.x- boundsSize.x,
                transform.position.x + offset.x + boundsSize.x,
                transform.position.y + offset.y - boundsSize.y,
                transform.position.y + offset.y + boundsSize.y),

            new CameraBounds(
                transform.position.x + softOffset.x - softBounds.x,
                transform.position.x + softOffset.x + softBounds.x,
                transform.position.y + softOffset.y - softBounds.y,
                transform.position.y + softOffset.y + softBounds.y)
            );
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (disableWhenExit)
            behavior.DisableConstraints();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0, 0.3f);
        Gizmos.DrawCube(transform.position + (Vector3)offset, new Vector3(boundsSize.x*2, boundsSize.y*2, boundsSize.y));
        Gizmos.color = new Color(255, 255, 0, 0.3f);
        Gizmos.DrawCube(transform.position + (Vector3)softOffset, new Vector3(softBounds.x*2, softBounds.y*2, softBounds.y));
    }
}

[System.Serializable]
public struct CameraBounds
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    public CameraBounds(float xMin, float xMax, float yMin, float yMax)
    {
        this.xMin = xMin;
        this.xMax = xMax;
        this.yMin = yMin;
        this.yMax = yMax;
    }
}
