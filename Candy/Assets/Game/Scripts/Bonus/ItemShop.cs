using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    [SerializeField] private int numberItem;
    [SerializeField] private Button _buyBtn;
    [SerializeField] private int price;
    [SerializeField] private Text _priceText;

    private void Awake()
    {
        _priceText.text = price.ToString();

        _buyBtn.onClick.AddListener(() =>
        {
            if (BalanceUser.Instance.Balance >= price)
            {
                BalanceUser.Instance.DiscreaseBalance(price);
                StoreHandler.I.BuySuccessItem(numberItem);
            }
        });
    }
}
