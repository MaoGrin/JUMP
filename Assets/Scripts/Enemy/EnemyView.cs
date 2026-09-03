using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private BoxCollider2D box2d;
    private SpriteRenderer spRender;
    // Start is called before the first frame update
    void Start()
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
