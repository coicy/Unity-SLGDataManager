using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Rendering.FilterWindow;


public class InventoryEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    [SerializeField]
    private VisualTreeAsset TransformationTableTemplate;
    [SerializeField]
    private VisualTreeAsset SourceReviewTemplate;

    [SerializeField]
    private Sprite White;

    private ListView SourceListView;
    private ListView TransformationTableList;
    VisualElement root;
    

    [SerializeField]
    private SO_SourceList SourceInventory;
    [MenuItem("MyTools/InventoryEditor")]
    public static void ShowExample()
    {
        InventoryEditor wnd = GetWindow<InventoryEditor>();
        wnd.titleContent = new GUIContent("InventoryEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy


        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        Init();

    }

    public void Init()
    {
        SourceListView = root.Q<VisualElement>("MainBlock").Q<VisualElement>("ListBlock").Q<ListView>("SourceListView");
        TransformationTableList = root.Q<VisualElement>("MainBlock").Q<VisualElement>("ShowBlock").Q<VisualElement>("IntroductionEditor").Q<VisualElement>("InvisualArea").Q<VisualElement>("TransformationTable").Q<ListView>("TransformationList");

        CreateList();
    }

    public void CreateList()
    {
        CreateSourceList();

        CreateTransformationList(0);
    }

    public void CreateSourceList()
    {
        Func<VisualElement> makeItem = () => SourceReviewTemplate.CloneTree();

        Action<VisualElement, int> bindItem = (element, id) =>
        {
            if(id < SourceInventory.Sources.Count)
            {
                
                    if (SourceInventory.Sources[id].m_SourceSprite != null)
                        element.Q<VisualElement>("MainBlock").Q<VisualElement>("SpriteArea").style.backgroundImage = SourceInventory.Sources[id].m_SourceSprite.texture;
                    else
                        element.Q<VisualElement>("MainBlock").Q<VisualElement>("SpriteArea").style.backgroundImage = SourceInventory.Sources[id].m_SourceSprite.texture;

                var TextArea = element.Q<VisualElement>("MainBlock").Q<VisualElement>("TextArea");
                TextArea.Q<IntegerField>("IDField").value = SourceInventory.Sources[id] == null ? -1 : SourceInventory.Sources[id].m_index;
                TextArea.Q<TextField>("NameField").value = SourceInventory.Sources[id].m_name == " " ? "未命名" : SourceInventory.Sources[id].m_name;



            }
        };
        SourceListView.itemsSource = SourceInventory.Sources;
        SourceListView.makeItem = makeItem;
        SourceListView.bindItem = bindItem;


    }

    //刷新显示选中的Source下的Transformation
    public void CreateTransformationList(int ID)
    {
        if (SourceInventory.transformations[ID] == null)
        {
            return;
        }

        TransformationTableList.Clear();
        Func<VisualElement> makeItem = () => TransformationTableTemplate.CloneTree();

        Action<VisualElement, int> bindItem = (element, id) =>
        {
            CreateTransformationTable(ref TransformationTableList, SourceInventory.transformations[ID].Count, SourceInventory.transformations[ID], ID);

        };

        TransformationTableList.itemsSource = SourceInventory.transformations[ID];
        TransformationTableList.makeItem = makeItem;
        TransformationTableList.bindItem = bindItem;

    }

    //TODO:刷新显示每一个Transformation中的Source表
    public void CreateTransformationTable(ref ListView TransformationTable, int count, List<SO_Transformation> SO_Transformation,int ID)
    {
        Func<VisualElement> makeItem = () => SourceReviewTemplate.CloneTree();

        Action<VisualElement, int> bindItem = (element, id) =>
        {
            if (id < count)
            {
                if (SO_Transformation[ID].sources[id].m_SourceSprite != null)
                    element.Q<VisualElement>("MainBlock").Q<VisualElement>("SpriteArea").style.backgroundImage = SourceInventory.Sources[id].m_SourceSprite.texture;
                else
                    element.Q<VisualElement>("MainBlock").Q<VisualElement>("SpriteArea").style.backgroundImage = SourceInventory.Sources[id].m_SourceSprite.texture;

                var TextArea = element.Q<VisualElement>("MainBlock").Q<VisualElement>("TextArea");
                TextArea.Q<IntegerField>("IDField").value = SO_Transformation[ID].sources[id] == null ? -1 : SourceInventory.Sources[id].m_index;
                TextArea.Q<TextField>("NameField").value = SO_Transformation[ID].sources[id].m_name == " " ? "未命名" : SourceInventory.Sources[id].m_name;

            }
        };

        TransformationTable.itemsSource = SO_Transformation;
        TransformationTable.makeItem = makeItem;
        TransformationTable.bindItem = bindItem;
    }
}
