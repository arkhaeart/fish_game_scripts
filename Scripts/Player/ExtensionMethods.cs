using UnityEngine;

public static class ExtensionMethods 
{
   public static bool ApproxEq(this Vector3 pos, Vector3 pos1,float ratio=0.1f)
    {
        if((Mathf.Abs(pos.x-pos1.x))<=ratio)
        {
            if (Mathf.Abs(pos.y - pos1.y) <= ratio)
                return true;
        }
        return false;
    }

    public static Direction ToGlobal(this Direction local, Direction axis)
    {
        if (axis == Direction.UP)
            return local;
        else if (axis == Direction.DOWN)
            return local.Opposite();
        else if (local == Direction.UP)
            return axis;
        else if (local == axis)
            return Direction.DOWN;
        else return Direction.UP;
    }

    public static Direction ToDirection(this Quaternion quaternion)
    {
        Debug.Log($"euler angles: {quaternion.eulerAngles}");
        int dir =(int) quaternion.eulerAngles.z / 90;
        return (Direction)dir;

        //Vector3 euler = quaternion.eulerAngles;

        //if (euler == Vector3.zero)
        //    return Direction.UP;
        //else if (euler == new Vector3(0, 0, 90))
        //    return Direction.RIGHT;
        //else if (euler == new Vector3(0, 0, 180))
        //    return Direction.DOWN;
        //else return Direction.LEFT;
    }
    public static Quaternion ToRotation(this Direction direction)
    {
        Quaternion quat = Quaternion.Euler(0, 0, (int)direction*90);
        return quat;
    }
    public static void CheckIncrement(this ref int val, int check)
    {
        val++;
        if (val >= check)
            val = 0;
    }
    public static void CheckDecrement(this ref int val, int check)
    {
        val--;
        if (val < 0)
            val = check - 1;
    }
    public static Direction ToLocal(this Direction global,Direction axis)
    {
        if (global == axis)
            return Direction.UP;
        int checkRight = (int)global;
        checkRight.CheckIncrement(4);
        if ((int)axis == checkRight)
            return Direction.RIGHT;
        return Direction.LEFT;

    }
    public static Direction Opposite(this Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                return Direction.RIGHT;
            case Direction.UP:
                return Direction.DOWN;
            case Direction.RIGHT:
                return Direction.LEFT;
            case Direction.DOWN:
                return Direction.UP;
            default:
                throw new System.Exception("Useless exception");
        }
    }

}
