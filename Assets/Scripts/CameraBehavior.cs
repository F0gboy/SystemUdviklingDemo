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

    private void Update()
    {
        if (player != null && parent != null)
        {
            parent.transform.position = player.position;
        }

        if (targets.Count > 0)
        {
            Vector3 midpoint = GetMidpointOfPoints(targets);

            Debug.Log(midpoint);

            parent.transform.position = Vector3.Lerp(parent.transform.position, midpoint, globalCamSpeed * Time.deltaTime);
        }

        transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, Mathf.Lerp(xStartPoint.localPosition.x, xEndPoint.localPosition.x, Input.mousePosition.x / (float)Screen.width), fractionOfJourney),
                                              Mathf.Lerp(transform.localPosition.y, Mathf.Lerp(yStartPoint.localPosition.y, yEndPoint.localPosition.y, Input.mousePosition.y / (float)Screen.height), fractionOfJourney),
                                              transform.localPosition.z);

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
    }
}
