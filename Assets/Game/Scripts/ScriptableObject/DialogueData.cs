using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data {
    [CreateAssetMenu(fileName = "DialogueData", menuName = "GameSO/DialogueData")]
    public class DialogueData : ScriptableObject
    {
        [System.Serializable]
        public struct DialogueInfo
        {
            public string name;
            public string quote;
        }

        [SerializeField] private List<DialogueInfo> dialogue;
        public List<DialogueInfo> Dialogue => dialogue;
    }
}
