using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data {
    [CreateAssetMenu(fileName = "Dialogue", menuName = "GameSO/Dialogue")]
    public class DialogueData : ScriptableObject
    {
        [System.Serializable]
        public struct DialogueStatus
        {
            public string name;
            [TextArea(1, 3)] public string quote;
        }

        [SerializeField] private List<DialogueStatus> dialogue;
        public List<DialogueStatus> Dialogue => dialogue;
    }
}
