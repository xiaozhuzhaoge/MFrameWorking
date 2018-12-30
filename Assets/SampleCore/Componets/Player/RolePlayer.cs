using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePlayer : CharacterCtrlBase
{
    public Transform WeaponFollow;

    public SkinnedMeshRenderer weaponPoint;

    public override void OnAwake()
    {
        WeaponFollow = Utility.FindChild(transform, "WeaponPoint");
        if (WeaponFollow != null)
            WeaponFollow.gameObject.layer = LayerMask.NameToLayer(SelfWeaponLayer);
    }

    public override void CheckOnGround()
    {
        if (isJumpingUp == false)
        {
            if (RayCastGround == false)
            {
                fsm.SetState("Fall");
            }
        }
    }
    public override void OnUpdate()
    {
        CheckOnGround();
    }


    public void OpenBox() { WeaponFollow.gameObject.SetActive(true); }
    public void CloseBox() { WeaponFollow.gameObject.SetActive(false); }

}
