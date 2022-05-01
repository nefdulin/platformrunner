using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public enum ObstacleType
    {
        STATIC = 0,
        HORIZONTAL
    }

    public class Obstacle : MonoBehaviour
    {
        public ObstacleType Type;
    }

}