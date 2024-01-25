using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler
{
    public int Kills { get; private set; }
    public int Deaths{ get; private set; }
    public int Assists{ get; private set; }

    public void UpdateScore(int kills = 0, int deaths = 0, int assists = 0)
    {
        Kills += kills;
        Deaths += deaths;
        Assists += assists;
    }
}
