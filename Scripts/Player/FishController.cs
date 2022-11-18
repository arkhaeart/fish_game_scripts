using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class FishController
{
    //public static event System.Action TurnOffAllWater;

    public static event System.Action<Direction[], Action<Direction>> HandleInputOn;
    public static event System.Action HandleInputOff;
    public static System.Action<TurningApproach, Direction, TurningPoint,int> OnCheckPoint;
    Fish fish;
    Fish.Settings settings;
    Direction current;
    MyCour moving, stopping;
    MyCour<float,Vector2> turning;
    MyCour<TurningPoint> movingTo;
    System.Action actionOnNextTurn;
    TurningPoint point;


    public FishController(Fish fish, Fish.Settings settings)
    {
        this.fish = fish;
        this.settings = settings.Copy();
        fish.speed = settings.speed;
        current = fish.transform.rotation.ToDirection();
        moving = new MyCour(fish, Moving);
        stopping = new MyCour(fish, Stopping);
        turning = new MyCour<float,Vector2>(fish, Turning);
        movingTo = new MyCour<TurningPoint>(fish, Moving);
        TurningZone.OnPlayerEntered = TurningZoneEntered;
        TurnCheck.OnTurn = TurnPointReached;

    }

    private void TurningZoneEntered(TurningApproach approach, TurningPoint point, bool isCheckPoint,int index)
    {
        fish.anim.SetIdleAnimation();

        if (isCheckPoint)
        {
            OnCheckPoint?.Invoke(approach, current, point,index);
        }
        Direction[] localPossible = approach.possibleDirections;
        this.point = point;
        Debug.Log($"Local possible direciotns: {localPossible.Length}");
        if (localPossible.Length > 1)
        {
            stopping.Run();

            Direction[] globalPossible = new Direction[localPossible.Length];
            for (int i = 0; i < globalPossible.Length; i++)
            {
                globalPossible[i] = localPossible[i].ToGlobal(current);
            }

            //moving.Stop();

            HandleInputOn?.Invoke(globalPossible, Input);
        }
        else
        {
            Input(localPossible[0].ToGlobal(current));
        }
    }
    void Input(Direction global)
    {
        if (point != null)
        {
            HandleInputOff?.Invoke();

            Direction local = global.ToLocal(current);
            Debug.Log($"Input received: global:{global}," +
                $"\n local: {local}" +
                $"\n current: {current}");
            current = global;
            RestoreMovement();
            NextTurn(local, point);
            point = null;
        }

    }

    void TurnPointReached()
    {
        actionOnNextTurn?.Invoke();
    }

    public void StartGame()
    {
        //TurnOffAllWater?.Invoke();
        fish.anim.SetForwardAnimation();
        moving.Run();
    }

    public void SetOnCheckPoint(Vector3 pos, Direction direction)
    {
        Debug.Log($"Setting fish to checkpoint!" +
            $"\nPos is: {pos}" +
            $"\ndirection is {direction}" +
            $"\nand rotation is {direction.ToRotation().eulerAngles}");
        fish.rigidbody.MovePosition(pos);
        current = direction;
        fish.transform.rotation = direction.ToRotation();
    }


    void NextTurn(Direction direction, TurningPoint point)
    {
        float angle = 0;
        switch (direction)
        {
            case Direction.LEFT:
                angle = 90;
                break;
            case Direction.UP:
                actionOnNextTurn = null;
                fish.anim.SetForwardAnimation();
                return;
            case Direction.RIGHT:
                angle = -90;
                break;
        }

        fish.anim.SetTurnAnimation(angle);

        actionOnNextTurn = () =>
        {
            turning.Run(angle,point.transform.position);
            actionOnNextTurn = null;
        };

        movingTo.Run(point);
    }

    void RestoreMovement()
    {
        stopping.Stop();
        fish.speed = settings.speed;
    }
    #region Coroutines
    IEnumerator Stopping()
    {
        float spdDcrsRt = settings.speedDecreaseRatio;

        while (fish.speed - spdDcrsRt * Time.deltaTime >= 0)
        {
            fish.speed -= spdDcrsRt * Time.deltaTime;
            yield return null;

        }
        fish.speed = 0;
    }
    IEnumerator Moving()
    {
        while (true)
        {
            Vector3 newPos = fish.transform.position + fish.transform.up * fish.speed * Time.deltaTime;
            fish.rigidbody.MovePosition(newPos);
            yield return null;
        }
    }

    IEnumerator Turning(float angle,Vector2 pos)
    {

        moving.Stop();
        int frames= settings.turnFrames / 3;
        Vector2 start = fish.transform.position;
        for (int i = 1; i <= frames;  i++)
        {
            float ratio = (float)i / (float)frames;
            Vector2 newPos = Vector2.Lerp(start, pos, ratio);
            fish.rigidbody.MovePosition(newPos);
            yield return null;
        }
        Quaternion goal = Quaternion.AngleAxis(angle, Vector3.forward) * fish.transform.rotation;
        for (int i = 0; i < settings.turnFrames; i++)
        {
            Quaternion lerped = Quaternion.Lerp(fish.transform.rotation, goal, settings.turnSpeed * Time.deltaTime);

            fish.rigidbody.MoveRotation(lerped);
            yield return null;
        }
        fish.rigidbody.MoveRotation(goal);
        moving.Run();
    }
    IEnumerator Moving(TurningPoint target)
    {
        moving.Stop();
        if (Mathf.Abs(fish.transform.position.x - target.transform.position.x) > Mathf.Abs(fish.transform.position.y - target.transform.position.y))
        {
            Vector3 alignPoint = new Vector3(
                Mathf.Lerp(fish.transform.position.x, target.transform.position.x, settings.alignLengthRatio),
                target.transform.position.y);
            for (int i = 0; i < settings.turnFrames / 5; i++)
            {
                Vector3 newPos = Vector3.MoveTowards(fish.transform.position, alignPoint, fish.speed * Time.deltaTime);
                fish.rigidbody.MovePosition(newPos);
                yield return null;
            }
            fish.rigidbody.MovePosition(alignPoint);
        }
        else
        {
            Vector3 alignPoint = new Vector3(
                target.transform.position.x,
                Mathf.Lerp(fish.transform.position.y, target.transform.position.y, settings.alignLengthRatio));
            for (int i = 0; i < settings.turnFrames / 5; i++)
            {
                Vector3 newPos = Vector3.MoveTowards(fish.transform.position, alignPoint, fish.speed * Time.deltaTime);
                fish.rigidbody.MovePosition(newPos);
                yield return null;
            }
            fish.rigidbody.MovePosition(alignPoint);
        }
        Debug.Log($"Align point reached" +
            $"\n Fish pos is {fish.transform.position}" +
            $"\n Target pos is {target.transform.position}");

        moving.Run();
    }
    #endregion
    public class Factory : PlaceholderFactory<Fish.Settings, FishController>
    {

    }
}
