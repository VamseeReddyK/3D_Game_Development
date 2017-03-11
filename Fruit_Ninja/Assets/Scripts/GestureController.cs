using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkelitenJoints
{
    SpineBase,	    //0 	Base of the spine
    SpineMid,	    //1 	Middle of the spine
    Neck,	        //2 	Neck
    Head,            //3 	Head
    ShoulderLeft,    //4 	Left shoulder
    ElbowLeft,	    //5 	Left elbow
    WristLeft,	    //6 	
    HandLeft,	    //7 	Left hand
    ShoulderRight,	//8 	Right shoulder
    ElbowRight,	    //9 	Right elbow
    WristRight,	    //10 	
    HandRight,	    //11 	Right hand
    HipLeft,         //12 	Left hip
    KneeLeft,	    //13 	Left knee
    AnkleLeft,	    //14 	Left ankle
    FootLeft,	    //15 	Left foot
    HipRight,	    //16 	Right hip
    KneeRight,	    //17 	Right knee
    AnkleRight,	    //18 	Right ankle
    FootRight,	    //19 	Right foot
    SpineShoulder,	//20 	Spine at the shoulder	
    HandTipLeft,	    //21 	Tip of the left hand
    ThumbLeft,	    //22 
    HandTipRight,    //23 	Tip of the right hand
    ThumbRight	    //24 	
}

public class GestureController : MonoBehaviour {

    KinectManager km;
    public bool isFlyActive = false;
    public bool isSliceActive = false;
    public bool isAttackActive = false;
    public bool isHookActive = false;
    public bool isThrowActive = false;
    public bool isDuckActive = false;
    public bool isJumpActive = false;

    Vector3 _elbowLeft = new Vector3(0,0,0);
    SmoothingFilter SmoothElbowLeft = new SmoothingFilter();
    Vector3 _elbowRight;
    SmoothingFilter SmoothElbowRight = new SmoothingFilter();
    Vector3 _wristLeft;
    SmoothingFilter SmoothWristLeft = new SmoothingFilter();
    public Vector3 _wristRight;
    SmoothingFilter SmoothWristRight = new SmoothingFilter();
    Vector3 _neck;
    SmoothingFilter SmoothNeck = new SmoothingFilter();
    Vector3 _spineBase;
    SmoothingFilter SmoothSpineBase = new SmoothingFilter();
    Vector3 _ankleRight;
    SmoothingFilter SmoothAnkleRight = new SmoothingFilter();
    Vector3 _ankleLeft;
    SmoothingFilter SmoothAnkleLeft = new SmoothingFilter();
    public Vector3 _shoulderRight;
    SmoothingFilter SmoothShoulderRight = new SmoothingFilter();
    Vector3 _shoulderLeft;
    SmoothingFilter SmoothShoulderLeft = new SmoothingFilter();
    Vector3 _kneeLeft;
    SmoothingFilter SmoothKneeLeft = new SmoothingFilter();

    private bool flySet = false;
    private bool throwSet = false;
    private bool jumpSet = false;
    private bool duckSet = false;
    private bool hookSet = false;
    // Use this for initialization
    void Start () {
        km = this.gameObject.GetComponent<KinectManager>();    
    }

