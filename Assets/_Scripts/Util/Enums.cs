using UnityEngine;

public enum Layers
{
    Default = 0,
    TransparentFX,
    IgnoreRaycast,
    Door,
    Water,
    UI,
    Player,
    Walls,
    Enemies
}

public enum GuardState
{
    PATROL = 0,
    ALERTED,
    CHASE,
    SEARCH
}

public enum AnimationTriggers
{
    Idle = 0,
    Walk,
    Attack,
    Hurt,
    Die,
}