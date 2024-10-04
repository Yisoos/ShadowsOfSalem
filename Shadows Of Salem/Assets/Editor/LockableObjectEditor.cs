using UnityEditor;
using UnityEngine;

// Custom editor for the Tags (Tags) script
[CustomEditor(typeof(Tags))]
public class TagsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get a reference to the Tags component
        Tags tags = (Tags)target;

        // Draw General Tags
        EditorGUILayout.LabelField("General Tags", EditorStyles.boldLabel);
        tags.objectName = EditorGUILayout.TextField("Object Name", tags.objectName);
        tags.objectType = (ObjectType)EditorGUILayout.EnumPopup("Object Type", tags.objectType);

        if (tags.objectType == ObjectType.Key)
        {
            // Draw Inventory Tags only if not CloseUp
            EditorGUILayout.LabelField("Key Tags", EditorStyles.boldLabel);
            tags.keyID = EditorGUILayout.IntField("Key ID", tags.keyID);
        }

        // Draw Lock Tags only if objectType is Lock
        if (tags.objectType == ObjectType.Lock)
        {
            EditorGUILayout.LabelField("Lock Tags", EditorStyles.boldLabel);

            // Enable the Lock-related fields (isLocked, keyID, unlockCombination)
            tags.isLocked = EditorGUILayout.Toggle("Is Locked", tags.isLocked);
            tags.keyID = EditorGUILayout.IntField("Key ID", tags.keyID);
            tags.unlockCombination = EditorGUILayout.IntField("Unlock Combination", tags.unlockCombination);
        }
        // Hide isCollectable if objectType is CloseUp
        if ((tags.objectType != ObjectType.CloseUp ) && (tags.objectType != ObjectType.Lock))
        {
            // Draw Inventory Tags only if not CloseUp
            EditorGUILayout.LabelField("Inventory Tags", EditorStyles.boldLabel);
            tags.isCollectible = EditorGUILayout.Toggle("Is Collectible", tags.isCollectible);
        }

        // Mark the object as dirty to ensure changes are saved
        EditorUtility.SetDirty(tags);
    }
}
