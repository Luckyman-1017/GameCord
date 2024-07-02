using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OwnedItemsData
{
    [Serializable]
    public class OwnedItem  //�C���i�[�N���X
    {

        [SerializeField] private Item.ItemType type;
        [SerializeField] private int number;

        public Item.ItemType Type
        {
            get { return type; }
        }

        public int Number
        {
            get { return number; }
        }

        public OwnedItem(Item.ItemType type)
        {
            this.type = type;
        }

        public void Add(int number = 1)
        {
            this.number += number;
        }

        public void Use(int number = 1)
        {
            this.number -= number;
        }
    }

    [SerializeField] private List<OwnedItem> ownedItems = new List<OwnedItem>();//�A�C�e�����X�g

    private static OwnedItemsData _instance;
    private const string PlayerPrefsKey = "OWNED_ITEMS_DATA";//PlayerPrefs�ۑ���L�[��
    private OwnedItem _ownedItem;

    public static OwnedItemsData Instance
    {
        get
        {
            if (_instance == null)//�A�C�e���擾���̏���
            {
                _instance = PlayerPrefs.HasKey(PlayerPrefsKey) ?//�f�[�^��ɃC���X�^���X�����邩
                    JsonUtility.FromJson<OwnedItemsData>(PlayerPrefs.GetString(PlayerPrefsKey))//����Ɠǂݍ���
                    : new OwnedItemsData();//�����ƃC���X�^���X��
            }

            return _instance;
        }
    }

    public OwnedItem[] OwnedItems
    {
        get 
        { 
            return ownedItems.ToArray();
        }
    }

    private OwnedItemsData()
    {

    }

    public void Save()
    {
        var jsonString = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(PlayerPrefsKey, jsonString);//�L�[�ƒl��ݒ�
        PlayerPrefs.Save();//�ݒ��ۑ�
    }

    public void Initialize()
    {
        PlayerPrefs.DeleteKey(PlayerPrefsKey);
        PlayerPrefs.Save();
    }

    public void Add(Item.ItemType type, int number = 1)
    {
        var item = GetItem(type);//�A�C�e���̃^�C�v���擾
        if (item == null)//���߂Ẵ^�C�v����
        {
            item = new OwnedItem(type);//�A�C�e���̃^�C�v��V���ɍ��
            ownedItems.Add(item);
        }
        item.Add(number);
    }

    public void Use(Item.ItemType type, int number = 1)
    {
        var item = GetItem(type);
        if (item == null || item.Number < number)
        {
            throw new Exception("�A�C�e��������܂���");
        }
        item.Use(number);
    }

    public OwnedItem GetItem(Item.ItemType type)
    {
        return ownedItems.FirstOrDefault(x => x.Type == type);
    }
}