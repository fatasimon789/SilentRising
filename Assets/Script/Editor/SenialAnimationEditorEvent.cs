using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class SeniaAnimationEventEditor : EditorWindow
{
    [MenuItem("Senia/SeniaAnimationEvent Editor", false, 6)]
    static void SeniaAnimationEventEditorMenu()
    {
        EditorWindow.GetWindow(typeof(SeniaAnimationEventEditor));
    }


    public class AnimationEventItem
    {
        public AnimationEventItem(AnimationEvent animationEvent)
        {
            this.animationEvent = animationEvent;
        }

        public int selectedIndex = -1;
        public AnimationEvent animationEvent;
    }






    Vector2 scrollPos;
    int selectedIndex;


    Animator sourceAnimator;
    AnimationClip currentClip;
    List<MethodInfo> listEventMethod;
    string[] arrayEventMethodName;
    List<AnimationEventItem> listAnimEventItem;


    void OnGUI()
    {
        Animator tmpAnimator = EditorGUILayout.ObjectField("Animator Object", sourceAnimator, typeof(Animator), true) as Animator;
        if (tmpAnimator == null)
        {
            sourceAnimator = null;
            listEventMethod = null;
            listAnimEventItem = null;

            return;
        }
        if (sourceAnimator != tmpAnimator)
        {
            sourceAnimator = tmpAnimator;

            selectedIndex = 0;

            MonoBehaviour[] arrayMonoBehaviour = sourceAnimator.GetComponents<MonoBehaviour>();
            listEventMethod = new List<MethodInfo>();
            List<string> tmpNames = new List<string>();

            foreach (MonoBehaviour mono in arrayMonoBehaviour)
            {
                Type type = mono.GetType();
                MethodInfo[] arrayMethodInfo = type.GetMethods();

                IEnumerable<MethodInfo> tmpInfos = arrayMethodInfo.Where
                (
                    p =>
                    p.IsPublic &&
                    p.ReturnType == typeof(void) &&
                    (p.GetParameters().Select(q => q.ParameterType).SequenceEqual(new Type[] { }) ||
                    p.GetParameters().Select(q => q.ParameterType).SequenceEqual(new Type[] { typeof(int) }) ||
                    p.GetParameters().Select(q => q.ParameterType.BaseType).SequenceEqual(new Type[] { typeof(Enum) }) ||
                    p.GetParameters().Select(q => q.ParameterType).SequenceEqual(new Type[] { typeof(float) }) ||
                    p.GetParameters().Select(q => q.ParameterType).SequenceEqual(new Type[] { typeof(string) }) ||
                    p.GetParameters().Select(q => q.ParameterType).SequenceEqual(new Type[] { typeof(UnityEngine.Object) }))
                );
                listEventMethod.AddRange(tmpInfos);

                foreach (MethodInfo info in tmpInfos)
                {
                    ParameterInfo[] paramInfo = info.GetParameters();
                    if (paramInfo.Length == 0)
                    {
                        tmpNames.Add(type + "." + info.Name + " ( )");
                    }
                    else
                    {
                        tmpNames.Add(type + "." + info.Name + " ( " + paramInfo[0].ParameterType + " )");
                    }
                }
            }

            arrayEventMethodName = tmpNames.ToArray();

        }


        List<string> listClipName = new List<string>();
        foreach (AnimationClip clip in sourceAnimator.runtimeAnimatorController.animationClips)
        {
            listClipName.Add(clip.name);
        }
        selectedIndex = EditorGUILayout.Popup(selectedIndex, listClipName.ToArray());


        AnimationClip tmpClip = sourceAnimator.runtimeAnimatorController.animationClips[selectedIndex];
        if (tmpClip == null)
        {
            return;
        }
        if (currentClip != tmpClip)
        {
            currentClip = tmpClip;

            Debug.Log("currentClip=" + currentClip);

            // create list for editor UI display
            listAnimEventItem = new List<AnimationEventItem>();
            foreach (AnimationEvent animEvent in currentClip.events)
            {
                listAnimEventItem.Add(new AnimationEventItem(animEvent));
            }

        }


        if (listAnimEventItem == null || listEventMethod == null)
        {
            Debug.LogError("listAnimEventItem=" + listAnimEventItem + ", listEventMethod=" + listEventMethod);
            sourceAnimator = null;
            return;
        }


        // add new event button
        /*if (GUILayout.Button("Add Event"))
        {
            listAnimEvent.Add(new AnimationEvent());
        }*/




        //
        decimal frameTime = (1.0m / new Decimal(currentClip.frameRate));
        EditorGUILayout.LabelField("FrameTime=" + frameTime);


        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        int currentFrameText = -1;

        foreach (AnimationEventItem item in listAnimEventItem)
        {
            AnimationEvent animEvent = item.animationEvent;

            //
            int frame = (int)Decimal.Round(new Decimal(animEvent.time) / frameTime);
            if (frame > currentFrameText)
            {
                currentFrameText = frame;
                EditorGUILayout.PrefixLabel("Frame " + currentFrameText);
            }

            //
            EditorGUI.indentLevel++;

            //
            if (item.selectedIndex == -1)
            {
                item.selectedIndex = listEventMethod.FindIndex((MethodInfo x) => (x.Name == animEvent.functionName));
            }
            item.selectedIndex = EditorGUILayout.Popup("functionName", item.selectedIndex, arrayEventMethodName);
            if (item.selectedIndex == -1)
            {
                Debug.LogError("functionName=" + animEvent.functionName);
                continue;
            }
            else
            {
                animEvent.functionName = listEventMethod[item.selectedIndex].Name;
            }


            //
            animEvent.time = Decimal.ToSingle(new Decimal(EditorGUILayout.IntField("frame", frame)) * frameTime);

            //
            MethodInfo info = listEventMethod[item.selectedIndex];
            ParameterInfo[] arrayParameterInfo = info.GetParameters();
            if (arrayParameterInfo.Length == 1)
            {
                EditorGUI.indentLevel++;

                Type paramType = arrayParameterInfo[0].ParameterType;
                if (paramType == typeof(int) || paramType.BaseType == typeof(Enum))
                {
                    animEvent.intParameter = EditorGUILayout.IntField("intParameter", animEvent.intParameter);
                }
                else if (paramType == typeof(float))
                {
                    animEvent.floatParameter = EditorGUILayout.FloatField("floatParameter", animEvent.floatParameter);
                }
                else if (paramType == typeof(string))
                {
                    animEvent.stringParameter = EditorGUILayout.TextField("stringParameter", animEvent.stringParameter);
                }
                else if (paramType == typeof(UnityEngine.Object))
                {
                    animEvent.objectReferenceParameter = EditorGUILayout.ObjectField("objectReferenceParameter", animEvent.objectReferenceParameter, typeof(UnityEngine.Object), true);
                }

                EditorGUI.indentLevel--;
            }


            //
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
        }

        EditorGUILayout.EndScrollView();

        GUI.color = Color.green;
        if (GUILayout.Button("Save"))
        {
            SaveAnimation();
            AssetDatabase.SaveAssets();
            Debug.Log("Save: currentClip=" + currentClip);
        }
        GUI.color = Color.white;


    }

    void SaveAnimation()
    {
        if (currentClip != null && listAnimEventItem != null)
        {
            List<AnimationEvent> tmpList = new List<AnimationEvent>();
            foreach (AnimationEventItem item in listAnimEventItem)
            {
                tmpList.Add(item.animationEvent);
            }
            AnimationUtility.SetAnimationEvents(currentClip, tmpList.ToArray());
        }
    }

    void OnFocus()
    {
        // reload animation clip
        currentClip = null;
    }


    void OnLostFocus()
    {
        SaveAnimation();
    }

}
