using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace VerumEditor
{
    public class DialogueNode : VerumNode
    {
        //private UnityEditor.UIElements.TextField textField;
        public DialogueNode()
        {
            title = "Verum";
            Guid = System.Guid.NewGuid().ToString();

            var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
            inputContainer.Add(inputPort);

            var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
            outputContainer.Add(outputPort);

            var visualTree = Resources.Load<VisualTreeAsset>("VerumWindow/DialogueNode");
            visualTree.CloneTree(extensionContainer);
            extensionContainer.style.backgroundColor = new Color(0.2f, 0.2f, 0.2f);
            
            RefreshExpandedState();
            RefreshPorts();
        }
    }
}
