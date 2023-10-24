using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

//속성 [System.Serializable]
//유니티 에디터에서 직접 클래스, 구조체에 대한 접근을 진행 할 수 있게 처리해주는 속성



[System.Serializable]
public class Item
{
    //아이템에 관련된 기본 정보
    //아이템 이름, 아이템 고유 번호, 아이템 설명, 아이콘
    [Header("========== 아이템 기본 정보 ===========")]
    public string name;
    public int id;
    public string description;
    public Texture2D icon;

    [Header("========== 능력치 ============")]
    public int atk;
    public int def;
    public int speed;
    public ItemType type;

    /// <summary>
    /// 아이템의 유형을 표현하는 enum
    /// 0 : 무기
    /// 1 : 방어구
    /// 2 : 소모품
    /// </summary>
    public enum ItemType
    {
        Weapon, Armor, Use
    }

    public Item(string icon_text, string name, int id, string description, int atk, int def, int speed, ItemType type)
    {
        this.name = name;
        this.id = id;
        this.description = description;
        this.atk = atk;
        this.def = def;
        this.speed = speed;
        this.type = type;

        icon = Resources.Load<Texture2D>($"ItemIcon/" + icon_text);
        //리소스 폴더/아이템 아이콘 폴더/파일 이름
    }


    /// <summary>
    /// 기본 생성자(default Constructor)
    /// 해당 생성자가 있을 경우, 클래스를 선언할 수 있습니다.
    /// 따로 생성자를 만들지 않았을 경우, 자동으로 설정되는 형태이기도 함.
    /// </summary>
    public Item()
    {
    }

}
