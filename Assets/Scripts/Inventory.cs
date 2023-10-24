using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �������� ����Ʈ�� �����ϴ� �κ��丮�� ����ڽ��ϴ�.
/// �κ��丮 ���ο����� �����ͺ��̽��� ������ �޾� �� ������
/// onGUI�� ���� UI�� �������մϴ�.
/// </summary>

public class Inventory : MonoBehaviour
{
    //������ �κ��丮
    public List<Item> inventory = new List<Item>();
    //�κ��丮�� ���� ������ �����ͺ��̽�
    private ItemDB ItemDB;
    //�κ��丮 ���� ���� Ȯ��( �⺻ ���� : false)
    public bool isOpen = false;

    public int slot_X, slot_Y; //�κ��丮�� ���ο� ����
    public List<Item> slots = new List<Item>(); //������ ����
    public GUISkin skin; //�κ��丮 ���Կ� ���� ��Ų ����

    //2023-10-19 �� ( �� �� ��� �߰�)
    private bool isOpenToolTip = false;
    private string tooltip;

    //2023-10-19 �� ( �巡�� ��� �߰�)
    private bool isDrag = false;        //�巡�� ���θ� ó���ϱ� ���� ����
    private Item draggedItem;           //�巡���� ������ ������ ���� ����
    private int pre_idx;                //�����߾��� �������� ��ġ�� �����ϱ� ���� ����

    private void Start()
    {
        //���� ����
        for (int i = 0; i < slot_X * slot_Y; i++)
        {
            slots.Add(new Item());
            //�� ������ x * y��ŭ �߰�
            //Item �����ڰ� �����Ǿ��ֱ� ������, �⺻ ������ �߰��� �������� ��
            inventory.Add(new Item());
            //�κ��丮 �߰�
        }

        //���� ���ο� �ִ� ������ DB�� �˻��ؼ� ã�Ƴ��ϴ�.
        //�� �۾��� ����Ƽ ������ Item DB�� ���� �±׸� ������
        //�۵��մϴ�.
        ItemDB = GameObject.FindGameObjectWithTag("ItemDB").GetComponent<ItemDB>();
        //�����ͺ��̽��� ù��° ���� �������ڽ��ϴ�.
        //���� ���� ��쿡�� �κ��丮 ��ü�� ���� ������ �� �ֵ��� �մϴ�.

        //0 ~ 8���� �ݺ�
        for (int i = 0; i < slot_X * slot_Y; i++)
        {
            if (ItemDB.items.Count > i)
            {
                //�����ͺ��̽��� �������� �����ϴ� ���
                if (ItemDB.items[i] != null)
                {
                    inventory[i] = ItemDB.items[i];
                }
            }
        }
        //Add Item ����
        AddItem(2);
        RemoveItem(1000);
    }

    private void Update()
    {
        //����Ƽ�� Input.Manager���� ������ Inventory ��ư�� ������ ��
        if (Input.GetButtonDown("Inventory"))
        {
            isOpen = !isOpen;
        }
    }

    private void OnGUI()
    {
        //GUI�� ��Ų�� �κ��丮���� �����س��� ��Ų���� �����մϴ�.
        GUI.skin = skin;
        //������ ���� �ʱ�ȭ
        tooltip = "";

        //�κ��丮�� ���� ���ο� ���� �κ��丮�� ���� ������
        if (isOpen)
        {
            InventoryRender();
            //�κ��丮�� ������ŭ �ݺ��� �����ϸ鼭, ȭ�鿡 ���� ��� ����
            /*for (int i = 0; i < inventory.Count; i++)
            {
                //Label�� ȭ�鿡 ��µǴ� �ؽ�Ʈ�� �ǹ��ϴ� UI�Դϴ�.
                GUI.Label(new Rect(100, i * 20, 200, 50), inventory[i].name);
            }*/
        }

        GUI.Box(new Rect(10, 10, 100, 90), "Menu");
        if(GUI.Button(new Rect(20, 40, 80, 20), "Use"))
        {

        }

        //�� �� ���� ���ο� ���� ������ ���� ������
        if (isOpenToolTip)
        {
            ToolTipRender();
        }

        //�巡�� ���ο� ���� �巡�� ��� ����
        if (isDrag)
        {
            OnDragRender();
        }

    }
    private void OnDragRender()
    {
        Rect drag_rect = new Rect(Event.current.mousePosition.x - 5, Event.current.mousePosition.y - 5, 50, 50);
        GUI.DrawTexture(drag_rect, draggedItem.icon);
        //������ ��ġ��
        //�巡���� �������� �������� �׷��ݴϴ�.
    }

    private void ToolTipRender()
    {
        Rect toolRect = new Rect(Event.current.mousePosition.x + 5,
            Event.current.mousePosition.y + 2, 200, 200);

        //Event : GUI�� ���� �̺�Ʈ, �ش� ���������� OnGUI()��
        //ȣ���ϴ� ��ü�� �ǹ��մϴ�.

        // 1. Event.current : ���� OnGUI�� ȣ���ϰ� �� Event
        // 2. Layout Event : GUI�� ����� ������, ũ�Ⱑ ����� �� ó���Ǵ� Event
        // ������ ����� �Ǵ°� �ƴ� ���� ��ɰ� ������ ����
        // ��ġ�ϴ� �ܰ迡 ���� ����

        GUI.Box(toolRect, tooltip, skin.GetStyle("ToolTip"));
        //toolRect ��ǥ ������, ������ ���� ���ڿ��� �ۼ��ϰ�
        //�̹��� ��Ų�� ToolTip���� ������ �ڽ��� �׷��ݴϴ�.
    }

