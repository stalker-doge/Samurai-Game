using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateController : MonoBehaviour
{
    State currentState;

    public SleepState sleepState = new SleepState();
    public ChaseState chaseState = new ChaseState();
    public PatrolState patrolState = new PatrolState();
    public HurtState hurtState = new HurtState();
    public SpawnState spawnState = new SpawnState();

    [SerializeField] bool isSpawner = false;

    private void Start()
    {
        if(!isSpawner)
        {
            ChangeState(patrolState);
        }
        else
        {
            ChangeState(spawnState);
        }
    }

    void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnStateUpdate();
        }
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        currentState = newState;
        currentState.OnStateEnter(this);
    }
    public void Hurt()
    {
        currentState.OnStateHurt();
    }
}
public abstract class State
{
    protected StateController sc;
    public void OnStateEnter(StateController stateController)
    {
        // Code placed here will always run
        sc = stateController;
        Debug.Log("Current state is: "+this.ToString());
        OnEnter();
    }

    protected virtual void OnEnter()
    {
        // Code placed here can be overridden
    }

    public void OnStateUpdate()
    {
        // Code placed here will always run
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        // Code placed here can be overridden
    }

    public void OnStateHurt()
    {
        // Code placed here will always run
        Debug.Log("YEEEEEOUCH");
        OnHurt();
    }

    protected virtual void OnHurt()
    {
        // Code placed here can be overridden
    }

    public void OnStateExit()
    {
        // Code placed here will always run
        OnExit();
    }

    protected virtual void OnExit()
    {
        // Code placed here can be overridden
    }
}