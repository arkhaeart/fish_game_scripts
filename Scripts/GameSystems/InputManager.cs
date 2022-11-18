using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    System.Action<Direction> callback;
    List<Direction> currentPossible;
    Vector2 startingPoint;
    Vector2 endingPoint;
    Vector2 swipe;
    const float minSwipeLength = 100f;
    MyCour handlingInput;

    private void Start()
    {
        handlingInput = new MyCour(this, HandlingInput);
        FishController.HandleInputOn += HandleInput;
        FishController.HandleInputOff += HandleInput;
    }
    private void OnDisable()
    {
        FishController.HandleInputOn -= HandleInput;
        FishController.HandleInputOff -= HandleInput;
    }
    public void HandleInput(Direction[] directions, System.Action<Direction> callback)
    {
        this.callback = callback;
        currentPossible = directions.ToList();
        Debug.Log("Possible directions:");
        foreach(var dir in directions)
        {
            Debug.Log(dir);
        }
        handlingInput.Run();
    }

    public void HandleInput()
    {
        callback = null;
        handlingInput.Stop();
    }

    public void SwipeDetected(Direction global)
    {
        Debug.Log($"{global} swipe was detected");
        callback?.Invoke(global);
    }

    IEnumerator HandlingInput()
    {

        while(true)
        {
#if(UNITY_EDITOR)
            DetectMouseSwipe();
#else
            DetectSwipe();
#endif
            yield return null;
        }
    }

    void DetectSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began )
            {
                Debug.Log("Input started...");
                startingPoint = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                Debug.Log("Input ended!");
                endingPoint = new Vector2(t.position.x, t.position.y);
                swipe = endingPoint - startingPoint;

                if (swipe.magnitude < minSwipeLength)
                {
                    Debug.Log("Wasnt recognized as swipe");
                    return;
                }
                Debug.Log("Recognized as swipe");
                swipe.Normalize();

                if (swipe.y > 0 && swipe.x > -0.5f && swipe.x < 0.5f && currentPossible.Contains(Direction.UP))
                {
                    SwipeDetected(Direction.UP);
                }
                else if (swipe.y < 0 && swipe.x > -0.5f && swipe.x < 0.5f && currentPossible.Contains(Direction.DOWN))
                {
                    SwipeDetected(Direction.DOWN);
                }
                else if (swipe.x < 0 && swipe.y > -0.5f && swipe.y < 0.5f && currentPossible.Contains(Direction.LEFT))
                {
                    SwipeDetected(Direction.LEFT);
                }
                else if (swipe.x > 0 && swipe.y > -0.5f && swipe.y < 0.5f && currentPossible.Contains(Direction.RIGHT))
                {
                    SwipeDetected(Direction.RIGHT);

                }
            }
        }
        
    }
    void DetectMouseSwipe()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            Vector3 pos = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Input started...");
                startingPoint = new Vector2(pos.x, pos.y);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Input ended!");
                endingPoint = new Vector2(pos.x, pos.y);
                swipe = endingPoint - startingPoint;

                if (swipe.magnitude < minSwipeLength)
                {
                    Debug.Log("Wasnt recognized as swipe");
                    return;
                }
                Debug.Log("Recognized as swipe");
                swipe.Normalize();

                if (swipe.y > 0 && swipe.x > -0.5f && swipe.x < 0.5f && currentPossible.Contains(Direction.UP))
                {
                    SwipeDetected(Direction.UP);
                }
                else if (swipe.y < 0 && swipe.x > -0.5f && swipe.x < 0.5f && currentPossible.Contains(Direction.DOWN))
                {
                    SwipeDetected(Direction.DOWN);
                }
                else if (swipe.x < 0 && swipe.y > -0.5f && swipe.y < 0.5f && currentPossible.Contains(Direction.LEFT))
                {
                    SwipeDetected(Direction.LEFT);
                }
                else if (swipe.x > 0 && swipe.y > -0.5f && swipe.y < 0.5f && currentPossible.Contains(Direction.RIGHT))
                {
                    SwipeDetected(Direction.RIGHT);

                }
            }
        }
    }
}
