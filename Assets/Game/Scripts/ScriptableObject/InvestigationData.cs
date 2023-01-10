using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data {
    [CreateAssetMenu(fileName = "Dialogue", menuName = "GameSO/InvestigationData")]
    public class InvestigationData : ScriptableObject
    {
        [System.Serializable]
        public struct InvestigationInfo
        {
            public string name;
            public string explanation;
        }

        [SerializeField] private InvestigationInfo info;
        public InvestigationInfo Info => info;
    }
}
