using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine.Editor;
using System;

[CustomPropertyDrawer(typeof(AbilityPostion))]
public class PostionAbilityColliderDrawer : PropertyDrawer
{
    AbilityPostion abilityPos;
    private SerializedProperty _centerPostion;
    private SerializedProperty _rangeExtendBoxCollider;
    private SerializedProperty _rangeRadiusCircleCollider;
    private SerializedProperty _isBoxPostion;
    private SerializedProperty _isCirclePostion;
    private SerializedProperty _name;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        // fill out properties
  
        _centerPostion = property.FindPropertyRelative("centerPostion");
        _rangeExtendBoxCollider = property.FindPropertyRelative("rangeExtendBoxCollider");
        _rangeRadiusCircleCollider = property.FindPropertyRelative("rangeRadiusCircleCollider");
        _isBoxPostion = property.FindPropertyRelative("isBoxPostion");
        _isCirclePostion = property.FindPropertyRelative("isCirclePostion");
        _name = property.FindPropertyRelative("name");
        // drawing instuction here
        Rect foldOutbox = new Rect(position.min.x, position.min.y, position.size.x,EditorGUIUtility.singleLineHeight);
        property.isExpanded =  EditorGUI.Foldout(foldOutbox,property.isExpanded,label);
        if (property.isExpanded)
        {
           
            abilityPos = attribute as AbilityPostion;
            // draw our properties
            DrawIsBox(position);
            // add bool true on /off
            var isOffBoxPostion = _isBoxPostion.boolValue.Equals(abilityPos.isBoxPostion);
            var isOffCirclePostion = _isCirclePostion.boolValue.Equals(abilityPos.isCirclePostion);
           
            if (!isOffBoxPostion)
            {

                DrawName(position);
                DrawRangeExtendBoxCollider(position);
                GUI.enabled = false;

            }
            DrawIsCircle(position);
            if (!isOffCirclePostion) 
            {

                DrawName(position);
                DrawRangeExtendCircle(position);
                GUI.enabled = true;
            }

        }
       
        EditorGUI.EndProperty();
    }


    private void DrawIsBox(Rect position)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight;
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;
        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea,_isBoxPostion);
      
    }
    private void DrawIsCircle(Rect position)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * 3;
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;
        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea,_isCirclePostion);
    }

    private void DrawName(Rect position)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * 2;
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos,yPos, width, height);
        EditorGUI.PropertyField(drawArea,_name,new GUIContent("Name"));
    }

    private void DrawRangeExtendCircle(Rect position)
    {
        Rect drawArea = new Rect(position.min.x  ,
              position.min.y + EditorGUIUtility.singleLineHeight * 4,
              position.size.x * 0.5f , EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(drawArea,_rangeRadiusCircleCollider,new GUIContent("Range Circle Collider"));
    }

    private void DrawRangeExtendBoxCollider(Rect position)
    {
        Rect drawArea = new Rect(position.min.x,
              position.min.y + EditorGUIUtility.singleLineHeight * 4,
              position.size.x, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(drawArea, _rangeExtendBoxCollider, new GUIContent("Range Box Collider"));
    }

    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int totalLine = 1;
        if (property.isExpanded) 
        {
            totalLine += 5;
        }
        return (EditorGUIUtility.singleLineHeight * totalLine);
    }
}
