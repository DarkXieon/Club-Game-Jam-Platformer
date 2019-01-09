using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallAndChainSwing : MonoBehaviour
{
    public HingeJoint2D BallJoint;
    public HingeJoint2D TopJoint;
    public HingeJoint2D RotationReader;

    public float StartRotation;
    public float CorrectionFactor;

    private Rigidbody2D ballBody;
    private Vector2 startPosition;
    private HingeJoint2D[] hingeJoints;

    private bool corrected;
    private float topPreviousRotation;
    private float bottomPreviousRotation;
    private float mult;

    private void Start()
    {
        ballBody = BallJoint.GetComponent<Rigidbody2D>();
        hingeJoints = transform.GetComponentsInChildren<HingeJoint2D>();

        startPosition = transform.position;

        foreach (HingeJoint2D joint in transform.GetComponentsInChildren<HingeJoint2D>())
        {
            //joint.transform.RotateAround(TopJoint.anchor, joint.transform.forward, StartRotation);
            joint.transform.RotateAround(transform.position, joint.transform.forward, StartRotation);
        }

        topPreviousRotation = StartRotation;
    }
    
    private void FixedUpdate()
    {
        float currentTopRotation = TopJoint.transform.eulerAngles.z - 180;
        //float currentBottomPosition = Vector3.Angle(startPosition, BallJoint.transform.position);
        //float currentTopPosition = Vector3.Angle(startPosition, TopJoint.transform.position);

        if (currentTopRotation < 0)
            currentTopRotation = -180f - currentTopRotation;
        else
            currentTopRotation = 180f - currentTopRotation;
        
        bool isRotatingRight = topPreviousRotation > currentTopRotation;
        
        if (isRotatingRight)
        {
            corrected = false;
        }
        else
        {
            if (!corrected && topPreviousRotation > -StartRotation)// && topPreviousRotation < 0)
            {
                float difference = StartRotation - topPreviousRotation;

                mult = difference;
                corrected = true;
            }
            
            if (corrected)
            {
                foreach(HingeJoint2D joint in hingeJoints)
                {
                    if(joint != BallJoint && joint != RotationReader)
                        joint.GetComponent<Rigidbody2D>().AddForce(-1 * TopJoint.transform.right * CorrectionFactor * Mathf.Abs(mult), ForceMode2D.Force);
                }

                //ballBody.AddForce(-1 * TopJoint.transform.right * CorrectionFactor * Mathf.Abs(mult), ForceMode2D.Force);
                //ballBody.AddRelativeForce(Vector2.left * CorrectionFactor * Mathf.Abs(mult), ForceMode2D.Force);
            }
        }

        topPreviousRotation = currentTopRotation;
    }
}
