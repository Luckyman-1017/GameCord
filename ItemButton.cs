using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemButton : MonoBehaviour
{
    [SerializeField] private ItemTypeSpriteMap[] itemSprites;
    [SerializeField] private Image image;
    [SerializeField] private Text number;

    private Button _button;
    private OwnedItemsData.OwnedItem _ownedItem;

    public OwnedItemsData.OwnedItem OwnedItem
    {
        get { return _ownedItem; }
        set
        {
            _ownedItem = value;

            var isEmpty = _ownedItem == null;
            image.gameObject.SetActive(!isEmpty);
            number.gameObject.SetActive(!isEmpty);
            _button.interactable = !isEmpty;
            if (!isEmpty)
            {
                image.sprite = itemSprites.First(x => x.itemType == _ownedItem.Type).sprite;
                number.text = "" + _ownedItem.Number;
            }
        }
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {

    }

    [Serializable]
    public class ItemTypeSpriteMap
    {
        public Item.ItemType itemType;
        public Sprite sprite;
    }
}
