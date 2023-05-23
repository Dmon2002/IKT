using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

using GS_Utilities;

namespace GameScore
{
    public class GS_Payments : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void GSPaymentsFetchProducts();
        public static void FetchProducts() => GSPaymentsFetchProducts();



        [DllImport("__Internal")]
        private static extern void GSPaymentsPurchase(string idOrTag);
        public static void Purchase(string idOrTag) => GSPaymentsPurchase(idOrTag);



        [DllImport("__Internal")]
        private static extern void GSPaymentsConsume(string idOrTag);
        public static void Consume(string idOrTag) => GSPaymentsConsume(idOrTag);



        [DllImport("__Internal")]
        private static extern string GSPaymentsIsAvailable();
        public static bool IsAvailable() => GSPaymentsIsAvailable() == "true";



        public static event UnityAction<List<FetchProducts>> OnPaymentsFetchProducts;
        public static event UnityAction<List<FetchPlayerPurcahses>> OnPaymentsFetchPlayerPurcahses;
        public static event UnityAction OnPaymentsFetchProductsError;
        public static event UnityAction<string> OnPaymentsPurchase;
        public static event UnityAction OnPaymentsPurchaseError;
        public static event UnityAction<string> OnPaymentsConsume;
        public static event UnityAction OnPaymentsConsumeError;


        private List<FetchProducts> _products;
        private List<FetchPlayerPurcahses> _playerPurchases;


        private void CallPaymentsFetchProducts(string data)
        {
            _products = GS_JSON.GetArrayList<FetchProducts>(data);
            OnPaymentsFetchProducts?.Invoke(_products);
        }
        private void CallPaymentsFetchPlayerPurcahses(string data)
        {
            _playerPurchases = GS_JSON.GetArrayList<FetchPlayerPurcahses>(data);
            OnPaymentsFetchPlayerPurcahses?.Invoke(_playerPurchases);
        }

        private void CallPaymentsFetchProductsError() => OnPaymentsFetchProductsError?.Invoke();

        private void CallPaymentsPurchase(string PuchasedIdOrTag) => OnPaymentsPurchase?.Invoke(PuchasedIdOrTag);
        private void CallPaymentsPurchaseError() => OnPaymentsPurchaseError?.Invoke();

        private void CallPaymentsConsume(string idOrTag) => OnPaymentsConsume?.Invoke(idOrTag);
        private void CallPaymentsConsumeError() => OnPaymentsConsumeError?.Invoke();
    }

    [System.Serializable]
    public struct FetchProducts
    {
        public int id;
        public string icon;
        public string iconSmall;
        public string tag;
        public string price;
        public string name;
        public string description;
        public string yandexId;
        public string currencySymbol;
        public string currency;
    }

    [System.Serializable]
    public struct FetchPlayerPurcahses
    {
        public int productId;
        public string payload;
    }
}