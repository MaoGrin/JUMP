using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private BoxCollider2D box2d;
    private SpriteRenderer spRender;
    // Awake：只做「自己组件」的初始化（GetComponent 缓存引用）
    void Awake()
    {
        box2d = GetComponent<BoxCollider2D>();
        spRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpriteRenderColor(Color color)
    {
        spRender.color = color;
    }

    public Color GetSpriteRenderColor()
    {
        return spRender.color;
    }
}
