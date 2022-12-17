using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Game.Input;
using Game.Story;

namespace Game.Intaract
{
    public class ObjectManager : MonoBehaviour
    {
        [SerializeField] InputReader input;
        [SerializeField] DialogueManager dialogueManager;
        [SerializeField] List<Intaractable> intaractables;
        public List<Intaractable> Intaractables => intaractables;
        [SerializeField] float displayDistance;
        [SerializeField] float investigateDistance;
        [SerializeField] Transform playerTransform;
        int nearestIndex = -1;

        public Intaractable NearestIntaractableObject()
        {
            if(nearestIndex < 0 || !intaractables[nearestIndex].StoryIncident().Active) return null;
            float distance;
            distance = (playerTransform.position - intaractables[nearestIndex].transform.position).sqrMagnitude;
            if(distance * distance > investigateDistance * investigateDistance) return null;
            return intaractables[nearestIndex];
        }

        void Start()
        {
            OnCheckVisibleCamera(this.GetCancellationTokenOnDestroy()).Forget();
        }

        void Update()
        {
        }

        async UniTask OnCheckVisibleCamera(CancellationToken token)
        {
            while(true) {
                await UniTask.Delay(System.TimeSpan.FromSeconds(0.5f), cancellationToken: token);
                CheckActibleHint();
            }
        }

        public void CheckActibleHint()
        {
            float distance;
            float nearestDistance = 0;
            nearestIndex = -1;
            
            for(int index = 0; index < intaractables.Count; index++)
            {
                intaractables[index].HintActive = false;

                if(intaractables[index].Completed) continue;
                if(!intaractables[index].StoryIncident().Active) continue;

                distance = (playerTransform.position - intaractables[index].transform.position).sqrMagnitude;
                if(distance * distance > displayDistance * displayDistance) continue;

                if(intaractables[index].Renderer.isVisible)
                {
                    intaractables[index].HintActive = true;
                    if(nearestIndex == -1) {
                        nearestIndex = index;
                        nearestDistance = distance;
                    }
                    if(nearestDistance < distance) continue;
                    nearestIndex = index;
                    nearestDistance = distance;
                }
            }
        }
    }
}
