using System;
using TMPro;
using UnityEngine;

namespace SiberOdinEditor.Mono.Anim
{
    public class AnimCheckSetter : MonoBehaviour
    {
        public  Animator anim;
        public  TMP_Text textNumber;
        private string   defaultStateName;

        public void SetInfo(int index, RuntimeAnimatorController animatorController, string defaultStateName = "Idle")
        {
            gameObject.name                = $"AnimCheckSetter [{index}]";
            anim.runtimeAnimatorController = animatorController;
            textNumber.text                = index.ToString();
            this.defaultStateName          = defaultStateName;
        }

        public void StopAnim()
        {
            if (anim.enabled)
                anim.enabled = false;
        }

        public void PlayAnim(string animationName , bool isWaitForDefault = true)
        {
            if (!anim.enabled)
                anim.enabled = true;

            if (isWaitForDefault)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName(defaultStateName))
                    anim.Play(animationName);
            }
            else
            {
                anim.Play(animationName);
            }
            
        }
    }
}