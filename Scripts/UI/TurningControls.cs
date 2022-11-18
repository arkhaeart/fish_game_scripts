using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurningControls : MonoBehaviour
{
    public TurnButton[] controls;
    public GameObject controlsParent;
    public Dictionary<Direction, TurnButton> controlsDict = new Dictionary<Direction, TurnButton>();
    Action<Direction> callback;
    private void Awake()
    {
        FishController.HandleInputOn += HandleInput;
        FishController.HandleInputOff += HandleInput;
    }
    private void Start()
    {
        FillDict();
        InitButtons();
        
    }
    public void GameStart()
    {
        controlsParent.SetActive(true);
    }
    private void InitButtons()
    {
        foreach(var con in controls)
        {
            con.OnClick = Clicked;
        }
        HandleInput();
    }

    void FillDict()
    {
        foreach(var con in controls)
        {
            if(!controlsDict.ContainsKey(con.direction))
                controlsDict.Add(con.direction, con);
        }
    }
    private void OnDisable()
    {
        FishController.HandleInputOn -= HandleInput;
        FishController.HandleInputOff -= HandleInput;
    }
    void HandleInput(Direction[] possible,System.Action<Direction> callback)
    {
        this.callback = callback;
        foreach(var dir in possible)
        {
            controlsDict[dir].gameObject.SetActive(true);
        }
    }
    void HandleInput()
    {
        callback = null;
        foreach(var but in controlsDict.Values)
        {
            but.gameObject.SetActive(false);
        }
    }
    void Clicked(Direction dir)
    {
        callback?.Invoke(dir);
    }
    
}
