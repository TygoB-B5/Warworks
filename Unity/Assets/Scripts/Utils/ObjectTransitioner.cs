using System.Collections;
using UnityEngine;

public class ObjectTransitioner
{
    /// <summary>
    /// Moves object position and rotation to pose.
    /// </summary>
    public static IEnumerator MoveObject(Transform movableObject, Pose pose, float transitionSpeed, bool useForwardRotation = false)
    {
        Quaternion rot = pose.rotation;
        if (useForwardRotation)
        {
            rot = Quaternion.LookRotation(pose.forward);
        }

        Vector3 initialPosition = movableObject.transform.position;
        Quaternion initialRotation = movableObject.transform.rotation;

        float transitionProgress = 0;

        // Move if target is not reached.
        while (transitionProgress != 1)
        {
            yield return new WaitForEndOfFrame();
            transitionProgress += Time.deltaTime * transitionSpeed;
            transitionProgress = Mathf.Clamp01(transitionProgress);

            movableObject.transform.SetPositionAndRotation(
                Vector3.Lerp(initialPosition, pose.position, Mathf.Sin(transitionProgress * Mathf.PI * 0.5f)),
                Quaternion.Lerp(initialRotation, rot, Mathf.Sin(transitionProgress * Mathf.PI * 0.5f))
            );
        }

        // Set final precise pose.
        movableObject.SetPositionAndRotation(
            pose.position,
            rot
        );

        yield return null;
    }

    public static IEnumerator MoveObject(Transform movableObject, Vector3 position, float transitionSpeed)
    {
        return MoveObject(movableObject, new Pose(position, movableObject.rotation), transitionSpeed);
    }
}