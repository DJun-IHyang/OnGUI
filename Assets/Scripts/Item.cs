using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

//�Ӽ� [System.Serializable]
//����Ƽ �����Ϳ��� ���� Ŭ����, ����ü�� ���� ������ ���� �� �� �ְ� ó�����ִ� �Ӽ�



[System.Serializable]
public class Item
{
    //�����ۿ� ���õ� �⺻ ����
    //������ �̸�, ������ ���� ��ȣ, ������ ����, ������
    [Header("========== ������ �⺻ ���� ===========")]
    public string name;
    public int id;
    public string description;
    public Texture2D icon;

    [Header("========== �ɷ�ġ ============")]
    public int atk;
    public int def;
    public int speed;
    public ItemType type;

    /// <summary>
    /// �������� ������ ǥ���ϴ� enum
    /// 0 : ����
    /// 1 : ��
    /// 2 : �Ҹ�ǰ
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
        //���ҽ� ����/������ ������ ����/���� �̸�
    }


    /// <summary>
    /// �⺻ ������(default Constructor)
    /// �ش� �����ڰ� ���� ���, Ŭ������ ������ �� �ֽ��ϴ�.
    /// ���� �����ڸ� ������ �ʾ��� ���, �ڵ����� �����Ǵ� �����̱⵵ ��.
    /// </summary>
    public Item()
    {
    }

}
