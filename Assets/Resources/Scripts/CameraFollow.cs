using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float PanTime;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private float startTime;
    private float endTime;
    private bool currentlyInterpolating;

    private void Update()
    {
        if((Vector2)Camera.main.transform.position != (Vector2)transform.position)
        {
            if(!currentlyInterpolating)
            {
                startPosition = Camera.main.transform.position;
                endPosition = transform.position;
                startTime = Time.time;
                endTime = startTime + PanTime;
                currentlyInterpolating = true;
            }
            else
            {
                if ((Vector2)transform.position != endPosition)
                {
                    startPosition = Camera.main.transform.position;
                    endPosition = transform.position;
                    //float percentDone = Mathf.Clamp01(Mathf.Abs(Vector2.Distance(startPosition, Camera.main.transform.position) / Vector2.Distance(startPosition, endPosition)));
                    //endTime = (Time.time - startTime) / percentDone + startTime;
                    startTime = Time.time;
                    endTime = startTime + PanTime;
                }
            }
        }
        else
        {
            currentlyInterpolating = false;
        }
    }

    private void FixedUpdate()
    {
        if(currentlyInterpolating /*&& (Vector2)Camera.main.transform.position != startPosition*/ && Time.time != startTime)
        {
            float lerpPercent = Mathf.Clamp01((Time.time - startTime) / (endTime - startTime));

            Vector2 cameraPosition = Vector2.Lerp(startPosition, endPosition, lerpPercent);

            Camera.main.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, Camera.main.transform.position.z);
        }
    }

    //private void Awake()
    //{
    //    startPosition = Camera.main.transform.position;
    //    currentTargetLocation = transform.position;
    //    nextTargetLocation = transform.position;
    //    timeStarted = Time.time;
    //    endTime = timeStarted + PanSpeed;
    //}

    //private void Update()
    //{
    //    nextTargetLocation = transform.position;
    //}

    //bool test = false;

    //private void FixedUpdate()
    //{
    //    //if(cameraStartPosition != currentTargetLocation)
    //    //{
    //        Vector2 cameraPosition = Camera.main.transform.position;

    //        float lerpPercent = Mathf.Clamp01((Time.time - timeStarted) / endTime);

    //        if (lerpPercent > 0f)
    //        {
    //            if (nextTargetLocation != currentTargetLocation)
    //            {
    //                float var1 = Vector2.Distance(currentTargetLocation, startPosition);
    //                float var2 = Vector2.Distance(nextTargetLocation, startPosition);

    //                float originalLerp = lerpPercent;
    //                lerpPercent *= var1 / var2;

    //                endTime = (originalLerp * (endTime - timeStarted) + lerpPercent * timeStarted) / lerpPercent;
    //                currentTargetLocation = nextTargetLocation;
    //            }

    //            cameraPosition = Vector2.Lerp(startPosition, currentTargetLocation, lerpPercent);

    //            Camera.main.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -10);

    //            if (cameraPosition == currentTargetLocation && test)
    //            {
    //                startPosition = cameraPosition;
    //                timeStarted = Time.time;
    //                endTime = timeStarted + PanSpeed;
    //            }
    //        }
    //    //}
    //}
}
