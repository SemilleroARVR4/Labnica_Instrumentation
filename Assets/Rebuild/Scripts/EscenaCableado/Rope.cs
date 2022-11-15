using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform player;
    public Transform origin;

    public LineRenderer rope;
    public LayerMask collMask;

    public float minCollisionDistance;

    public List<Vector3> ropePositions { get; set; } = new List<Vector3>();

    private void Awake() => AddPosToRope(origin.position);

    private void Update()
    {
        UpdateRopePositions();
        LastSegmentGoToPlayerPos();

        DetectCollisionEnter();
        if (ropePositions.Count > 2) DetectCollisionExits();
    }

    private void UpdateRopePositions()
    {
        rope.positionCount = ropePositions.Count;
        ropePositions[0] = origin.transform.position;
        rope.SetPositions(ropePositions.ToArray());
    }

    private void LastSegmentGoToPlayerPos() => rope.SetPosition(rope.positionCount - 1, player.position);

    private void DetectCollisionEnter()
    {
        RaycastHit hit;

        if (Physics.Linecast(player.position, rope.GetPosition(ropePositions.Count - 2), out hit, collMask))
        {
            if (System.Math.Abs(Vector3.Distance(rope.GetPosition(ropePositions.Count - 2), hit.point)) > minCollisionDistance)
            {
                ropePositions.RemoveAt(ropePositions.Count - 1);
                AddPosToRope(hit.point);
            }
        }

        if (Physics.Linecast(origin.position, rope.GetPosition(1), out hit, collMask))
        {
            if (System.Math.Abs(Vector3.Distance(rope.GetPosition(1), hit.point)) > minCollisionDistance)
            {
                ropePositions.RemoveAt(0);
                ropePositions.Insert(0, hit.point);
                ropePositions.Insert(0, origin.position);
            }
        }
    }

    private void DetectCollisionExits()
    {
        RaycastHit hit;

        if (!Physics.Linecast(player.position, rope.GetPosition(ropePositions.Count - 3), out hit, collMask))
        {
            ropePositions.RemoveAt(ropePositions.Count - 2);
        }
        if (!Physics.Linecast(origin.position, rope.GetPosition(1), out hit, collMask))
        {
            ropePositions.RemoveAt(1);
        }

    }

    private void AddPosToRope(Vector3 _pos)
    {
        ropePositions.Add(_pos);
        ropePositions.Add(player.position); //Always the last pos must be the player
    }

    

    
}
