using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    public Transform point1;
    public Transform point2;

    enum State {
        MoveToPoint1,
        StopOnPoint1,
        MoveToPoint2,
        StopOnPoint2,
    }

    State currentState = State.MoveToPoint1;
    bool stateEnter = false;
    float stateTime = 0f;

    void ChangeState(State newState) {
        currentState = newState;
        stateEnter = true;
        stateTime = 0f;
        Debug.Log(currentState.ToString());
    }

    NavMeshAgent navMeshAgent;
    Animator animator;

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        stateTime += Time.deltaTime;

        float speed = navMeshAgent.velocity.magnitude;

        animator.SetFloat("PlayerSpeed", speed);


        switch (currentState) {

            case State.MoveToPoint1: {
                    if (stateEnter) {
                        navMeshAgent.SetDestination(point1.position);
                    }

                    if(navMeshAgent.remainingDistance <= 0.01f && !navMeshAgent.pathPending) {
                        ChangeState(State.StopOnPoint1);
                        return;
                    }

                    return;
            }

            case State.StopOnPoint1: {
                    if (stateEnter) {

                    }

                    if(stateTime >= 3f) {
                        ChangeState(State.MoveToPoint2);
                        return;
                    }

                    return;
                }

            case State.MoveToPoint2: {
                    if (stateEnter) {
                        navMeshAgent.SetDestination(point2.position);
                    }

                    if (navMeshAgent.remainingDistance <= 0.01f && !navMeshAgent.pathPending) {
                        ChangeState(State.StopOnPoint2);
                        return;
                    }

                    return;
                }

            case State.StopOnPoint2: {
                    if (stateEnter) {

                    }

                    if(stateTime >= 3f) {
                        ChangeState(State.MoveToPoint1);
                        return;
                    }


                    return;
                }

        }
    }

    private void LateUpdate() {
        
        if(stateTime != 0) {
            stateEnter = false;
        }

    }


}
