
using UnityEngine;

public struct PlayerInformation
{
    public Vector2 position;
}
public static class StaticInfo
{
    private static PlayerInformation playerInfo;

    public static PlayerInformation PlayerInfo
    {
        get => playerInfo;
    }
    public static void GetPlayerPos(Vector2 pos)
    {
        playerInfo.position=pos;
    }
}
