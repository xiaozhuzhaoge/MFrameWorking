using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction {

    public SkillAction() { }
    public SkillAction(string name,float time,int buttonIndex) { this.stateName = name; this.normlizedTime = time; this.buttonIndex = buttonIndex; }

    public string stateName;
    public float normlizedTime;
    public int buttonIndex;
}
