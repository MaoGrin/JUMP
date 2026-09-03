using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public EnemyModel model;
    public EnemyView view;
    [SerializeReference] public BaseEnemyMove moveBehaviour;
    [SerializeReference] public BaseEnemyHurt hurtBehaviour;

    void Start()
    {
        view = GetComponent<EnemyView>();
        moveBehaviour?.Init(this);
        hurtBehaviour?.Init(this);
    }

    // Update is called once per frame
    void Update()
    {
        moveBehaviour?.Tick();
        hurtBehaviour?.Tick();

        if (model.hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        model.hp -= damage;
        if (model.hp <= 0)
        {
            model.hp = 0;
        }
        hurtBehaviour?.OnDamaged(damage);

    }
   
}
