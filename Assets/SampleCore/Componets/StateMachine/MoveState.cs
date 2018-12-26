using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveState : PlayerStateBase {

    PlayerCtr mc;
    public MoveState()
    {
    }

    public MoveState(params object[] datas) : base(datas)
    {
        mc = owner.GetComponent<PlayerCtr>();
    }


    public override void OnUpdate()
    {
        Vector3 dir = JoySticker.Instance.dir;
        Vector3 moveDir = Camera.main.transform.TransformDirection(new Vector3(dir.x, 0, dir.y));
        moveDir.y = 0;
        cc.SimpleMove(moveDir * mc.speed);
        if (moveDir != Vector3.zero)
            cc.transform.rotation = Quaternion.Slerp(cc.transform.rotation, Quaternion.LookRotation(moveDir), mc.rotationSpeed * Time.fixedDeltaTime);
        ani.SetFloat("speed", dir.sqrMagnitude * mc.speed);
    }

    
}
