using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace VerumEditor
{
    public class VerumGraphView : GraphView
    {
        public VerumGraphView(VerumWindow window) : base()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("VerumWindow/VerumGraphView"));
            GridBackground gridBackground = new GridBackground();
            Insert(0, gridBackground);

            gridBackground.StretchToParentSize();
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ContentDragger());

            var searchWindowProvider = ScriptableObject.CreateInstance<SearchWindowProvider>();
            searchWindowProvider.Initialize(this, window);
            nodeCreationRequest += context =>
            {
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
            };
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

            foreach (var port in ports.ToList())
            {
                if (startPort.node == port.node) continue;
                // Input - Input, Output - Output
                if (startPort.direction == port.direction) continue;
                if (startPort.portType != port.portType) continue;

                compatiblePorts.Add(port);
            }

            return compatiblePorts;
        }
    }
}
