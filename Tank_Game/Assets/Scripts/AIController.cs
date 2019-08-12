using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Tank_Data pawn;

    public Tank_Data target;

    public Enemy_Shoot enemyShoot;

    public List<Transform> wayPoints;

    public Timer timer;
    //

    public int currentWayPoint;
    public float cutoff;
    public bool isForward;
    public float statesStartTime;
    public float startAvoidTime;
    public float startAttackTime;
    public float feelerDistance;
    public SphereCollider hearingRadar;
    public float hearingDistance;

    public bool isDead = false;

    public enum LoopType { Loop, Stop, PingPong, Random };
    public enum AiStates { Idle, Patrol, Chase, Flee, Dead };
    public enum AIAvoidState { Null, TurnToAvoid, MoveToAvoid };
    public enum AiAttackState { Null, Attack };

    public LoopType looptype;
    public AiStates currentState;
    public AIAvoidState currentAvoidState = AIAvoidState.Null;
    public AiAttackState currentAttackState = AiAttackState.Null;
    public float avoidMoveTime;

    private void Start()
    {
        hearingRadar = GetComponentInChildren<SphereCollider>();
        currentState = AiStates.Idle;
    }


    // Update is called once per frame
    void Update()
    {//If the enemy health hits zero the enemy will be destroyed. 
        if (!isDead)
        {
            AIMain();
        }
        if (pawn.health <= 0)
        {
            ChangeState(AiStates.Dead);
        }
    }//This script is for the enemy bullet. It allows for it to hit and take health away. 

    protected void AIMain()
    {
        //Handle S-States
        switch (currentState)
        {
            case AiStates.Idle:
                Idle();
                break;
            case AiStates.Patrol:
                Patrol(wayPoints[currentWayPoint]);
                break;
            case AiStates.Chase:
                Chase(target.transform);
                break;
            case AiStates.Flee:
                Flee(target.transform);
                break;
            case AiStates.Dead:
                Dead();
                break;
            default:

                break;

        }
        switch (currentAvoidState)
        {
            case AIAvoidState.Null:
                //do Nothing
                break;
            case AIAvoidState.MoveToAvoid:
                //MoveToAvoid();
                break;
            default:
                break;
        }

        switch (currentAttackState)
        {
            case AiAttackState.Null:
                //Do Nothing
                break;
            case AiAttackState.Attack:
                Attack(target.transform);
                break;
            default:
                break;
        }

        //Hearing radar is set as a trigger
        if (!hearingRadar.isTrigger)
        {
            Debug.LogWarning("The SphereCollider used for the Hearing Radar must be set to Trigger.");
        }

    }

    public void ChangeState(AiStates newState)
    {
        timer.StartTimer(0);
        statesStartTime = timer.currentTime[0];
        currentState = newState;

        currentAvoidState = AIAvoidState.Null;
    }

    public void ChangeAvoidState(AIAvoidState newState)
    {
        timer.StartTimer(1);
        startAvoidTime = timer.currentTime[1];
        currentAvoidState = newState;
    }

    public void ChangeAttackState(AiAttackState newState)
    {
        timer.StartTimer(3);
        startAttackTime = timer.currentTime[3];
        currentAttackState = newState;
    }

    public virtual void Idle()
    {
        //Do Nothing 
    }

    public void Seek(Transform target)
    {
        switch (currentAvoidState)
        {
            case AIAvoidState.Null:
                //Chase
                Vector3 targetVector = (target.position - pawn.bodytf.position).normalized;
                pawn.mover.RotateTowards(targetVector);
                pawn.mover.Move(Vector3.forward * pawn.moveSpeed * Time.deltaTime);
                //turn to avoid
                if (IsBlocked())
                {
                    ChangeAvoidState(AIAvoidState.TurnToAvoid);
                }
                break;
            case AIAvoidState.TurnToAvoid:
                pawn.mover.Rotate(pawn.rotateSpeed * Time.deltaTime);
                //If you are not block, move to avoid
                if (!IsBlocked())
                {
                    ChangeAvoidState(AIAvoidState.MoveToAvoid);
                }
                break;
            case AIAvoidState.MoveToAvoid:
                //Move foward
                pawn.mover.Move(Vector3.forward * Time.deltaTime);
                //If time is up, there's nothing in your way
                if (timer.currentTime[1] > startAvoidTime + avoidMoveTime)
                {
                    timer.ResetTime(1, false);
                    ChangeAvoidState(AIAvoidState.Null);
                }
                break;
            default:
                break;

        }
    }

    public virtual void Patrol(Transform target)
    {
        //Start Patrolling 
        Seek(target);
        if (Vector3.Distance(pawn.bodytf.position, wayPoints[currentWayPoint].position) <= cutoff)
        {
            if (isForward)
            {
                currentWayPoint++;
            }
            else
            {

            }

            if (currentWayPoint >= wayPoints.Count || currentWayPoint < 0)
            {
                if (looptype == LoopType.Loop)
                {
                    currentWayPoint = 0;
                }

                else if (looptype == LoopType.Random)
                {
                    currentWayPoint = Random.Range(0, wayPoints.Count);
                }
                else if (looptype == LoopType.PingPong)
                {
                    isForward = !isForward;
                    if (currentWayPoint >= wayPoints.Count)
                    {
                        currentWayPoint = wayPoints.Count - 1;
                    }
                    else
                    {
                        currentWayPoint = 0;
                    }
                }
            }
        }
    }
    public virtual bool IsBlocked()
    {
        if (Physics.Raycast(pawn.transform.position, transform.forward, out RaycastHit hit, feelerDistance))
        {
            if (hit.collider.tag == "Obstacle")
            {
                return true;
            }
        }
        return false;
    }
    public virtual void Flee(Transform target)
    {
        Vector3 targetVector = (pawn.transform.position - target.position);//Doing in revurse will cause a negative vector
        Vector3 awayVector = targetVector;
        pawn.mover.RotateTowards(awayVector);
        pawn.mover.Move(Vector3.forward * pawn.moveSpeed * Time.deltaTime);
    }
    //This stuff below will be put back in later with other things but the game cannot run right with it at the moment. 

    //public virtual bool MoveToAvoid()
    //{
       
    //    //avoiding objects
    //    //if (hit.collider.tag == "obstacle")
    //    //{
    //    //    pawn.mover.Rotate(pawn.rotateSpeed * Time.deltaTime);
    //    //    return true;
    //    //}
    //    //else
    //    //{
    //    //    ChangeState(AiStates.Patrol);
    //    //    ChangeAvoidState(AIAvoidState.Null);
    //    //    return false;
    //    //}
    //}
    public virtual void Chase(Transform target)
    {
        //chase function
        Vector3 targetVector = (target.position - pawn.bodytf.position).normalized;
        pawn.mover.RotateTowards(targetVector);
        pawn.mover.Move(Vector3.forward * pawn.moveSpeed * Time.deltaTime);
    }
    public virtual void Attack(Transform target)
    {
        //enemyShoot.InitiateEnemyControls(pawn.shotsPerSecond);//enemy routine
    }
    public virtual void Dead()
    {
        isDead = true;
    }
    public virtual bool CanSee()
    {
        //Produce raycast forward
        Debug.DrawRay(pawn.bodytf.position, transform.forward * feelerDistance, Color.red);

        if (Physics.Raycast(pawn.transform.position, pawn.transform.forward, out RaycastHit hit, feelerDistance))
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    public virtual bool CanHear()
    {
        switch (hearingRadar.isTrigger)
        {
            case false:
                Debug.LogWarning("The collider that is used as the hearing distance must be a trigger. ");
                break;
            case true:
                //if (GetComponentInChildren<RadarDetection>().playerDetected)
                //{
                //    Debug.Log("I hear you.");
                //    return true;
                //}
                break;
        }

        //can hear will use circle collider as a trigger.
        return false;
    }

}
