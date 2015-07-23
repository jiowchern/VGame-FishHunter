using UnityEngine;
using UnityEngine;
using System.Collections;

using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(NGUISpriteRotator))]
public class NGUISpriteRotatorInspector : Editor 
{
    override public void  OnInspectorGUI()
    {

        this.DrawDefaultInspector();
        var rotator = target as NGUISpriteRotator;


        if (rotator.Atlas != null)
        {
            GUILayout.BeginVertical();


            NGUIEditorTools.DrawAdvancedSpriteField(rotator.Atlas, "", _SelectSprite, false);

            foreach (var spriteName in rotator.SpriteNames)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(spriteName);
                if (GUILayout.Button("x"))
                {
                    rotator.Remove(spriteName);
                }
                GUILayout.EndHorizontal();

            }

            if (GUILayout.Button("Test Next"))
            {
                rotator.Next();                
            }
            GUILayout.EndVertical();    
        }
        
        
    }

    private void _SelectSprite(string sprite)
    {
        var rotator = target as NGUISpriteRotator;        
        rotator.Add(sprite);
    }
}
