using System;
using UnityEngine;

[Serializable]
public class EnemyPatrolMoveBehaviour : BaseEnemyMove
{
    [Header("巡逻范围")]
    public Vector2 pointA;
    public Vector2 pointB;
    public float speed = 2f;

    private Vector2 target;

    protected override void OnInit()
    {
        target = pointB; // 先朝 B 走
    }

    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(
            transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.01f)
        {
            target = target == pointA ? pointB : pointA; // 到头就折返
        }
    }
}

