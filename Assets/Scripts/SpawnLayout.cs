using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Layout", menuName = "Rowket/Spawn Layout", order = 0)]
public class SpawnLayout : JMilesScriptableObject
{
    public Vector3[] Positions;

    public Vector3 GetSpawnPos(int index,bool redTeam = true)
    {
        //Gets spawn pos
        //  Red team spawns at pos
        //  Blue team spawns at -z pos
        return redTeam ? Positions[index] : new Vector3(Positions[index].x, Positions[index].y, -Positions[index].z);
    }
}
