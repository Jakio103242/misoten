using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace VerumEditor 
{
    public class GraphSaveUtility
    {
        private VerumGraphView targetGraphView;

        private List<Edge> Edges => targetGraphView.edges.ToList();
        private List<DialogueNode> Nodes => targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();

        public static GraphSaveUtility GetInstance(VerumGraphView targetGraphView)
        {
            return new GraphSaveUtility
            {
                targetGraphView = targetGraphView
            };
        }

        public void SaveGraph(string fileName)
        {
            if(!Edges.Any()) return;

            var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

            var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();

            for(var i = 0; i < connectedPorts.Length; i++)
            {
                var outputNode = connectedPorts[i].output.node as DialogueNode;
                var inputNode = connectedPorts[i].input.node as DialogueNode;
                
                dialogueContainer.NodeLinks.Add(new NodeLinkData
                {
                    BaseNodeGuid = outputNode.Guid,
                    PortName = connectedPorts[i].output.portName,
                    TargetNodeGuid = inputNode.Guid
                });
            }
            
            foreach (var dialogueNode in Nodes)
            {
                dialogueContainer.DialogueNodeData.Add(new DialogueNodeData
                {
                    Guid = dialogueNode.Guid,
                    //DialogueText = dialogueNode.DialogueText,
                    Position = dialogueNode.GetPosition().position
                });
            }

            AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Editor/Resouces/{fileName}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}