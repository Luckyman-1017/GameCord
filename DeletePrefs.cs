using UnityEngine;

public class DeletePrefs : MonoBehaviour
{
    [SerializeField] private OwnedItemsData _data;

    void Start()
    {
        _data.Initialize();
    }
}
