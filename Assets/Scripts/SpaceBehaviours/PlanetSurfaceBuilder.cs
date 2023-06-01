using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSurfaceBuilder : MonoBehaviour
{
    private float radius;

    public void UpdateData(float radius, Sprite sprite)
    {
        SetRadius(radius);
        SetSprite(sprite);
    }

    private void SetRadius(float radius)
    {
        //GetComponent<CircleCollider2D>().radius = radius / gameObject.transform.localScale.x;
        this.radius = radius;
    }

    private void SetSprite(Sprite sprite)
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.transform.localScale = new Vector3(radius / 2.0f, radius / 2.0f, 1.0f);
    }

    /*private void Update()
    {
        centerOffset.x += 1.0f * Time.deltaTime;
        if (centerOffset.x > 6.5f)
            centerOffset.x = -6.5f;

        UpdateMaterial();
    }*/

    private void UpdateMaterial()
    {
        /*if (materialOverride == null)
            return;

        // UCG does not allow to provide Vector2. So we transform the vector2 from the inspector to a vector4 with useless values at z and y.
        Vector4 centerOffSetV4 = new Vector4(centerOffset.x, centerOffset.y, 0.0f, 0.0f);

        materialOverride.SetColor("_InnerColor", innerColor);
        materialOverride.SetColor("_OuterColor", outerColor);
        materialOverride.SetVector("_CenterOffset", centerOffSetV4);
        materialOverride.SetFloat("_ColoringDistanceFactor", coloringDistanceFactor);
        meshRenderer.SetPropertyBlock(materialOverride);*/
    }
}
