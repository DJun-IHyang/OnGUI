using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

/// <summary>
/// ���α׷� ���� ��, DB�� �߰��ص� ������ ������ ���Դϴ�.
/// �ش� ��ũ��Ʈ�� json, scriptable Object�� ���� ������
/// ���� ����� ���� �ʰ� ������ Ŭ������ ���� ������
/// ����Ʈ�� �����ϴ� ������ DB�� �����Ұ���.
/// </summary>
public class ItemDB : MonoBehaviour
{
    //�������� ������ ����Ʈ
    public List<Item> items = new List<Item>();

    private void Start()
    {
        // ����Ʈ�� �⺻ �Լ� Add�� �����͸� ����Ʈ�� �߰��ϴ� ����Դϴ�.

        // �����ۿ� ���� �߰� ����
        // �������� ������� ���ڰ��� �־ ������ ������

        // �� ó���� �־��ִ� ������ ���� ���ҽ� ������ �ִ� �̹����� �̸�
        // �̸��� ������ ������ �����ϱ� ������, ���ҽ� ���� ���� �̹�����
        // ���ڷ� �����س����� ����
        items.Add(new Item("1", "�ϵ� ����", 100, "basic_armor", 0, 5, 0, ItemType.Armor));
        items.Add(new Item("2", "���", 1000, "�� ������ �ִ�.", 0, 0, 0, ItemType.Use));
        items.Add(new Item("3", "����", 1, "���� �� ���ִ�.", 1, 0, 0, ItemType.Weapon));
        items.Add(new Item("4", "���", 2, "������ HP�� ȸ���ȴ�.", 0, 0, 0, ItemType.Use));
        items.Add(new Item("5", "HP����", 3, "���ø� HP�� ȸ���ȴ�.", 0, 0, 0, ItemType.Use)); 


    }
}
