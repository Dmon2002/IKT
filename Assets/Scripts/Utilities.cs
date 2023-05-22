using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static float ToAngle(this Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    public static List<Team> GetAgainstTeams(this Team team)
    {
        switch (team)
        {
            case Team.Player:
                return new () { Team.Enemy };
            case Team.Enemy:
                return new () { Team.Player };
            case Team.Neutral: 
                return new () { Team.Player, Team.Enemy };
            default:
                return new () { };
        }
    }
}
