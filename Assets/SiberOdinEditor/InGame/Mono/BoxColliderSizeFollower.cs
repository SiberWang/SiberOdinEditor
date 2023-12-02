using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SiberOdinEditor.Mono
{
    public class BoxColliderSizeFollower : MonoBehaviour
    {
        [SerializeField]
        private List<BoxCollider2D> colliders;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        [LabelText("[For Auto] 比例縮放: ")]
        private float scale = 1f;

        [SerializeField]
        [LabelText("[For Auto] 尺寸誤差: ")]
        private Vector2 fixOffset;

        [SerializeField]
        private Vector2 changeBoundSize;

        [SerializeField]
        private Vector2 changeCenter;

        /// <summary> 依照 Sprite 的 Rect 去自動設定 BoxCollider2D 的Size </summary>
        [Button("跟隨 Sprite's Bounds 參數")]
        public void AutoFollowSize()
        {
            changeBoundSize = (Vector2)(spriteRenderer.sprite.bounds.size * scale) + fixOffset;
            changeCenter    = new Vector2(0, changeBoundSize.y / 2);
            OverrideColliderSize(changeBoundSize, changeCenter);
        }

        /// <summary> 依照指定的參數，去設定 BoxCollider2D 的 Size </summary>
        [Button("覆蓋 BoxColliders 參數")]
        [GUIColor(0.6f, 1.2f, 0.6f)]
        public void OverrideColliderSize()
        {
            OverrideColliderSize(changeBoundSize, changeCenter);
        }

        /// <summary> 依照指定的參數，去設定 BoxCollider2D 的 Size </summary>
        /// <param name="size"> 大小 (Size) </param>
        /// <param name="offset"> 位置 (同等Center) </param>
        public void OverrideColliderSize(Vector2 size, Vector2 offset)
        {
            for (var i = 0; i < colliders.Count; i++)
            {
                colliders[i].size   = size;
                colliders[i].offset = offset;
            }
        }

        public void DoInit()
        {
            OverrideColliderSize();
            AutoFollowSize();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(changeCenter, changeBoundSize);
        }
    }
}