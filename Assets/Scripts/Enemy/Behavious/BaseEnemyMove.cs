using System;
using UnityEngine;

[Serializable]
public abstract class BaseEnemyMove : EnemyBehaviourBase, IEnemyMoveBehaviour
{
    public void Tick()
    {
        if (controller == null) return;
        Move();
    }

    protected abstract void Move(); // 强制子类实现，漏写会编译报错
}