using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class FireStocket : MonoBehaviour, IHitAnalysis
{
    PostProcessingBehaviour ppb;
    public float duration;
    float startTime = 0;
    public Color normal;
    public Color hit;
    Material mat;
 

    private void Start()
    {
        mat = transform.GetComponent<MeshRenderer>().material;
        MessageCenter.Instance.RegisterMessages("Destroy", gameObject, Callback);
    }

    private void Callback(object[] objects)
    {
        BeHit();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Hit(other);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerWeapon"))
        {
            BeHit(); 
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(Time.time - startTime > duration)
            {
                Hit(other);
            }
        }
    }
 
    public void Hit(Collider other)
    {
        other.GetComponent<CharacterCtrlBase>().fsm.SetState("HitBack" + UnityEngine.Random.Range(1, 3));
        startTime = Time.time;
    }

    public void BeHit(params object[] args)
    {
        mat.SetColor("_EmissionColor", hit);
        Utility.instance.WaitForFrame(3, () => { mat.SetColor("_EmissionColor", normal); });
       

        //if(args.Length >= 1)
        //{
        //    var go = ResourcesMgr.Instance.LoadResource("Dash01_1shot", ResourcesMgr.ResourceType.Effect);
        //    go.transform.position = (Vector3)(args[0]);
        //}
      
    }

}
