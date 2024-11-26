using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateDependentObject : MonoBehaviour
{
    public OrderedDependencies orderedDependencies; // Reference to the dependency script
    public GameObject objectToDeactivate; // The object to deactivate
    public GameObject objectToActivate; // The object to activate (contains a collider)
    private bool hasDeactivated = false; // Ensures deactivation only happens once

    private void Start()
    {
        // Ensure the objects are set up correctly at the start
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(true); // Make sure it's active initially
        }
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false); // Make sure it's inactive initially
        }
    }

    private void Update()
    {
        // If the OrderedDependencies is not assigned, show an error and stop execution
        if (orderedDependencies == null)
        {
            Debug.LogError("OrderedDependencies is not assigned to DependencyDeactivator!");
            return;
        }

        // Check if the second dependency is met (dependencyMet[1] is true)
        if (!hasDeactivated && orderedDependencies.dependencyMet.Length >= 1 && orderedDependencies.dependencyMet[orderedDependencies.dependencyMet.Length-1])
        {
            Debug.Log("2nd dependency was met.");

            // Deactivate the first object and activate the second one
            objectToDeactivate.SetActive(false); // Deactivate the first object
            objectToActivate.SetActive(true);    // Activate the second object (collider will work if the object is active)

            // Log the action for debugging
            Debug.Log($"{objectToDeactivate.name} has been deactivated after the 2nd dependency was met.");

            // Prevent further deactivations
            hasDeactivated = true;
        }
    }
}