    /// <summary>
    /// ������ ���� ���� ���·� ����� �� �ְ� ó���մϴ�.
    /// </summary>
    private void InventoryRender()
    {
        int idx = 0; //�ε��� ǥ��

        for (int j = 0; j < slot_Y; j++)
        {
            for (int i = 0; i < slot_X; i++)
            {
                Rect slot_Rect = new Rect(i * 52 + 100, j * 52 + 30, 50, 50);

                GUI.Box(slot_Rect, "slot", skin.GetStyle("Background"));

                //���ο� ���δ� �ݺ� ��ġ�� ���� ������ ���������� ����
                //�ڽ��� slot ��ȣ�� ���
                //�ڽ��� �̹����� Background ��Ÿ�Ϸ� ����

                slots[idx] = inventory[idx];
                //���Կ� �̸��� �����ϴ� ���(�� ������ �ƴ� ���)
                if (slots[idx].name != null)
                {
                    //�ؽ��� �̹����� �׷��ִ� ����
                    GUI.DrawTexture(slot_Rect, slots[idx].icon);

                    //�� �� ��� ����
                    //���� �߰�) ���� ���콺 ������ ��� �κп��� ������ ������ �߻��ϰ� ����.
                    if (slot_Rect.Contains(Event.current.mousePosition))
                    {
                        tooltip = SetToolTip(slots[idx]);
                        isOpenToolTip = true;
                        //�巡�� ��� �߰�
                        //1. ���콺�� ���� ��ư�̸鼭, ���콺�� �巡���Ϸ��� ��Ȳ
                        if (Event.current.button == 0 && Event.current.type == EventType.MouseDrag && !isDrag)
                        {
                            //1. �巡�׿� ���� �÷��׸� Ȱ��ȭ
                            isDrag = true;
                            //2. ������ idx ���� pre_idx�� ����(��ġ ����)
                            pre_idx = idx;
                            //3. ���� ������ ���� �巡���� ���������� ����
                            draggedItem = slots[idx];
                            //4. �κ��丮�� �� �������� ��ȯ
                            inventory[idx] = new Item();
                        }
                        //2. ���콺�� ����, �巡�� �ϰ� �ִ� �������� �����ϴ� ���(�巡�� ó��)
                        if (Event.current.type == EventType.MouseUp && isDrag)
                        {
                            //1. �������� �� ��ġ�� ������ �������� ��ġ�մϴ�.
                            inventory[pre_idx] = inventory[idx];

                            //2. ���� ��ġ�� �κ��丮�� �巡���� �������� ��ġ�մϴ�.
                            inventory[idx] = draggedItem;

                            //3. �巡�� ó���� ���� �÷��� ��ȯ
                            isDrag = false;
                            //4. �巡���� �����ۿ� ���� ������ ����ݴϴ�.
                            draggedItem = null;
                        }

                        //3. �������� Ÿ���� Use�� ���
                        if (Event.current.isMouse && Event.current.type == EventType.MouseDown && Event.current.button == 1)
                        {
                            if (inventory[idx].type == Item.ItemType.Use) ;
                        }
                    }
                }
                //�� ���Կ� ���� ���
                else
                {
                    if (slot_Rect.Contains(Event.current.mousePosition))
                    {
                        //�巡�� ó��
                        if (Event.current.type == EventType.MouseUp && isDrag)
                        {
                            //�� ���Կ����� �巡���� �����ۿ� ���� ������ ������ ������
                            inventory[idx] = draggedItem;

                            //3. �巡�� ó���� ���� �÷��� ��ȯ
                            isDrag = false;
                            //4. �巡���� �����ۿ� ���� ������ ����ݴϴ�.
                            draggedItem = null;
                        }

                        
                    }
                }

                if (tooltip == "")
                    isOpenToolTip = false;
                idx++;
            }
        }
    }

    /// <summary>
    ///�������� ������ ����, ��� ����� �ٸ��� ����Ǵ� ���⼺�� ����ִ°� �����ϴ�.
    ///�̸��� Ư¡�� �ְ� ���� ��쿡�� rich text�� ����غ��� �͵� �����ϴ�.
    ///richText�� ����� ��쿡�� �ؽ�Ʈ���� rich text ������ Ȱ��ȭ�� �Ǿ��ִ����� Ȯ���մϴ�.
    /// </summary>
    /// <param name="item">�����ۿ� ���� ����</param>
    private string SetToolTip(Item item)
    {
        tooltip = $"������ �̸� :  <color=red>{item.name}</color>\n������ ���ݷ� : {item.atk}\n������ ���� : {item.def}\n������ ���� �ӵ� {item.speed}\n\n[������ ����]\n{item.description}";

        return tooltip;
    }

    /// <summary>
    /// �������� �κ��丮�� �߰��ϴ� �ڵ�
    /// </summary>
    /// <param name="id">���̵� ���� �ڵ带 ���޹޾� �۾��մϴ�.</param>
    void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            //�κ��丮�� ����� ���
            if (inventory[i].name == null)
            {
                //�����ͺ��̽��� ������ ������ �˻��մϴ�.
                for (int j = 0; j < ItemDB.items.Count; j++)
                {
                    if (ItemDB.items[j].id == id)
                    {
                        inventory[i] = ItemDB.items[j];

                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    /// <param name="id">������ ��ȣ</param>
    void RemoveItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].id == id)
            {
                Debug.Log($"{inventory[i].name}�� �����Ǿ����ϴ�.");
                inventory[i] = new Item();
                break;
            }
        }
    }
}
