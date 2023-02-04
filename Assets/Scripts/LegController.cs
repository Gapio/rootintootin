using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{

    public GameObject player;
    public Transform raycastpoint;
    public Transform target;
    public Transform stepPoint;
    public Vector3 stepNormal;
    public Vector3 restingPos;
    public Vector3 newPos;
    public LayerMask mask;
    public bool legGrounded;
    public bool hasMoved;
    public bool moving;
    public bool movingDown;
    public float offset;
    public float moveDist;
    public float speed = 10f;
    public static int currentMoveValue = 1;
    public int moveValue;
    public float moveDirection
    {
        get
        {
            return SpiderController.instance.movy;
        }
    }
    public bool walkingBackwards = false;

    public LegController otherLeg;



    // Start is called before the first frame update
    void Start()
    {
        restingPos = target.position;
        stepPoint.position = new Vector3(restingPos.x + offset, restingPos.y, restingPos.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (moveDirection < 0 & walkingBackwards != true)
        {
            walkingBackwards = true;
            offset = -0.5f;
            stepPoint.localPosition = new Vector3(stepPoint.localPosition.x + offset, stepPoint.localPosition.y, stepPoint.localPosition.z);
        }
        else if (moveDirection > 0 && walkingBackwards != false)
        {
            walkingBackwards = false;
            offset = 0.5f;
            stepPoint.localPosition = new Vector3(stepPoint.localPosition.x + offset, stepPoint.localPosition.y, stepPoint.localPosition.z);
        }

        newPos = calcPoint(stepPoint.position);

        if (Vector3.Distance(restingPos, newPos) > moveDist || moving && legGrounded)
        {
            step(newPos);
        }
        updateIk();
    }

    public Vector3 calcPoint(Vector3 position)
    {
        Vector3 dir = position - raycastpoint.position;
        RaycastHit hit;

        if (Physics.SphereCast(raycastpoint.position, 1f, dir, out hit, 5f, mask))
        {
            stepNormal = hit.normal;
            position = hit.point;
            legGrounded = true;
        }
        else
        {
            stepNormal = Vector3.zero;
            position = restingPos;
            legGrounded = false;
        }
        return position;
    }

    public void step(Vector3 position)
    {
        if(currentMoveValue == moveValue)
        {
            legGrounded = false;
            hasMoved = false;
            moving = true;

            target.position = Vector3.MoveTowards(target.position, position + Vector3.up, speed * Time.deltaTime);
            restingPos = Vector3.MoveTowards(target.position, position + Vector3.up, speed * Time.deltaTime);

            if(target.position == position + Vector3.up)
            {
                movingDown = true;
            }

            if (movingDown)
            {
                target.position = Vector3.MoveTowards(target.position, position, speed * Time.deltaTime);
                restingPos = Vector3.MoveTowards(target.position, position, speed * Time.deltaTime);
            }

            if(target.position == position)
            {
                legGrounded = true;
                hasMoved = true;
                moving = false;
                movingDown = false;

                if(currentMoveValue == moveValue && otherLeg.hasMoved == true) 
                {
                    currentMoveValue = currentMoveValue * -1 + 3;
                }
            }
        }
    }

    public void updateIk()
    {
        target.position = restingPos;
    }
}
