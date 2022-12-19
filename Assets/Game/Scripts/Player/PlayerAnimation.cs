using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {

        }

        public void AnimationIdle()
        {
            animator.Play("Idle");
        }

        public void AnimationWalk()
        {
            animator.Play("Walk");
        }

        public void AnimationRun()
        {
            animator.Play("Run");
        }
    }
}
