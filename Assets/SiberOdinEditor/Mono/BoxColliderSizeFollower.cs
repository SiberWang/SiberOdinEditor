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
        private float scale = 1f;

        private Vector2 changeBoundSize;
        private Vector2 changeCenter;

        [Button("跟隨 Sprite Size")]
        private void FollowSize()
        {
            var boundsSize = (Vector2)spriteRenderer.sprite.bounds.size * scale;
            var centerPos  = new Vector2(0, boundsSize.y / 2);
            for (var i = 0; i < colliders.Count; i++)
            {
                colliders[i].size   = boundsSize;
                colliders[i].offset = centerPos;

                if (i != 0) continue; // 記錄一次
                changeBoundSize = boundsSize;
                changeCenter    = centerPos;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(changeCenter, changeBoundSize);
        }
    }
}