using System;
using UnityEngine;

[Serializable]
public class FlashHurtBehaviour : BaseEnemyHurt
{
    private Color originColor;
    private float flashTimer;

    protected override void OnInit()
    {
        originColor = view.GetSpriteRenderColor();
    }

    public override void OnDamaged(int damage)
    {
        view.SetSpriteRenderColor(Color.red);
        flashTimer = model.flashTime;
    }

    protected override void TickHurt()
    {
        if (flashTimer <= 0f) return;
        flashTimer -= Time.deltaTime;
        if (flashTimer <= 0f)
        {
            view.SetSpriteRenderColor(originColor);
        }
    }
}