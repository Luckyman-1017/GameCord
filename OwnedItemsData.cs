using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OwnedItemsData
{
    [Serializable]
    public class OwnedItem  //インナークラス
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

    [SerializeField] private List<OwnedItem> ownedItems = new List<OwnedItem>();//アイテムリスト

    private static OwnedItemsData _instance;
    private const string PlayerPrefsKey = "OWNED_ITEMS_DATA";//PlayerPrefs保存先キー名
    private OwnedItem _ownedItem;

    public static OwnedItemsData Instance
    {
        get
        {
            if (_instance == null)//アイテム取得時の処理
            {
                _instance = PlayerPrefs.HasKey(PlayerPrefsKey) ?//データ上にインスタンスがあるか
                    JsonUtility.FromJson<OwnedItemsData>(PlayerPrefs.GetString(PlayerPrefsKey))//あると読み込み
                    : new OwnedItemsData();//無いとインスタンス化
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
        PlayerPrefs.SetString(PlayerPrefsKey, jsonString);//キーと値を設定
        PlayerPrefs.Save();//設定を保存
    }

    public void Initialize()
    {
        PlayerPrefs.DeleteKey(PlayerPrefsKey);
        PlayerPrefs.Save();
    }

    public void Add(Item.ItemType type, int number = 1)
    {
        var item = GetItem(type);//アイテムのタイプを取得
        if (item == null)//初めてのタイプだと
        {
            item = new OwnedItem(type);//アイテムのタイプを新たに作る
            ownedItems.Add(item);
        }
        item.Add(number);
    }

    public void Use(Item.ItemType type, int number = 1)
    {
        var item = GetItem(type);
        if (item == null || item.Number < number)
        {
            throw new Exception("アイテムが足りません");
        }
        item.Use(number);
    }

    public OwnedItem GetItem(Item.ItemType type)
    {
        return ownedItems.FirstOrDefault(x => x.Type == type);
    }
}