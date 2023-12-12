using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

namespace SiberOdinEditor.Mono.Anim
{
    public class AnimCheckController : MonoBehaviour
    {
    #region ========== [Private Variables] ==========

        private const string GroupName = "SetterGroup [From AnimationCheck]";
        private const string Setting   = "動畫設定";
        private const string Spawn     = "生產設定";

        [BoxGroup(Setting)]
        [PropertyOrder(1)]
        [SerializeField]
        private AnimCheckSetter prefab;

        [BoxGroup(Setting)]
        [PropertyOrder(0)]
        [Required]
        [SerializeField]
        [OnValueChanged(nameof(UpdateAnimatorStates))]
        private RuntimeAnimatorController mainController;

        [BoxGroup(Setting)]
        [PropertyOrder(2)]
        [SerializeField]
        [ValueDropdown(nameof(States))]
        [InlineButton(nameof(UpdateAnimatorStates), SdfIconType.Recycle, "")]
        private string playStateName;

        [FormerlySerializedAs("defautlStateName")]
        [BoxGroup(Setting)]
        [PropertyOrder(3)]
        [SerializeField]
        [ValueDropdown(nameof(States))]
        [InlineButton(nameof(UpdateAnimatorStates), SdfIconType.Recycle, "")]
        private string defaultStateName = "Idle";


        [BoxGroup(Setting)]
        [PropertyOrder(4)]
        [SerializeField]
        private bool isStartSpawn = true;

        [BoxGroup(Setting)]
        [PropertyOrder(5)]
        [SerializeField]
        private List<RuntimeAnimatorController> animatorControllers;

        [BoxGroup(Spawn)]
        [SerializeField]
        private Vector2Int spawnPath = new Vector2Int(5, 5);

        [BoxGroup(Spawn)]
        [SerializeField]
        private Vector2 offset = new Vector2(1.5f, 1.5f);

        [BoxGroup(Spawn)]
        [SerializeField]
        private float offsetCenterY = 0f;


        private bool                  isKeepPlaying;
        private List<string>          states;
        private GameObject            group;
        private List<AnimCheckSetter> setterList = new List<AnimCheckSetter>();

        private List<string> States
        {
            get
            {
                if (states == null)
                    UpdateAnimatorStates();
                return states;
            }
        }

    #endregion

    #region ========== [Unity Events] ==========

        private void Start()
        {
            if (isStartSpawn)
                CreateSetters();
        }

        private void Update()
        {
            if (!isKeepPlaying) return;
            foreach (var setter in setterList)
                setter.PlayAnim(playStateName);
        }

    #endregion

    #region ========== [Events] ==========

        private void OnGUI()
        {
            var state = string.IsNullOrEmpty(playStateName) ? "Null" : playStateName;
            GUI.skin.label.fontSize = 32;
            GUI.Label(new Rect(20, 20, 500, 500), $"Play State : {state}");
        }

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

            isKeepPlaying = true;
        }

        [PropertyOrder(13)]
        [Button("全部重新生產")] [ButtonGroup]
        private void OnReset()
        {
            if (!Application.isPlaying) return;
            isKeepPlaying = false;
            CreateSetters();
        }

        [PropertyOrder(11)]
        [GUIColor(1f, 0.3f, 0.3f)]
        [Button("停止動畫")] [ButtonGroup]
        private void OnStopAnimation()
        {
            if (!Application.isPlaying) return;
            isKeepPlaying = false;
            foreach (var setter in setterList)
                setter.StopAnim();
        }

        [PropertyOrder(12)]
        [GUIColor(1f, 1f, 0.3f)]
        [Button("回預設動畫")] [ButtonGroup]
        private void OnPlayDefault()
        {
            if (!Application.isPlaying) return;
            isKeepPlaying = false;
            foreach (var setter in setterList)
                setter.PlayAnim(defaultStateName, false);
        }

        [PropertyOrder(14)]
        [Button("生產")] [ButtonGroup("2")]
        private void OnSpawn()
        {
            CreateSetters();
        }

        [PropertyOrder(15)]
        [Button("刪除")] [ButtonGroup("2")]
        private void DestroyGroup()
        {
            setterList.Clear();
            var gameObjects = FindObjectsOfType<GameObject>();
            foreach (var obj in gameObjects)
            {
                if (obj == null) continue;
                if (obj.name.Equals(GroupName))
                    DestroyImmediate(obj);
            }
        }

    #endregion

    #region ========== [Private Methods] ==========

        private void UpdateAnimatorStates()
        {
            states = new List<string>();
        #if UNITY_EDITOR
            var controller = mainController as AnimatorController;
            if (controller == null) return;
            foreach (var layer in controller.layers)
                foreach (var childAnimatorState in layer.stateMachine.states)
                    states.Add(childAnimatorState.state.name);
        #endif
        }

        private void CreateSetters()
        {
            DestroyGroup();
            group = new GameObject(GroupName);
            if (animatorControllers.Count <= 0)
            {
                Debug.LogError("animatorControllers.Count <= 0");
                return;
            }

            if (animatorControllers.Count > spawnPath.x * spawnPath.y)
            {
                var sqrt = Mathf.CeilToInt(Mathf.Sqrt(animatorControllers.Count));
                spawnPath = new Vector2Int(sqrt, sqrt);
                Debug.Log($"animatorControllers.Count > x * y , NewVector:{spawnPath}");
            }

            int index = 0;
            for (int x = 0; x < spawnPath.x; x++)
            {
                for (int y = 0; y < spawnPath.y; y++)
                {
                    if (index >= animatorControllers.Count) break;
                    var spawnPathX = (y - (spawnPath.y / 2)) * offset.x;
                    var spawnPathY = (-x + (spawnPath.x / 2)) * offset.y + offsetCenterY;
                    var pos        = new Vector3(spawnPathX, spawnPathY);

                    var animCheckSetter = Instantiate(prefab, pos, Quaternion.identity, group.transform);
                    animCheckSetter.SetInfo(index, animatorControllers[index], defaultStateName);

                    setterList.Add(animCheckSetter);
                    index++;
                }
            }
        }

    #endregion
    }
}