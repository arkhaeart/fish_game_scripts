using GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapPreview : MonoBehaviour
{
    public MapCheckPointIcon[] icons;
    [Inject]
    public void Init(GameManager gameManager)
    {
        if(gameManager.CheckGameConfig(out CheckPointEntry entry))
        {
            if(icons.Length>=entry.index)
            {
                if(icons[entry.index].index==entry.index)
                {
                    icons[entry.index].Activate(entry);
                }
                else
                {
                    foreach(var icon in icons)
                    {
                        if(icon.index==entry.index)
                        {
                            icon.Activate(entry);
                        }
                    }
                }
            }
        }
    }
}
