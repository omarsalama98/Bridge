using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBapplication
{
    public class StoredProcedures
    {
        public static string RetreiveUser = "spRetreiveUser";
        public static string updateClient = "UpdateClient";
        public static string UserSignInAuth = "spUserSignIn";
        public static string retreiveStores = "spGetStores";
        public static string retreiveStoresofArea = "spGetStoresOfArea";
        public static string retreiveItemsOfStore = "spGetItemsOfStore";
        public static string retreiveAllItems = "spGetItems";
        public static string retreiveMyOrders = "spGetOrdersOfClient";
        public static string retreiveItem = "spGetItem";
        public static string retreiveAllOffers = "SelectFromOffer";
        public static string retreiveOffersOfStore = "spGetOffersOfStore";
        public static string retreiveItemsOfOffer = "spGetItemsOfOffer";
        public static string InsertOrder = "InsertOrders";
        public static string InsertCurrentOrder = "InsertCurrentOrder";
        public static string GetLatestOrder = "spGetLatestOrder";
        public static string GetLatestReview = "spGetLatestReview";
        public static string GetAddressLoc = "spGetAddressLocation";
        public static string GetAvailableCouriers = "AvailableCouriers";
        public static string GetAreas = "spGetAreas";
        public static string InsertClientPhoneNum = "InsertClientPhoneNumber";
        public static string InsertClient = "InsertClient";
        public static string InsertCreditCard = "InsertClientCreditCard";
        public static string InsertOrderContainsItem = "InsertOrderContainsItem";
        public static string InsertOrderContainsOffer = "InsertOrderContainsOffer";
        public static string InsertRequest = "InsertRequest";
        public static string getStorebyId = "SelectFromStore";
        public static string getCurrentOrderDetails = "spGetCurrentOrderDetails";
        public static string getOrderDetails = "GetOrderDetails";
        public static string InsertReview = "InsertReview";
        public static string InsertReviewCourier = "InsertReviewCourier";
        public static string InsertReviewStore = "InsertReviewStore";
        public static string InsertReviewOffer = "InsertReviewOffer";
        public static string InsertReviewItem = "InsertReviewItem";
        public static string updateOrderRating = "spUpdateOrderRating";
    }
}
