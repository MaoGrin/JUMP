using UnityEngine;

public abstract class EnemyBehaviourBase
{
    public EnemyView view;
    public EnemyModel model;
    public EnemyController controller;
    public Transform transform;

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