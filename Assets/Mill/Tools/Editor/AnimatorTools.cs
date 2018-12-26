using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.IO;

public class AnimatorTools : EditorWindow {

    [MenuItem("动画处理/Animator")]
    static void Init() {
        AnimatorTools window = (AnimatorTools)EditorWindow.GetWindow(typeof(AnimatorTools), false, "Animator", true);
        window.Show();
    }
    static Object ani;

    public void OnGUI()
    {
        ani = EditorGUILayout.ObjectField(ani, typeof(UnityEngine.Object));
        if (GUILayout.Button("Insert Animation"))
        {
            AnimatorController aController = AnimatorController.CreateAnimatorControllerAtPath(Application.dataPath + "/Ani.controller");
            Object[] temp = Selection.GetFiltered<Object>(SelectionMode.Deep);
            for (int i = 0; i < temp.Length; i++)
            {
                Debug.Log(temp[i]);
            }
        }
    }

}

