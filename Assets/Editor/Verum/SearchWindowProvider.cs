using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace VerumEditor
{
    public class SearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private VerumWindow window;
        private VerumGraphView graphView;

        public void Initialize(VerumGraphView graphView, VerumWindow window)
        {
            this.window = window;
            this.graphView = graphView;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var entries = new List<SearchTreeEntry>();
            entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));
            string name;

            foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass == false) continue;

                    if (type.IsAbstract) continue;

                    if (type.IsSubclassOf(typeof(VerumNode)) == false) continue;

                    name = type.Name;
                    name = name.Remove(name.Length - 4);
                    entries.Add(new SearchTreeEntry(new GUIContent(name)) { level = 1, userData = type });
                }
            }

            return entries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            var type = SearchTreeEntry.userData as System.Type;
            var node = System.Activator.CreateInstance(type) as Node;

            // ノードの生成位置をマウスの座標にする
            var worldMousePosition = window.rootVisualElement.ChangeCoordinatesTo(window.rootVisualElement.parent, context.screenMousePosition - window.position.position);
            var localMousePosition = graphView.contentViewContainer.WorldToLocal(worldMousePosition);

            node.SetPosition(new Rect(localMousePosition, new Vector2(100, 100)));

            graphView.AddElement(node);
            return true;
        }
    }
}
