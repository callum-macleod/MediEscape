using UnityEngine;

public enum Layers
{
    Default = 0,
    TransparentFX,
    IgnoreRaycast,
    UNUSEDLAYER,
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
