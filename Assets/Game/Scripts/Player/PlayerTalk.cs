using UnityEngine;
using Game.Input;
using UniRx;
using Game.Intaract;

namespace Game.Player
{
    public class PlayerTalk : MonoBehaviour
    {
        [SerializeField] private InputReader input;
        private RaycastHit hit;
        bool hitCheck;

        [SerializeField] private float range;
        public float Range => range;
        [SerializeField] private float radius;
        public float Radius => radius;

        private void Start()
        {
            input.OnTalk.Subscribe(_ => Talk());
        }

        private void Talk()
        {
            if(Physics.SphereCast(transform.position, radius, transform.forward, out hit, range))
            {
                if(hit.transform.TryGetComponent(out Investigatable investigatable))
                {
                    
                }
            }
        }
    }
}