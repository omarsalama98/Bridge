using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CourierUser;

namespace DBapplication
{
    public class Controller
    {
        DBManager dbMan;
        public Controller()
        {
            dbMan = new DBManager();
        }

      
        public void TerminateConnection()
        {
            dbMan.CloseConnection();
        }
        public List<string> RetreiveUser(string userName)
        {
            string StoredProcedureName = StoredProcedures.RetreiveUser;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserName", userName);
            return dbMan.ExecuteUserInfo(StoredProcedureName, Parameters);
        }
        public int InsertClient(string userName, string FName, string MName, string LName, char Sex, string Password, string Email, string BirthDate, string Address=" ")
        {
            string StoredProcedureName = StoredProcedures.InsertClient;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_Username", userName);
            Parameters.Add("@_Password", Password);
            Parameters.Add("@_Sex", Sex);
            Parameters.Add("@_Fname", FName);
            Parameters.Add("@_Mname", MName);
            Parameters.Add("@_Lname", LName);
            Parameters.Add("@_Birthdate", BirthDate);
            Parameters.Add("@_Email", Email);
            Parameters.Add("@_ClientAddress", Address);
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters); 
        }
        public int InsertClientPhoneNum(string userName, string PhoneNum)
        {
            string StoredProcedureName = StoredProcedures.InsertClientPhoneNum;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_ClientUN", userName);
            Parameters.Add("@_Pnumber", PhoneNum);
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int InsertCreditCard(string userName, string creditCardNum, int creditCardCVC)
        {
            string StoredProcedureName = StoredProcedures.InsertCreditCard;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_ClientUN", userName);
            Parameters.Add("@_Cnumber", creditCardNum);
            Parameters.Add("@_CVC", creditCardCVC);
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int InsertRequest(string userName, string details)
        {
            string StoredProcedureName = StoredProcedures.InsertRequest;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_ClientUN", userName);
            Parameters.Add("@_Details", details);
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int UpdateClient(string userName, string pass, string fname, string mname,string lname, string email)
        {
            string StoredProcedureName = StoredProcedures.updateClient;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_Username", userName);
            Parameters.Add("@_Password", pass);
            Parameters.Add("@_Fname", fname);
            Parameters.Add("@_Mname",mname);
            Parameters.Add("@_Lname",lname);
            Parameters.Add("@_Email",email);	
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int UserSignIn(string userName, string password)
        {
            string StoredProcedureName = StoredProcedures.UserSignInAuth;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@UserName", userName);
            Parameters.Add("@Password", password);
            return int.Parse(dbMan.ExecuteScalar(StoredProcedureName, Parameters).ToString());
        }
        public Store getStoreById(int storeId)
        {
            string StoredProcedureName = StoredProcedures.getStorebyId;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_StoreID", storeId);
            return dbMan.ExecuteStore(StoredProcedureName, Parameters);
        }
        public List<Store> getStores()
        {
            string StoredProcedureName = StoredProcedures.retreiveStores;
            return dbMan.ExecuteStoresList(StoredProcedureName,null);
        }
        public List<Store> getStoresOfArea(string area)
        {
            string StoredProcedureName = StoredProcedures.retreiveStoresofArea;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@Area", area);
            return dbMan.ExecuteStoresList(StoredProcedureName, Parameters);
        }
        public List<Item> getItemsOfStore(int StoreID)
        {
            string StoredProcedureName = StoredProcedures.retreiveItemsOfStore;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@StoreID", StoreID);
            return dbMan.ExecuteItemsOfStore(StoredProcedureName, Parameters);
        }
        public List<string> getCurrentOrderDets(string ClientUN)
        {
            string StoredProcedureName = StoredProcedures.getCurrentOrderDetails;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@ClientUN", ClientUN);
            return dbMan.ExecuteCurrentOrder(StoredProcedureName, Parameters);
        }
        public List<int> getOrderDets(int OrderID)
        {
            string StoredProcedureName = StoredProcedures.getOrderDetails;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@OrderID", OrderID);
            return dbMan.ExecuteOrder(StoredProcedureName, Parameters);
        }
        public List<Item> getAllItems()
        {
            string StoredProcedureName = StoredProcedures.retreiveAllItems;
            return dbMan.ExecuteItemsOfStore(StoredProcedureName, null);
        }
        public List<Offer> getAllOffers()
        {
            string StoredProcedureName = StoredProcedures.retreiveAllOffers;
            return dbMan.ExecuteOffersList(StoredProcedureName, null);
        }
        public List<Offer> getOffersofStore(int storeID)
        {
            string StoredProcedureName = StoredProcedures.retreiveOffersOfStore;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@StoreID", storeID);
            return dbMan.ExecuteOffersList(StoredProcedureName, null);
        }
        public void UpdateOrderRating(int OrderId, int rating)
        {
            string StoredProcedureName = StoredProcedures.updateOrderRating;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@OrderID", OrderId);
            Parameters.Add("@Rating", rating);
            dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public List<string> getAllAreas()
        {
            string StoredProcedureName = StoredProcedures.GetAreas;
            return dbMan.ExecuteAreasList(StoredProcedureName, null);
        }
        public List<Order> getAllOrders(string clientId)
        {
            string StoredProcedureName = StoredProcedures.retreiveMyOrders;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@ClientUN", clientId);
            return dbMan.ExecuteOrdersList(StoredProcedureName, Parameters);
        }
        public List<Item> getItemsOfOffer(int offerId)
        {
            string StoredProcedureName = StoredProcedures.retreiveItemsOfOffer;     
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@OfferID", offerId);
            return dbMan.ExecuteItemsOfOffer(StoredProcedureName, Parameters);
        }
        public int InsertReview(int Rating, string ReviewDetails)
        {
            string StoredProcedureName = StoredProcedures.InsertReview;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_Rating", Rating);
            Parameters.Add("@_ReviewText", ReviewDetails);
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int InsertReviewCourier(int ReviewId, string clientUN, int courierId)
        {
            string StoredProcedureName = StoredProcedures.InsertReviewCourier;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_ReviewId", ReviewId);
            Parameters.Add("@_ClientUN", clientUN);
            Parameters.Add("@_CourierId", courierId);
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int InsertReviewOffer(int ReviewId, string clientUN, int OfferId)
        {
            string StoredProcedureName = StoredProcedures.InsertReviewOffer;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_ReviewId" , ReviewId);
            Parameters.Add("@_ClientUN" , clientUN);
            Parameters.Add("@_OfferId" , OfferId);
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int InsertReviewStore(int ReviewId, string clientUN, int StoreId)
        {
            string StoredProcedureName = StoredProcedures.InsertReviewStore;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_ReviewId", ReviewId);
            Parameters.Add("@_ClientUN", clientUN);
            Parameters.Add("@_StoreId", StoreId);
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int InsertReviewItem(int ReviewId, string clientUN, int ItemId)
        {
            string StoredProcedureName = StoredProcedures.InsertReviewItem;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_ReviewId", ReviewId);
            Parameters.Add("@_ClientUN", clientUN);
            Parameters.Add("@_ItemId", ItemId);
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int getLatestOrderId()
        {
            string StoredProcedureName = StoredProcedures.GetLatestOrder;
            return int.Parse(dbMan.ExecuteScalar(StoredProcedureName, null).ToString());
        }
        public int getLatestReviewId()
        {
            string StoredProcedureName = StoredProcedures.GetLatestReview;
            return int.Parse(dbMan.ExecuteScalar(StoredProcedureName, null).ToString());
        }
        public void insertOrder(int storeId, int price, DateTime timeStamp, DateTime ArrivalTime, string OrderStatus = "Delivering")
        {
            string StoredProcedureName = StoredProcedures.InsertOrder;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_StoreId", storeId);
            Parameters.Add("@_TotalPrice", price);
            Parameters.Add("@_Stamp", timeStamp);
            Parameters.Add("@_OrderStatus", OrderStatus);
            Parameters.Add("@_ArrivalPeriod", ArrivalTime);
            dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }

        public string getAddressLoc(int itemStoreID)
        {
            string StoredProcedureName = StoredProcedures.GetAddressLoc;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@StoreID", itemStoreID);
            return dbMan.ExecuteScalar(StoredProcedureName, Parameters).ToString();
        }

        public int insertCurrentOrder(int orderId, string userName, string areaLoc, int courierId)
        {
            string StoredProcedureName = StoredProcedures.InsertCurrentOrder;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_OrderId", orderId);
            Parameters.Add("@_ClientUN", userName);
            Parameters.Add ("@_AreaLoc", areaLoc);
            Parameters.Add("@_CourierId", courierId);
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int insertOrderContainsItem(int orderId, int itemId, int itemCount)
        {
            string StoredProcedureName = StoredProcedures.InsertOrderContainsItem;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_OrderId", orderId);
            Parameters.Add("@_ItemId", itemId);
            Parameters.Add("@_ItemCount", itemCount);
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public int insertOrderContainsOffer(int orderId, int offerId)
        {
            string StoredProcedureName = StoredProcedures.InsertOrderContainsOffer;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@_OrderId", orderId);
            Parameters.Add("@_OfferId", offerId);
            return dbMan.ExecuteNonQuery(StoredProcedureName, Parameters);
        }
        public List<int> getAvailableCouriers(int storeId)
        {
            string StoredProcedureName = StoredProcedures.GetAvailableCouriers;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@StoreID", storeId);
            return dbMan.ExecuteAvailableCouriers(StoredProcedureName, Parameters);
        }
        public Item getItem(int ItemID)
        {
            string StoredProcedureName = StoredProcedures.retreiveItem;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add("@ItemID", ItemID);
            return dbMan.ExecuteItem(StoredProcedureName, Parameters);
        }
    }
}
