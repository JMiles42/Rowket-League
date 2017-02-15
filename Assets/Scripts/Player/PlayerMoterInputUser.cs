using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputUser", menuName = "Rowket/Input/User", order = 0)]
public class PlayerMoterInputUser : PlayerMoterInputBase
{
    public override Vector3 GetInput()
    {
        return new Vector3(PlayerInputManager.Instance.Horizontal,PlayerInputManager.Instance.Vertical);
    }
}
