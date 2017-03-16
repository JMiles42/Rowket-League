using UnityEngine;

/// <summary>
/// A layout for spawning players
/// </summary>
[CreateAssetMenu(fileName = "Spawn Layout", menuName = "Rowket/Spawn Layout", order = 0)]
public class SpawnLayout : JMilesScriptableObject
{
    public Vector3[] Positions;

    public Vector3 GetSpawnPos(int index,bool redTeam = true)
    {
        //Gets spawn pos
        //  TeamOne team spawns at pos
        //  TeamTwo team spawns at -z pos
        return redTeam ? Positions[index] : new Vector3(Positions[index].x, Positions[index].y, -Positions[index].z);
    }
}
