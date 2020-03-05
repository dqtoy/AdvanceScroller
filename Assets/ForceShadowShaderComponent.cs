using UnityEngine;

public class ForceShadowShaderComponent: MaterialChangeComponent
{
    private Color materialTintColor = Color.white;
    private readonly float _shadowLastingTime;
    private float timeCounter;
    private static readonly int Tint = Shader.PropertyToID("_Tint");

    public ForceShadowShaderComponent(SpriteRenderer renderer, Material material, float shadowLastingTime) : base(renderer, material)
    {
        _shadowLastingTime = shadowLastingTime;
        timeCounter = _shadowLastingTime;
        SetMaterialColorToWhite();
    }

    private void SetMaterialColorToWhite()
    {
        materialTintColor.a = 1;
        SpriteRenderer.material.SetColor(Tint, materialTintColor);
    }

    public override void ResetMaterial(Material material)
    {        
        timeCounter = _shadowLastingTime;
        SpriteRenderer.material = material;
        SetMaterialColorToWhite();
    }

    public override void Tick()
    {
        if (timeCounter >= 0)
        {        
            timeCounter-=Time.deltaTime;
            if (timeCounter <= 0)
            {
                Object.Destroy(SpriteRenderer.gameObject);
            }
        }
       
    }
}