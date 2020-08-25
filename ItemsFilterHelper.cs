using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CourierUser
{
    using Android.Runtime;
    using Android.Widget;
    using Java.Lang;
    using String = System.String;

 
    class FilterHelper : Filter
    {
        static JavaList<String> currentList;
        static ItemsListAdapter adapter;

        public static FilterHelper newInstance(JavaList<String> currentList, ItemsListAdapter adapter)
        {
            FilterHelper.adapter = adapter;
            FilterHelper.currentList = currentList;
            return new FilterHelper();
        }
        /*
        - Perform actual filtering.
         */
        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            FilterResults filterResults = new FilterResults();
            if (constraint != null && constraint.Length() > 0)
            {
                //CHANGE TO UPPER
                //constraint = constraint.ToString().ToUpper();
                string query = constraint.ToString().ToUpper();

                //HOLD FILTERS WE FIND
                JavaList<String> foundFilters = new JavaList<String>();

                //ITERATE CURRENT LIST
                for (int i = 0; i < currentList.Size(); i++)
                {
                    string galaxy = currentList[i];

                    //SEARCH
                    if (galaxy.ToUpper().Contains(query.ToString()))
                    {
                        //ADD IF FOUND
                        foundFilters.Add(galaxy);
                    }
                }
                //SET RESULTS TO FILTER LIST
                filterResults.Count = foundFilters.Size();
                filterResults.Values = foundFilters;
            }
            else
            {
                //NO ITEM FOUND.LIST REMAINS INTACT
                filterResults.Count = currentList.Size();
                filterResults.Values = currentList;
            }

            //RETURN RESULTS
            return filterResults;
        }
        /*
         * Publish results to UI.
         */
        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            //adapter.setItems((JavaList<String>)results.Values);
            adapter.NotifyDataSetChanged();
        }
    }
    
}