using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class targetPriority
{
    public Transform target;
    [Range(0f, 1f)]
    public float startPriority;
    
    [SerializeField]
    private float priority;
}


public class CameraBehavior : MonoBehaviour
{
    [SerializeField]
    private float fractionOfJourney = 0.5f;

    [SerializeField]
    private float targetCheckSize = 10;

    [Range(0f, 1f)]
    [SerializeField]
    private float globalCamSpeed;

    [SerializeField]
    private Transform xEndPoint;
    [SerializeField]
    private Transform xStartPoint;
    [SerializeField]
    private Transform yEndPoint;
    [SerializeField]
    private Transform yStartPoint;

    [SerializeField]
    private List<targetPriority> targets;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject parent;


    Vector3 cameraScale = new Vector3(3.5f, 2);
    Vector3 unboundedPosition;
    Vector3 targetPos;
    Vector3 unCapTargetPos;
    [SerializeField]
    CameraBounds bounds, softBounds;
    [SerializeField]
    bool boundsEnabled = false;


    private void Update()
    {
        if (player != null && parent != null)
        {
            Vector3 dif= player.position - parent.transform.position;
            if (boundsEnabled && targetPos.x != unCapTargetPos.x)
            {
                dif.x = 0;
            }
            if (boundsEnabled && targetPos.y != unCapTargetPos.y)
            {
                dif.y = 0;
            }
            unboundedPosition += dif;
            parent.transform.position = player.position;
        }
        if (targets.Count > 0)
        {
            Vector3 midpoint = GetMidpointOfPoints(targets);

            //Debug.Log(midpoint);

            parent.transform.position = Vector3.Lerp(parent.transform.position, midpoint, globalCamSpeed * Time.deltaTime);
        }


        if (boundsEnabled)
        {
            unCapTargetPos = new Vector3(Mathf.Lerp(xStartPoint.position.x, xEndPoint.position.x, Input.mousePosition.x / (float)Screen.width),
                            Mathf.Lerp(yStartPoint.position.y, yEndPoint.position.y, Input.mousePosition.y / (float)Screen.height),
                            transform.position.z);
            targetPos = unCapTargetPos;

            targetPos.x = Mathf.Clamp(targetPos.x, softBounds.xMin, softBounds.xMax);
            targetPos.y = Mathf.Clamp(targetPos.y, softBounds.yMin, softBounds.yMax);
            unboundedPosition = Vector3.Lerp(unboundedPosition, targetPos, fractionOfJourney);
            transform.position = new Vector3(
                Mathf.Clamp(unboundedPosition.x, bounds.xMin, bounds.xMax),
                Mathf.Clamp(unboundedPosition.y, bounds.yMin, bounds.yMax),
                transform.position.z);
        }
        else
        {
            unboundedPosition = new Vector3(Mathf.Lerp(unboundedPosition.x, Mathf.Lerp(xStartPoint.position.x, xEndPoint.position.x, Input.mousePosition.x / (float)Screen.width), fractionOfJourney),
                                                  Mathf.Lerp(unboundedPosition.y, Mathf.Lerp(yStartPoint.position.y, yEndPoint.position.y, Input.mousePosition.y / (float)Screen.height), fractionOfJourney),
                                                  transform.position.z);
            transform.position = unboundedPosition;
        }
    }

    public void EnableConstraints(CameraBounds bounds, CameraBounds softBounds)
    {
        bounds.xMin += cameraScale.x;
        bounds.xMax -= cameraScale.x;
        bounds.yMin += cameraScale.y;
        bounds.yMax -= cameraScale.y;
        this.bounds = bounds;
        softBounds.xMin += cameraScale.x;
        softBounds.xMax -= cameraScale.x;
        softBounds.yMin += cameraScale.y;
        softBounds.yMax -= cameraScale.y;
        this.softBounds = softBounds;
        boundsEnabled = true;
    }
    public void DisableConstraints()
    {
        boundsEnabled = false;
    }

    private Vector3 GetMidpointOfPoints(List<targetPriority> targets)
    {
        Vector3 output = new Vector3(0, 0, 0);

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == targets[0])
            {
                output += targets[i].target.position * targets[i].startPriority;
            }
            else
            {
                float distanceModifier = targetCheckSize - Vector3.Distance(player.position, targets[i].target.position) / targetCheckSize;
                //Debug.Log(i + " " + distanceModifier);
                output += targets[i].target.position * (targets[i].startPriority * distanceModifier);
            }
            
        }
        
        return output / targets.Count;

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0, 0.5f);

        Gizmos.DrawWireSphere(player.position, targetCheckSize);

        Gizmos.color = new Color(0, 0, 255, 0);

        Gizmos.DrawSphere(GetMidpointOfPoints(targets), 1);

        Gizmos.color = new Color(255, 0, 0, 0.3f);
        Gizmos.DrawCube(new Vector3(bounds.xMin, bounds.yMin), new Vector3(1,1,1));
        Gizmos.DrawCube(new Vector3(bounds.xMax, bounds.yMin), new Vector3(1, 1, 1));
        Gizmos.DrawCube(new Vector3(bounds.xMin, bounds.yMax), new Vector3(1, 1, 1));
        Gizmos.DrawCube(new Vector3(bounds.xMax, bounds.yMax), new Vector3(1, 1, 1));
        Gizmos.color = new Color(255, 255, 0, 0.3f);
        Gizmos.DrawCube(new Vector3(softBounds.xMin, softBounds.yMin), new Vector3(1, 1, 1));
        Gizmos.DrawCube(new Vector3(softBounds.xMax, softBounds.yMin), new Vector3(1, 1, 1));
        Gizmos.DrawCube(new Vector3(softBounds.xMin, softBounds.yMax), new Vector3(1, 1, 1));
        Gizmos.DrawCube(new Vector3(softBounds.xMax, softBounds.yMax), new Vector3(1, 1, 1));
        Gizmos.color = new Color(0, 0, 255, 0.3f);
        Gizmos.DrawCube((Vector3)targetPos, new Vector3(1, 1, 1));
        Gizmos.color = new Color(0, 255, 255, 0.3f);
        Gizmos.DrawCube(unCapTargetPos, new Vector3(1, 1, 1));
    }
}
