using UnityEngine;

public abstract class EnemyBehaviourBase
{
    protected EnemyView view;
    protected EnemyModel model;
    protected EnemyController controller;
    protected Transform transform;

    public void Init(EnemyController con)
    {
        controller = con;
        model = con.model;
        view = con.view;
        transform = con.transform;
        OnInit();
    }

    protected virtual void OnInit() { }
}