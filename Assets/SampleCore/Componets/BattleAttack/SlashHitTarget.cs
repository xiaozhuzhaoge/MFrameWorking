using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashHitTarget : MonoBehaviour {

    public bool OneHit = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(other.GetComponent<IHitAnalysis>() != null)
            {
                other.GetComponent<IHitAnalysis>().BeHit();
                if(OneHit)
                    gameObject.SetActive(false);
            }
        }
    }
}
