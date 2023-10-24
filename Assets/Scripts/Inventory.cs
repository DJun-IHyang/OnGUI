using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 아이템을 리스트로 관리하는 인벤토리를 만들겠습니다.
/// 인벤토리 내부에서는 데이터베이스의 정보를 받아 그 값으로
/// onGUI를 통해 UI를 렌더링합니다.
/// </summary>

public class Inventory : MonoBehaviour
{
    //아이템 인벤토리
    public List<Item> inventory = new List<Item>();
    //인벤토리에 값을 전달할 데이터베이스
    private ItemDB ItemDB;
    //인벤토리 오픈 여부 확인( 기본 상태 : false)
    public bool isOpen = false;

    public int slot_X, slot_Y; //인벤토리의 가로와 세로
    public List<Item> slots = new List<Item>(); //아이템 슬롯
    public GUISkin skin; //인벤토리 슬롯에 대한 스킨 설정

    //2023-10-19 목 ( 툴 팁 기능 추가)
    private bool isOpenToolTip = false;
    private string tooltip;

    //2023-10-19 목 ( 드래그 기능 추가)
    private bool isDrag = false;        //드래그 여부를 처리하기 위한 변수
    private Item draggedItem;           //드래그한 아이템 정보에 대한 저장
    private int pre_idx;                //선택했었던 아이템의 위치를 저장하기 위한 변수

    private void Start()
    {
        //슬롯 생성
        for (int i = 0; i < slot_X * slot_Y; i++)
        {
            slots.Add(new Item());
            //빈 슬롯을 x * y만큼 추가
            //Item 생성자가 수정되어있기 때문에, 기본 생성자 추가를 진행해줄 것
            inventory.Add(new Item());
            //인벤토리 추가
        }

        //게임 내부에 있는 아이템 DB를 검색해서 찾아냅니다.
        //이 작업은 유니티 내에서 Item DB에 대한 태그를 만들어야
        //작동합니다.
        ItemDB = GameObject.FindGameObjectWithTag("ItemDB").GetComponent<ItemDB>();
        //데이터베이스의 첫번째 값을 가져오겠습니다.
        //여러 개일 경우에는 인벤토리 전체의 값을 가져올 수 있도록 합니다.

        //0 ~ 8까지 반복
        for (int i = 0; i < slot_X * slot_Y; i++)
        {
            if (ItemDB.items.Count > i)
            {
                //데이터베이스의 아이템이 존재하는 경우
                if (ItemDB.items[i] != null)
                {
                    inventory[i] = ItemDB.items[i];
                }
            }
        }
        //Add Item 진행
        AddItem(2);
        RemoveItem(1000);
    }

    private void Update()
    {
        //유니티의 Input.Manager에서 설정한 Inventory 버튼을 눌렀을 때
        if (Input.GetButtonDown("Inventory"))
        {
            isOpen = !isOpen;
        }
    }

    private void OnGUI()
    {
        //GUI의 스킨을 인벤토리에서 연결해놓은 스킨으로 설정합니다.
        GUI.skin = skin;
        //툴팁에 대한 초기화
        tooltip = "";

        //인벤토리의 오픈 여부에 따라 인벤토리에 대한 렌더링
        if (isOpen)
        {
            InventoryRender();
            //인벤토리의 개수만큼 반복을 진행하면서, 화면에 대한 출력 진행
            /*for (int i = 0; i < inventory.Count; i++)
            {
                //Label은 화면에 출력되는 텍스트를 의미하는 UI입니다.
                GUI.Label(new Rect(100, i * 20, 200, 50), inventory[i].name);
            }*/
        }

        GUI.Box(new Rect(10, 10, 100, 90), "Menu");
        if(GUI.Button(new Rect(20, 40, 80, 20), "Use"))
        {

        }

        //툴 팁 오픈 여부에 따라 툴팁에 대한 렌더링
        if (isOpenToolTip)
        {
            ToolTipRender();
        }

        //드래그 여부에 따라 드래그 기능 설정
        if (isDrag)
        {
            OnDragRender();
        }

    }
    private void OnDragRender()
    {
        Rect drag_rect = new Rect(Event.current.mousePosition.x - 5, Event.current.mousePosition.y - 5, 50, 50);
        GUI.DrawTexture(drag_rect, draggedItem.icon);
        //지정한 위치에
        //드래그한 아이템의 아이콘을 그려줍니다.
    }

    private void ToolTipRender()
    {
        Rect toolRect = new Rect(Event.current.mousePosition.x + 5,
            Event.current.mousePosition.y + 2, 200, 200);

        //Event : GUI에 대한 이벤트, 해당 예제에서는 OnGUI()를
        //호출하는 주체를 의미합니다.

        // 1. Event.current : 현재 OnGUI를 호출하게 된 Event
        // 2. Layout Event : GUI의 요소의 포지션, 크기가 변경될 때 처리되는 Event
        // 실제로 드로잉 되는게 아닌 관련 기능과 정보를 조사
        // 배치하는 단계에 대한 조사

        GUI.Box(toolRect, tooltip, skin.GetStyle("ToolTip"));
        //toolRect 좌표 지점에, 툴팁에 적은 문자열을 작성하고
        //이미지 스킨은 ToolTip으로 설정해 박스를 그려줍니다.
    }

