using System;
using UnityEngine;

[Serializable]
public abstract class BaseEnemyHurt : EnemyBehaviourBase, IHurtBehaviour
{
    public virtual void OnDamaged(int damage) { }

    public void Tick()
    {
        if (controller == null) return;
        TickHurt();
    }

    protected abstract void TickHurt();
}