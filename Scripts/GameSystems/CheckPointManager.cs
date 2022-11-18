using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CheckPointManager
{
    public CheckPointManager()
    {
        FishController.OnCheckPoint = CheckPoint;
    }
    List<CheckPointEntry> entries = new List<CheckPointEntry>();
    CheckPointEntry last;
    public bool TryGetCheckPoint(out CheckPointEntry entry)
    {
        Debug.Log($"Trying to get checkPoint");
        if (entries.Count > 0)
        {
            Debug.Log("Got checkpoint");
            entry = last;
            return true;
        }
        else 
        {
            Debug.Log("Failed to get CheckPoint");
            entry = null;
            return false; 
        }
    }
    void CheckPoint(TurningApproach approach,Direction direction,TurningPoint point,int index)
    {
        Debug.Log($"CheckPoint! {approach} from {direction}");
        if (!entries.Any((n) => n.point == point))
        {
            Debug.Log("Added new");
            entries.Add(new CheckPointEntry(approach.transform.position, direction, point,index));
            last = entries.Last();
        }
        else
        {
            Debug.Log("Updated existing");
            CheckPointEntry entry = entries.Single((n) => n.point == point);
            //entry.Update(approach.transform.position, direction);
            last = entry;
        }

    }

}
public class CheckPointEntry
{
    public Vector3 pos;
    public Direction direction;
    public readonly TurningPoint point;
    public int index;
    public CheckPointEntry(Vector3 pos, Direction direction, TurningPoint point,int index)
    {
        this.pos = pos;
        this.direction = direction;
        this.point = point;
        this.index = index;
    }

    public void Update(Vector3 position, Direction direction)
    {
        pos = position;
        this.direction = direction;
    }
}
