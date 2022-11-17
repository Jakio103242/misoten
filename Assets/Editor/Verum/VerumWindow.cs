using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace VerumEditor
{
    public class VerumWindow : EditorWindow
    {
        private VerumGraphView graphView;
        private string fileName = "New Narrative";

        [MenuItem("Window/Verum")]
        static void Open()
        {
            GetWindow<VerumWindow>("Verum");
        }
        void OnEnable()
        {
            graphView = new VerumGraphView(this)
            {
                style = { flexGrow = 1 }
            };

            var visualTree = Resources.Load<VisualTreeAsset>("VerumWindow/VerumWindow");
            visualTree.CloneTree(rootVisualElement);

            GenericToolBar();
            rootVisualElement.Add(graphView);
        }

        void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }

        void GenericToolBar()
        {
            // var toolbar = new Toolbar();

            // var fileNameTextField = new TextField(label: "File Name");
            // fileNameTextField.SetValueWithoutNotify(fileName);
            // fileNameTextField.MarkDirtyRepaint();
            // fileNameTextField.RegisterValueChangedCallback(evt => fileName = evt.newValue);
            // fileNameTextField.ElementAt(0).style.minWidth = 10;
            // toolbar.Add(fileNameTextField);

            // rootVisualElement.Add(toolbar);

            var textField = rootVisualElement.Q<TextField>("inputFileName");
            textField.SetValueWithoutNotify(fileName);
            textField.MarkDirtyRepaint();
            textField.RegisterValueChangedCallback(evt => fileName = evt.newValue);
            
            var saveButton = rootVisualElement.Q<Button>("saveButton");
            saveButton.clicked += () => Save();
            var loadButton = rootVisualElement.Q<Button>("loadButton");
            loadButton.clicked += () => Load();
        }

        void Save()
        {
            Debug.Log("Save");
        }

        void Load()
        {
            Debug.Log("Load");
        }
    }
}