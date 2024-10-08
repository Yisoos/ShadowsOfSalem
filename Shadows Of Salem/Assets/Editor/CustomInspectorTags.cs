using System;
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
        switch (tags.objectType)
        {
            case ObjectType.Lock:
                // Code for when objectType is Lock
                EditorGUILayout.LabelField("Lock Tags", EditorStyles.boldLabel);
                tags.isLocked = EditorGUILayout.Toggle("Is Locked", tags.isLocked);
                tags.keyID = EditorGUILayout.IntField("Key ID", tags.keyID);
                tags.unlockCombination = EditorGUILayout.IntField("Unlock Combination", tags.unlockCombination);
               
                break;

            case ObjectType.DetailedView:
                // Code for when objectType is CloseUp
                EditorGUILayout.LabelField("Close-Up Tags", EditorStyles.boldLabel);
                tags.referenceItem = (GameObject)EditorGUILayout.ObjectField("Reference Prefab", tags.referenceItem, typeof(GameObject), true);
                EditorGUILayout.HelpBox("\"Reference Prefrab\" refers to the close-up view that will be initialized by this GameObject.", MessageType.Info);
                EditorGUILayout.LabelField("Dependencies", EditorStyles.boldLabel);

                // Checkbox for lock dependency
                tags.lockDependency = EditorGUILayout.Toggle("Lock Dependency", tags.lockDependency);

                // If lockDependency is checked, allow selection of the required lock
                if (tags.lockDependency)
                {
                    // Assuming you still want to allow selection of a GameObject for the required lock
                    tags.requiredLock = (GameObject)EditorGUILayout.ObjectField("Required Lock", tags.requiredLock, typeof(GameObject), true);
                    EditorGUILayout.HelpBox("This object requires the selected lock to be unlocked before accessing it.", MessageType.Info);
                }
                else
                {
                    tags.isLocked = false;
                }

                // If dependency is not initialized, initialize it
                if (tags.dependency == null)
                {
                    tags.dependency = new string[0];
                }

                // Loop through the dependency array
                for (int i = 0; i < tags.dependency.Length; i++)
                {
                    // Use TextField to allow string input for each dependency
                    tags.dependency[i] = EditorGUILayout.TextField($"Dependency {i + 1}", tags.dependency[i]);
                }

                // Buttons to add or remove elements from dependency array
                if (GUILayout.Button("Add Dependency"))
                {
                    Array.Resize(ref tags.dependency, tags.dependency.Length + 1);
                }

                if (tags.dependency.Length > 0 && GUILayout.Button("Remove Last Dependency"))
                {
                    Array.Resize(ref tags.dependency, tags.dependency.Length - 1);
                }

                break;

            case ObjectType.Key:
                // Code for when objectType is Key
                tags.keyID = EditorGUILayout.IntField("Key ID", tags.keyID);

                tags.isLocked = false;
                break;

            case ObjectType.Inspectable:
                EditorGUILayout.LabelField("Pop-Up Item Tags", EditorStyles.boldLabel);
                tags.referenceItem = (GameObject)EditorGUILayout.ObjectField("Reference Prefab", tags.referenceItem, typeof(GameObject), true);
                EditorGUILayout.HelpBox("\"Reference Prefrab\" refers to a prefab that will pop up on screen (UI Item).", MessageType.Info);
                break;

            case ObjectType.PopUpWindow:
                EditorGUILayout.LabelField("Detailed View Tags", EditorStyles.boldLabel);
                tags.referenceItem = (GameObject)EditorGUILayout.ObjectField("Reference Prefab", tags.referenceItem, typeof(GameObject), true);
                EditorGUILayout.HelpBox("\"Reference Prefrab\" refers to the inspectable prefab associated with the pop up window (UI Item).", MessageType.Info);
                break;


            default:
                // Code for other cases

                EditorGUILayout.LabelField("Dependencies", EditorStyles.boldLabel);

                // Checkbox for lock dependency
                tags.lockDependency = EditorGUILayout.Toggle("Lock Dependency", tags.lockDependency);

                // If lockDependency is checked, allow selection of the required lock
                if (tags.lockDependency)
                {
                    // Assuming you still want to allow selection of a GameObject for the required lock
                    tags.requiredLock = (GameObject)EditorGUILayout.ObjectField("Required Lock", tags.requiredLock, typeof(GameObject), true);
                    EditorGUILayout.HelpBox("This object requires the selected lock to be unlocked before accessing it.", MessageType.Info);
                }
                else
                {
                    tags.isLocked = false;
                }

                // If dependency is not initialized, initialize it
                if (tags.dependency == null)
                {
                    tags.dependency = new string[0];
                }

                // Loop through the dependency array
                for (int i = 0; i < tags.dependency.Length; i++)
                {
                    // Use TextField to allow string input for each dependency
                    tags.dependency[i] = EditorGUILayout.TextField($"Dependency {i + 1}", tags.dependency[i]);
                }

                // Buttons to add or remove elements from dependency array
                if (GUILayout.Button("Add Dependency"))
                {
                    Array.Resize(ref tags.dependency, tags.dependency.Length + 1);
                }

                if (tags.dependency.Length > 0 && GUILayout.Button("Remove Last Dependency"))
                {
                    Array.Resize(ref tags.dependency, tags.dependency.Length - 1);
                }

                break;
        }
        // Mark the object as dirty to ensure changes are saved
        EditorUtility.SetDirty(tags);
    }
}
