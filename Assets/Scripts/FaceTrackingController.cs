using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class FaceTrackingController : MonoBehaviour
{
    public ARFaceManager faceManager;
    public GameObject[] augmentedObjects; // Array of unique augmented objects

    private Dictionary<ARFace, GameObject> faceToObjectMapping = new Dictionary<ARFace, GameObject>();

    private void OnEnable()
    {
        faceManager.facesChanged += OnFacesChanged;
    }

    private void OnDisable()
    {
        faceManager.facesChanged -= OnFacesChanged;
    }

    void OnFacesChanged(ARFacesChangedEventArgs eventArgs)
    {
        foreach (var addedFace in eventArgs.added)
        {
            // Implement face recognition and mapping logic here
            // Associate addedFace with a specific augmented object

            // Instantiate and link the appropriate augmented object
            int chosenObjectIndex = 0; // Replace with the actual index of the chosen object
            GameObject augmentedObject = Instantiate(augmentedObjects[chosenObjectIndex], addedFace.transform);
            faceToObjectMapping.Add(addedFace, augmentedObject);
        }

        foreach (var removedFace in eventArgs.removed)
        {
            // Clean up and remove associated augmented object
            if (faceToObjectMapping.TryGetValue(removedFace, out GameObject obj))
            {
                Destroy(obj);
                faceToObjectMapping.Remove(removedFace);
            }
        }
    }
}