    /// <summary>
    /// 슬롯을 가로 세로 형태로 출력할 수 있게 처리합니다.
    /// </summary>
    private void InventoryRender()
    {
        int idx = 0; //인덱스 표현

        for (int j = 0; j < slot_Y; j++)
        {
            for (int i = 0; i < slot_X; i++)
            {
                Rect slot_Rect = new Rect(i * 52 + 100, j * 52 + 30, 50, 50);

                GUI.Box(slot_Rect, "slot", skin.GetStyle("Background"));

                //가로와 세로는 반복 수치에 따라 간격이 벌어지도록 설정
                //박스에 slot 번호를 출력
                //박스의 이미지는 Background 스타일로 적용

                slots[idx] = inventory[idx];
                //슬롯에 이름이 존재하는 경우(빈 슬롯이 아닌 경우)
                if (slots[idx].name != null)
                {
                    //텍스쳐 이미지를 그려주는 도구
                    GUI.DrawTexture(slot_Rect, slots[idx].icon);

                    //툴 팁 기능 연결
                    //오류 발견) 현재 마우스 가져다 대는 부분에서 자잘한 오류가 발생하고 있음.
                    if (slot_Rect.Contains(Event.current.mousePosition))
                    {
                        tooltip = SetToolTip(slots[idx]);
                        isOpenToolTip = true;
                        //드래그 기능 추가
                        //1. 마우스가 왼쪽 버튼이면서, 마우스가 드래그하려는 상황
                        if (Event.current.button == 0 && Event.current.type == EventType.MouseDrag && !isDrag)
                        {
                            //1. 드래그에 대한 플래그를 활성화
                            isDrag = true;
                            //2. 현재의 idx 값을 pre_idx에 전달(위치 저장)
                            pre_idx = idx;
                            //3. 현재 슬롯의 값을 드래그한 아이템으로 설정
                            draggedItem = slots[idx];
                            //4. 인벤토리를 빈 슬롯으로 전환
                            inventory[idx] = new Item();
                        }
                        //2. 마우스를 떼고, 드래그 하고 있는 아이템이 존재하는 경우(드래그 처리)
                        if (Event.current.type == EventType.MouseUp && isDrag)
                        {
                            //1. 아이템의 전 위치에 현재의 아이템을 배치합니다.
                            inventory[pre_idx] = inventory[idx];

                            //2. 현재 위치의 인벤토리에 드래그한 아이템을 배치합니다.
                            inventory[idx] = draggedItem;

                            //3. 드래그 처리에 대한 플래그 전환
                            isDrag = false;
                            //4. 드래그한 아이템에 대한 정보는 비워줍니다.
                            draggedItem = null;
                        }

                        //3. 아이템의 타입이 Use인 경우
                        if (Event.current.isMouse && Event.current.type == EventType.MouseDown && Event.current.button == 1)
                        {
                            if (inventory[idx].type == Item.ItemType.Use) ;
                        }
                    }
                }
                //빈 슬롯에 놓는 경우
                else
                {
                    if (slot_Rect.Contains(Event.current.mousePosition))
                    {
                        //드래그 처리
                        if (Event.current.type == EventType.MouseUp && isDrag)
                        {
                            //빈 슬롯에서는 드래그한 아이템에 대한 정보만 넣으면 마무리
                            inventory[idx] = draggedItem;

                            //3. 드래그 처리에 대한 플래그 전환
                            isDrag = false;
                            //4. 드래그한 아이템에 대한 정보는 비워줍니다.
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
    ///아이템의 유형에 따라, 출력 결과가 다르게 설계되는 방향성을 잡아주는게 좋습니다.
    ///이름에 특징을 주고 싶을 경우에는 rich text를 사용해보는 것도 좋습니다.
    ///richText를 사용할 경우에는 텍스트에서 rich text 설정이 활성화가 되어있는지를 확인합니다.
    /// </summary>
    /// <param name="item">아이템에 대한 정보</param>
    private string SetToolTip(Item item)
    {
        tooltip = $"아이템 이름 :  <color=red>{item.name}</color>\n아이템 공격력 : {item.atk}\n아이템 방어력 : {item.def}\n아이템 공격 속도 {item.speed}\n\n[아이템 설명]\n{item.description}";

        return tooltip;
    }

    /// <summary>
    /// 아이템을 인벤토리에 추가하는 코드
    /// </summary>
    /// <param name="id">아이디에 대한 코드를 전달받아 작업합니다.</param>
    void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            //인벤토리가 비었을 경우
            if (inventory[i].name == null)
            {
                //데이터베이스의 아이템 정보를 검색합니다.
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
    /// 아이템 삭제
    /// </summary>
    /// <param name="id">아이템 번호</param>
    void RemoveItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].id == id)
            {
                Debug.Log($"{inventory[i].name}이 삭제되었습니다.");
                inventory[i] = new Item();
                break;
            }
        }
    }
}
