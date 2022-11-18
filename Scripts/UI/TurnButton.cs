using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
    {
        
        UP,
        LEFT,
        DOWN,
        RIGHT
        
    }
public class TurnButton : MonoBehaviour
{
    public System.Action<Direction> OnClick;
    public Direction direction;
    public Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void Clicked()
    {
        OnClick?.Invoke(direction);
    }
}
