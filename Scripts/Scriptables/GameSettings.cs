using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scriptables
{
    [CreateAssetMenu(menuName = "Game/Settings")]
    public class GameSettings : ScriptableObject
    {
        public Fish.Settings fish;
        public MiniMapHandler.Settings miniMap;
        public CameraFollow.Settings camera;
    }
}