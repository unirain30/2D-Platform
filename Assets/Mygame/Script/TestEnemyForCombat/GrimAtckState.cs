using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimAtckState : EnemyState
{
    GrimEnermy enemy;
    public GrimAtckState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,GrimEnermy enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetZeroVelocity();
        if(triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
    
}