    // Update is called once per frame
    void Update()
    {

        _elbowLeft = SmoothElbowLeft.Filter(km.GetJointPosition(km.GetPlayer1ID(), (int)SkelitenJoints.ElbowLeft));
        _elbowRight = SmoothElbowRight.Filter(km.GetJointPosition(km.GetPlayer1ID(), (int)SkelitenJoints.ElbowRight));
        _wristLeft = SmoothWristLeft.Filter(km.GetJointPosition(km.GetPlayer1ID(), (int)SkelitenJoints.WristLeft));
        _wristRight = SmoothWristRight.Filter(km.GetJointPosition(km.GetPlayer1ID(), (int)SkelitenJoints.WristRight));
        _neck = SmoothNeck.Filter(km.GetJointPosition(km.GetPlayer1ID(), (int)SkelitenJoints.Neck));
        _spineBase = SmoothSpineBase.Filter(km.GetJointPosition(km.GetPlayer1ID(), (int)SkelitenJoints.SpineBase));
        _ankleRight = SmoothAnkleRight.Filter(km.GetJointPosition(km.GetPlayer1ID(), (int)SkelitenJoints.AnkleRight));
        _ankleLeft = SmoothAnkleLeft.Filter(km.GetJointPosition(km.GetPlayer1ID(), (int)SkelitenJoints.AnkleLeft));
        _shoulderLeft = SmoothShoulderLeft.Filter(km.GetJointPosition(km.GetPlayer1ID(), (int)SkelitenJoints.ShoulderLeft));
        _shoulderRight = SmoothShoulderRight.Filter(km.GetJointPosition(km.GetPlayer1ID(), (int)SkelitenJoints.ShoulderRight));
        _kneeLeft = SmoothKneeLeft.Filter(km.GetJointPosition(km.GetPlayer1ID(), (int)SkelitenJoints.KneeLeft));

        //Debug.Log("_wristLeft.z" + _wristLeft.z + "_wristRight" + _wristRight.z + "_shoulderRight" + _shoulderRight.z + " _shoulderLeft" + _shoulderLeft.z);
        if (Fly())
            isFlyActive = true;
        else
            isFlyActive = false;

        if (Slice())
            isSliceActive = true;
        else
            isSliceActive = false;

        if (Attack())
            isAttackActive = true;
        else
            isAttackActive = false;

        if (Hook())
            isHookActive = true;
        else
            isHookActive = false;

        if (Throw())
            isThrowActive = true;
        else
            isThrowActive = false;

        if (Duck())
            isDuckActive = true;
        else
            isDuckActive = false;

        if (Jump())
            isJumpActive = true;
        else
            isJumpActive = false;

    }
    //Player can take run values from here
    public Vector3 Run(){

        //neck z axis  > 3
        Vector3 moveVector;
        moveVector = Vector3.zero;

        //Debug.Log("_neck.z: " + _neck.z + " " + "_neck.x: " + _neck.x + " " + "_neck.y: " + _neck.y + " " + "_spineBase.z" + " " + _spineBase.z + "_spineBase.x" + " " + _spineBase.x);
        if ((km.GetPlayer1ID() > 0) && _spineBase.z - _neck.z > 0.2f)
            moveVector.z = 1f;

        if ((km.GetPlayer1ID() > 0) && _neck.z - _spineBase.z > 0.2f)
            moveVector.z = -1f;

        if ((km.GetPlayer1ID() > 0) && _neck.x - _spineBase.x > 0.1f)
            moveVector.x = 1f;

        if ((km.GetPlayer1ID() > 0) && _spineBase.x - _neck.x > 0.1f)
            moveVector.x = -1f;

        return moveVector;
    }

    bool Fly()
    {
        // right hand wrist - neck distance > 0.4
        if ((km.GetPlayer1ID() > 0) && _wristRight.y - _neck.y > 0.4f)
        {
            if (!flySet)
            {
                StartCoroutine(UnsetAfter(2.0f));
                flySet = true;
                return true;
            }
        }
        return false;
    }

    IEnumerator UnsetAfter(float time)
    {
        yield return new WaitForSeconds(time);
        flySet = false;
        throwSet = false;
        jumpSet = false;
        duckSet = false;
        hookSet = false;
    }

    bool Slice()
    {
        //right hand wrist touches left hand elbow
        if ((km.GetPlayer1ID() > 0) && _wristRight.x < _elbowLeft.x)
            return true;
        return false;
    }

    bool Attack()
    {
        //left hand wrist touches right hand elbow
        if ((km.GetPlayer1ID() > 0) && _wristLeft.x > _elbowRight.x)
            return true;
        return false;
    }

    bool Hook()
    {
        //right hand wrist touches right hand shoulder
        if ((km.GetPlayer1ID() > 0) && _wristLeft.y - _neck.y > 0.4f)
        {
            if (!hookSet)
            {
                StartCoroutine(UnsetAfter(2.0f));
                hookSet = true;
                return true;
            }
        }
        return false;
    }
    
    bool Throw()
    {
        //left wrist and left shoulder distance >
        if (km.IsPlayerCalibrated(km.GetPlayer1ID()) && _shoulderLeft.z - _wristLeft.z > 0.5f && _wristLeft.z >0f)
        {
            if (!throwSet)
            {
                StartCoroutine(UnsetAfter(3.0f));
                throwSet = true;
                return true;
            }
        }
        return false;
    }

    bool Duck()
    {
        // neck and ankle distance < 5
        if ((km.GetPlayer1ID() > 0)&& _kneeLeft.y > 0f && _neck.y - _kneeLeft.y < 0.7f && _spineBase.z - _neck.z < 0.2f && _spineBase.z - _neck.z > 0.1f)
            if (!duckSet)
            {
                StartCoroutine(UnsetAfter(3.0f));
                duckSet = true;
                return true;
            }
        return false;
    }

    bool Jump()
    {
        // neck y axis >
        
        if ((km.GetPlayer1ID() > 0) && _wristRight.z > 0f && _neck.z - _wristRight.z < 0.2f && _neck.y - _wristRight.y < 0.2f && _neck.z - _wristLeft.z < 0.2f && _neck.y - _wristLeft.y < 0.2f)
        {
            if (!jumpSet)
            {
                StartCoroutine(UnsetAfter(3.0f));
                jumpSet = true;
                return true;
            }
        }
        return false;
    }
}
