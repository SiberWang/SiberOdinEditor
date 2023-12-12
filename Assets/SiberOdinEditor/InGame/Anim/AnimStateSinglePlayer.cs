using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

namespace SiberOdinEditor.Mono.Anim
{
    public class AnimStateSinglePlayer : MonoBehaviour
    {
    #region ========== [Private Variables] ==========

        private const string Setting = "動畫設定";

        [BoxGroup(Setting)]
        [PropertyOrder(0)]
        [SerializeField] [Required]
        private Animator animator;

        [BoxGroup(Setting)]
        [PropertyOrder(3)]
        [SerializeField]
        private bool isStartSpawn = true;

        [BoxGroup(Setting)]
        [PropertyOrder(2)]
        [SerializeField]
        [ValueDropdown(nameof(States))]
        [InlineButton(nameof(UpdateAnimatorStates), SdfIconType.Recycle, "")]
        private string defaultStateName = "Idle";

        [BoxGroup(Setting)]
        [PropertyOrder(1)]
        [SerializeField]
        [ValueDropdown(nameof(States))]
        [InlineButton(nameof(UpdateAnimatorStates), SdfIconType.Recycle, "")]
        private string playStateName;

        private List<string> States
        {
            get
            {
                if (states == null)
                    UpdateAnimatorStates();
                return states;
            }
        }

        private bool         isKeepPlaying;
        private List<string> states;

    #endregion

    #region ========== [Unity Events] ==========

        private void Start()
        {
            Assert.IsNotNull(animator, "Your animator is null");
            if (!isStartSpawn) return;
            animator.Play(playStateName);
        }

        private void Update()
        {
            if (!isKeepPlaying) return;
            animator.Play(playStateName);
        }

    #endregion

    #region ========== [Events] ==========

        [PropertyOrder(10)]
        [GUIColor(0.3f, 1f, 0.3f)]
        [Button("播放動畫")] [ButtonGroup]
        private void OnPlayAnimation()
        {
            if (!Application.isPlaying) return;
            if (string.IsNullOrEmpty(playStateName))
            {
                Debug.Log("Animation Name is Empty");
                return;
            }

            isKeepPlaying    = true;
            animator.enabled = true;
        }

        [PropertyOrder(12)]
        [GUIColor(1f, 1f, 0.3f)]
        [Button("回預設動畫")] [ButtonGroup]
        private void OnPlayDefault()
        {
            if (!Application.isPlaying) return;
            isKeepPlaying    = false;
            animator.enabled = true;
            animator.Play(defaultStateName);
        }

        [PropertyOrder(11)]
        [GUIColor(1f, 0.3f, 0.3f)]
        [Button("停止動畫")] [ButtonGroup]
        private void OnStopAnimation()
        {
            if (!Application.isPlaying) return;
            isKeepPlaying    = false;
            animator.enabled = false;
        }

    #endregion

    #region ========== [Private Methods] ==========

        private void UpdateAnimatorStates()
        {
            states = new List<string>();
            if (animator == null) return;
            
        #if UNITY_EDITOR
            var controller = animator.runtimeAnimatorController as AnimatorController;
            if (controller == null) return;
            foreach (var layer in controller.layers)
                foreach (var childAnimatorState in layer.stateMachine.states)
                    states.Add(childAnimatorState.state.name);
        #endif
        }

    #endregion
    }
}