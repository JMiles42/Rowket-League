using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Layout", menuName = "Rowket/Spawn Layout", order = 0)]
public class SpawnLayout : JMilesScriptableObject
{
    public Vector3[] Positions;

    public Vector3 GetSpawnPos(int index,bool redTeam = true)
    {
        return redTeam ? Positions[index] : new Vector3(Positions[index].x, Positions[index].y, -Positions[index].z);
    }
}
