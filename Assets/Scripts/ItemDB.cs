using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

/// <summary>
/// 프로그램 시작 시, DB에 추가해둔 정보를 갱신할 것입니다.
/// 해당 스크립트는 json, scriptable Object와 같은 데이터
/// 저장 기능을 쓰지 않고 아이템 클래스에 대한 정보를
/// 리스트로 저장하는 것으로 DB를 연출할것임.
/// </summary>
public class ItemDB : MonoBehaviour
{
    //아이템을 저장할 리스트
    public List<Item> items = new List<Item>();

    private void Start()
    {
        // 리스트의 기본 함수 Add는 데이터를 리스트에 추가하는 기능입니다.

        // 아이템에 대한 추가 진행
        // 생성자의 순서대로 인자값을 넣어서 생성을 진행함

        // 맨 처음에 넣어주는 문장은 실제 리소스 폴더에 있는 이미지의 이름
        // 이름을 일일이 넣으면 불편하기 때문에, 리소스 폴더 내의 이미지는
        // 숫자로 저장해놓으면 편리함
        items.Add(new Item("1", "하드 레더", 100, "basic_armor", 0, 5, 0, ItemType.Armor));
        items.Add(new Item("2", "고기", 1000, "잘 구워져 있다.", 0, 0, 0, ItemType.Use));
        items.Add(new Item("3", "억까검", 1, "날이 잘 서있다.", 1, 0, 0, ItemType.Weapon));
        items.Add(new Item("4", "사과", 2, "먹으면 HP가 회복된다.", 0, 0, 0, ItemType.Use));
        items.Add(new Item("5", "HP물약", 3, "마시면 HP가 회복된다.", 0, 0, 0, ItemType.Use)); 


    }
}
