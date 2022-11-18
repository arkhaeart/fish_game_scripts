using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    new public Rigidbody2D rigidbody;
    public FishAnimations anim;
    public float speed;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    [System.Serializable]
    public class Settings
    {
        public float speed = 2;
        public float turnSpeed = 10;
        public float speedDecreaseRatio = 0.25f;
        public float alignLengthRatio = 0.25f;
        public int turnFrames = 10;
        public Settings Copy()
        {
            return MemberwiseClone() as Settings;
        }
    }
}
