using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sprite変更時にAspectRatioFitterのaspectRatioの値を再計算し変更するImageクラス
/// </summary>
[RequireComponent(typeof(AspectRatioFitter))]
public class ImageForAspectRatioFitter : Image
{
#if UNITY_EDITOR
    private Sprite currentSprite = null;
#endif
    private AspectRatioFitter aspectRatioFitter = null;

    public new Sprite sprite
    {
        get { return base.sprite; }
        set
        {
            base.sprite = value;
            UpdateSprite();
        }
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        if (currentSprite != sprite)
        {
            // inspectorで変更時に警告が出続けるのでその対処
            UnityEditor.EditorApplication.delayCall += () =>
            {
                UpdateSprite();
            };
        }
        base.OnValidate();
    }
#endif

    private void UpdateSprite()
    {
        if (aspectRatioFitter == null)
        {
            aspectRatioFitter = GetComponent<AspectRatioFitter>();
        }

        if (sprite != null)
        {
            aspectRatioFitter.aspectRatio = sprite.bounds.size.x / sprite.bounds.size.y;
        }

#if UNITY_EDITOR
        currentSprite = sprite;
#endif
    }
}
