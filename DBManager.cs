using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Android.Runtime;
using CourierUser;
using Android.Widget;
using Android.Graphics;

namespace DBapplication
{
    public class DBManager
    {
        static string DB_Connection_String = @"conn_string";
        SqlConnection myConnection;
        public DBManager()
        {
            myConnection = new SqlConnection(DB_Connection_String);
            try
            {
                myConnection.Open();
                Console.WriteLine("The DB connection is opened successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("The DB connection is failed");
                Console.WriteLine(e.ToString());
            }
        }

        public int ExecuteNonQuery(string storedProcedureName, Dictionary<string, object> parameters)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand(storedProcedureName, myConnection);

                myCommand.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> Param in parameters)
                {
                    myCommand.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                }

                return myCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public DataTable ExecuteReader(string storedProcedureName, Dictionary<string, object> parameters)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand(storedProcedureName, myConnection);

                myCommand.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> Param in parameters)
                    {
                        myCommand.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                    }
                }

                SqlDataReader reader = myCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                }
                else
                {
                    reader.Close();
                    return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
       
        public List<Store> ExecuteStoresList(string storedProcedureName, Dictionary<string, object> parameters)
        {
            List<Store> stores = new List<Store>();
            SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> Param in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                }
            }
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                
                while (reader.Read())
                {
                    string name = reader.GetString(Store.columnName);
                    int id = reader.GetInt32(Store.columnID);
                    string imgurl;
                    if (!reader.IsDBNull(Store.columnImage))
                    {
                        imgurl = reader.GetString(Store.columnImage);
                    }
                    else
                    {
                        imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                    }
                    var imageBitmap = loadingImages.GetImageBitmapFromUrl(imgurl);
                    int rating;
                    if (!reader.IsDBNull(Store.columnRating))
                    {
                        rating = reader.GetInt32(Store.columnRating);
                    }
                    else
                    {
                        rating = 4;
                    }
                    Store store = new Store(id,name,imageBitmap,rating); 
                    stores.Add(store);
                }
            }
            return stores;
        }

        public List<Order> ExecuteOrdersList(string storedProcedureName, Dictionary<string, object> parameters)
        {
            List<Order> Orders = new List<Order>();
            SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> Param in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                }
            }
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    int OID = reader.GetInt32(Order.columnOID);
                    int Sid = reader.GetInt32(Order.columnSID);
                    string status = reader.GetString(Order.columnStatus);
                    int price = reader.GetInt32(Order.columnPrice);
                    TimeSpan timeStamp = reader.GetTimeSpan(Order.columnTimeStamp);
                    TimeSpan Arrival;
                    if (!reader.IsDBNull(Order.columnArrivalPeriod))
                    {
                        Arrival = reader.GetTimeSpan(Order.columnArrivalPeriod);
                    }
                    else Arrival = timeStamp;   // Default value if null

                    Order order = new Order(OID, Sid, status, Arrival, price, timeStamp);
                    Orders.Add(order);
                }
            }
            return Orders;
        }

        public List<Item> ExecuteItemsOfStore(string storedProcedureName, Dictionary<string, object> parameters)
        {
            List<Item> items = new List<Item>();
            SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> Param in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                }
            }
             
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                
                while (reader.Read())
                {
                    string name = reader.GetString(Item.columnName);
                    int id = reader.GetInt32(Item.columnID);
                    string imgurl;
                    if (!reader.IsDBNull(Item.columnImage))
                    {
                        imgurl = reader.GetString(Item.columnImage);
                        if (imgurl.Length < 3) imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                    }
                    else
                    {
                        imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                    }
                    var imageBitmap = loadingImages.GetImageBitmapFromUrl(imgurl);
                    int rating;
                    if (!reader.IsDBNull(Item.columnRating))
                    {
                        rating = reader.GetInt32(Item.columnRating);
                    }
                    else rating = 4;        // default value
                    string category = reader.GetString(Item.columnCategory);
                    int price = reader.GetInt32(Item.columnPrice);
                    int itemCount = reader.GetInt32(Item.columnCount);
                    int storeId = reader.GetInt32(Item.columnStoreId);
                    Item item = new Item(id, storeId, name, category, imageBitmap, rating, price, itemCount);
                    items.Add(item);
                }
            }
            return items;
        }
        public List<Item> ExecuteItemsOfOffer(string storedProcedureName, Dictionary<string, object> parameters)
        {
            List<Item> items = new List<Item>();
            SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> Param in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                }
            }

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    string name = reader.GetString(Item.columnName);
                    int id = reader.GetInt32(Item.columnID); 
                    string imgurl;
                    if (!reader.IsDBNull(Item.columnImage))
                    {
                        imgurl = reader.GetString(Item.columnImage);
                        if (imgurl.Length < 3) imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                    }
                    else
                    {
                        imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                    }
                    var imageBitmap = loadingImages.GetImageBitmapFromUrl(imgurl);
                    int rating;
                    if (!reader.IsDBNull(Item.columnRating))
                    {
                        rating = reader.GetInt32(Item.columnRating);
                    }
                    else rating = 4;        // default value

                    string category = reader.GetString(Item.columnCategory);
                    int itemCount = reader.GetInt32(Item.columnCount);
                    int price = reader.GetInt32(Item.columnPrice);
                    int storeID = reader.GetInt32(Item.columnStoreId);
                    Item item = new Item(id, storeID, name, category, imageBitmap, rating, price, 10, " ", itemCount);
                    items.Add(item);
                }
            }
            return items;
        }

        public List<Offer> ExecuteOffersList(string storedProcedureName, Dictionary<string, object> parameters)
        {
            List<Offer> offers = new List<Offer>();
            SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> Param in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                }
            }

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    string name = reader.GetString(Offer.columnName);
                    int oId = reader.GetInt32(Offer.columnOID);
                    int sId = reader.GetInt32(Offer.columnSID);
                    
                    string imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                    
                    var imageBitmap = loadingImages.GetImageBitmapFromUrl(imgurl);
                    int rating;
                    if (!reader.IsDBNull(Offer.columnRating))
                    {
                        rating = reader.GetInt32(Offer.columnRating);
                    }
                    else rating = 4;        // default value
                    int price = reader.GetInt32(Offer.columnPrice);
                    Controller controller = new Controller();
                    List<Item> items = controller.getItemsOfOffer(oId);
                    Offer offer = new Offer(oId, sId, name, imageBitmap, price, rating, items);
                    offers.Add(offer);
                }
            }
            return offers;
        }
        public List<Offer> ExecuteOffersofStoreList(string storedProcedureName, Dictionary<string, object> parameters)
        {
            List<Offer> offers = new List<Offer>();
            SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> Param in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                }
            }

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    string name = reader.GetString(Offer.columnName);
                    int oId = reader.GetInt32(Offer.columnOID);
                    int sId = reader.GetInt32(Offer.columnSID);
                    string imgurl;
                    
                    imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                    
                    var imageBitmap = loadingImages.GetImageBitmapFromUrl(imgurl);
                    int rating;
                    if (!reader.IsDBNull(Offer.columnRating))
                    {
                        rating = reader.GetInt32(Offer.columnRating);
                    }
                    else rating = 4;        // default value
                    int price = reader.GetInt32(Offer.columnPrice);
                    Controller controller = new Controller();
                    List<Item> items = controller.getItemsOfOffer(oId);
                    Offer offer = new Offer(oId, sId, name, imageBitmap, price, rating, items);
                    offers.Add(offer);
                }
            }
            return offers;
        }

        public Item ExecuteItem(string storedProcedureName, Dictionary<string, object> parameters)
        {
            Item item = null;
            try
            {
                SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> Param in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                    }
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        string name = reader.GetString(Item.columnName);
                        int id = reader.GetInt32(Item.columnID);
                        string imgurl;
                        if (!reader.IsDBNull(Item.columnImage))
                        {
                            imgurl = reader.GetString(Item.columnImage);
                            if (imgurl.Length < 3) imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                        }
                        else
                        {
                            imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                        }
                        var imageBitmap = loadingImages.GetImageBitmapFromUrl(imgurl);
                        int rating;
                        if (!reader.IsDBNull(Item.columnRating))
                        {
                            rating = reader.GetInt32(Item.columnRating);
                        }
                        else rating = 4;        // default value
                        string category = reader.GetString(Item.columnCategory);
                        int price = reader.GetInt32(Item.columnPrice);
                        int quantity = reader.GetInt32(Item.columnCount);
                        int storeID = reader.GetInt32(Item.columnStoreId);
                        item = new Item(id, storeID, name, category, imageBitmap, rating, price, quantity);
                    }
                }
            }
            
            catch (Exception ex)
            {
                // throw the exception
            }
            return item;
        }
        public Store ExecuteStore(string storedProcedureName, Dictionary<string, object> parameters)
        {
            Store Store = null;
            try
            {
                SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> Param in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                    }
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        string name = reader.GetString(Store.columnName);
                        int storeID = reader.GetInt32(Store.columnID);
                        string imgurl;
                        if (!reader.IsDBNull(Store.columnImage))
                        {
                            imgurl = reader.GetString(Store.columnImage);
                            if(imgurl.Length<3) imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                        }
                        else
                        {
                            imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                        }
                        var imageBitmap = loadingImages.GetImageBitmapFromUrl(imgurl);
                        int rating;
                        if (!reader.IsDBNull(Store.columnRating))
                        {
                            rating = reader.GetInt32(Store.columnRating);
                        }
                        else rating = 4;        // default value
                        Store = new Store(storeID, name, imageBitmap, rating);
                    }
                }
            }

            catch (Exception ex)
            {
                // throw the exception
            }
            return Store;
        }
        public List<string> ExecuteUserInfo (string storedProcedureName, Dictionary<string, object> parameters)
        {
            List<string> list = new List<string>();

            try
            {
                SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> Param in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                    }
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(User.columnFName));
                        list.Add(reader.GetString(User.columnMName));
                        list.Add(reader.GetString(User.columnLName));
                        list.Add(reader.GetString(User.columnEmail));
                        list.Add(reader.GetString(User.columnPhoneNum));
                        string address, currLoc, creditCardNum, creditCardCVC;
                        if (!reader.IsDBNull(User.columnAddress))
                        {
                            address = reader.GetString(User.columnAddress);
                        }
                        else address = " ";
                        if (!reader.IsDBNull(User.columnCurrentLoc))
                        {
                            currLoc = reader.GetString(User.columnCurrentLoc);
                        }
                        else currLoc = " ";

                        list.Add(address);
                        list.Add(currLoc);
                        if (!reader.IsDBNull(User.columnCreditCardNum))
                        {
                            creditCardNum = reader.GetString(User.columnCreditCardNum);
                        }
                        else creditCardNum = " ";

                        if (!reader.IsDBNull(User.columnCreditCardCVC))
                        {
                            creditCardCVC = reader.GetString(User.columnCreditCardCVC);
                        }
                        else creditCardCVC = " ";
                        list.Add(creditCardNum);
                        list.Add(creditCardCVC);
                    }
                }

            }
            catch (Exception ex)
            {
                // throw the exception   
            }
            
            return list;
        }
        public List<string> ExecuteCurrentOrder(string storedProcedureName, Dictionary<string, object> parameters)
        {
            List<string> list = new List<string>();

            try
            {
                SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> Param in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                    }
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));                   //Courier Fname
                        list.Add(reader.GetString(1));                   //Courier Lname
                        string imgurl;
                        if (!reader.IsDBNull(2))
                        {
                            imgurl = reader.GetString(2);
                            if (imgurl.Length < 3) imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                        }
                        else
                        {
                            imgurl = "https://cdn.shopify.com/s/files/1/0533/2089/files/placeholder-images-image_large.png?v=1530129081";
                        }
                        list.Add(imgurl);
                        list.Add(reader.GetString(3));
                        list.Add(reader.GetInt32(4).ToString());
                        
                    }
                }

            }
            catch (Exception ex)
            {
                // throw the exception   
            }

            return list;
        }
        public List<int> ExecuteOrder(string storedProcedureName, Dictionary<string, object> parameters)
        {
            List<int> list = new List<int>();

            try
            {
                SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> Param in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                    }
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetInt32(0));                   //StoreID
                        list.Add(reader.GetInt32(1));                   //CourierID
                    }
                }

            }
            catch (Exception ex)
            {
                // throw the exception   
            }

            return list;
        }
        public List<string> ExecuteAreasList(string storedProcedureName, Dictionary<string, object> parameters)
        {
            List<string> list = new List<string>();

            try
            {
                SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> Param in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                    }
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                }

            }
            catch (Exception ex)
            {
                // throw the exception   
            }

            return list;
        }
        public List<int> ExecuteAvailableCouriers(string storedProcedureName, Dictionary<string, object> parameters)
        {
            List<int> list = new List<int>();

            try
            {
                SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> Param in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                    }
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetInt32(0));
                    }
                }

            }
            catch (Exception ex)
            {
                // throw the exception   
            }

            return list;
        }

        public object ExecuteScalar(string storedProcedureName, Dictionary<string, object> parameters)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand(storedProcedureName, myConnection);

                myCommand.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> Param in parameters)
                    {
                        myCommand.Parameters.Add(new SqlParameter(Param.Key, Param.Value));
                    }
                }

                return myCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void CloseConnection()
        {
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
;