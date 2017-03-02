using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : JMilesBehaviour
{
    public bool b = false;
    void OnValidate()
    {
        name = " SP: " + Position;
    }

    public Vector3 GetSpawnPos(bool redTeam = true)
    {
        return redTeam ? Position : new Vector3(Position.x, Position.y, -Position.z);
    }
}
