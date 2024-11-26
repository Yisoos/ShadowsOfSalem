using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateDependentObject : MonoBehaviour
{
    public OrderedDependencies orderedDependencies; // Reference to the dependency script
    public GameObject objectToDeactivate; // The object to deactivate
    public GameObject objectToActivate;
    private bool hasDeactivated = false; // Ensures deactivation only happens once
    private bool isDeactivated = true;

    private void Update()
    {
        if (orderedDependencies == null)
        {
            Debug.LogError("OrderedDependencies is not assigned to DependencyDeactivator!");
            return;
        }

        // Check if the second dependency is met
        if (!hasDeactivated && isDeactivated && orderedDependencies.dependencyMet.Length > 1 && orderedDependencies.dependencyMet[1])
        {
            objectToDeactivate.SetActive(false); // Deactivate the object
            objectToActivate.SetActive(true);
            Debug.Log($"{objectToDeactivate.name} has been deactivated after 2nd dependency was met.");
            hasDeactivated = true; // Prevent further deactivations
            isDeactivated = false;
        }
    }
}
