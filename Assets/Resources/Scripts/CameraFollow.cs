using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float PanSpeed;

    private Vector2 cameraStartPosition;
    private Vector2 currentTargetLocation;
    private Vector2 nextTargetLocation;
    private float timeStarted;
    private float endTime;

    private void Awake()
    {
        cameraStartPosition = Camera.main.transform.position;
        currentTargetLocation = transform.position;
        nextTargetLocation = transform.position;
        timeStarted = Time.time;
        endTime = timeStarted + PanSpeed;
    }
    
    private void Update()
    {
        nextTargetLocation = transform.position;
    }

    bool test = false;

    private void FixedUpdate()
    {
        //if(cameraStartPosition != currentTargetLocation)
        //{
            Vector2 cameraPosition = Camera.main.transform.position;

            float lerpPercent = Mathf.Clamp01((Time.time - timeStarted) / endTime);

            if (lerpPercent > 0f)
            {
                if (nextTargetLocation != currentTargetLocation)
                {
                    float var1 = Vector2.Distance(currentTargetLocation, cameraStartPosition);
                    float var2 = Vector2.Distance(nextTargetLocation, cameraStartPosition);

                    float originalLerp = lerpPercent;
                    lerpPercent *= var1 / var2;

                    endTime = (originalLerp * (endTime - timeStarted) + lerpPercent * timeStarted) / lerpPercent;
                    currentTargetLocation = nextTargetLocation;
                }

                cameraPosition = Vector2.Lerp(cameraStartPosition, currentTargetLocation, lerpPercent);

                Camera.main.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -10);

                if (cameraPosition == currentTargetLocation && test)
                {
                    cameraStartPosition = cameraPosition;
                    timeStarted = Time.time;
                    endTime = timeStarted + PanSpeed;
                }
            }
        //}
    }
}
