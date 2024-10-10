using System;
using UnityEditor;
using UnityEngine;

// Custom editor for the Tags (Tags) script
[CustomEditor(typeof(Tags))]
public class CustomInspectorTags : Editor
{
    public override void OnInspectorGUI()
    {
        // Get a reference to the Tags component
        Tags tags = (Tags)target;

        // Draw General Tags
        EditorGUILayout.LabelField("General Tags", EditorStyles.boldLabel);
        tags.objectName = EditorGUILayout.TextField("Object Name", tags.objectName);
        tags.objectDescription = EditorGUILayout.TextArea(tags.objectDescription);
        tags.objectType = (ObjectType)EditorGUILayout.EnumPopup("Object Type", tags.objectType);
        tags.category = (ObjectCategory)EditorGUILayout.EnumPopup("Category", tags.category);
        // Mark the object as dirty to ensure changes are saved
        EditorUtility.SetDirty(tags);
    }
}
