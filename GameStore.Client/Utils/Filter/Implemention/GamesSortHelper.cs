using GameStore.Client.Utils.Filter.Abstraction;
using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GameStore.Client.Utils.Filter.Implemention
{
    public class GamesSortHelper : ISortHelper<Game>
    {

        public string DESC { get => "desc"; }
        public string ASC { get => "asc"; }



        public Comparison<Game> GetComparison(string keyWord)
        {
            try
            {
                var propName = keyWord.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                var order = keyWord.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
                PropertyInfo prop = null;
                if ((prop = typeof(Game).GetProperties().First(x => x.Name.ToLower().Equals(propName.ToLower()))) != null)
                {
                    var comp = new Comparison<Game>((a, b) =>
                    {
                        var result = (int)prop.PropertyType.GetMethods().First(x => x.Name == "CompareTo")?.Invoke(prop.GetValue(a), new object[] { prop.GetValue(b) });

                        if (order == DESC)
                            result *= -1;
                        return result;
                    });
                    return comp;
                }

                //Comparison<Game> comp = null;
                //switch (propName) {
                //    case "name":
                //        comp = new Comparison<Game>((a, b) => {
                //            int result = a.Name.CompareTo(b.Name);
                //            if (order == DESC)
                //                result *= -1;
                //            return result;
                //        });
                //        break;
                //    case "price":
                //        comp = new Comparison<Game>((a, b) => { 
                //            int result = a.Price.CompareTo(b.Price);
                //            if (order == DESC)
                //                result *= -1;
                //            return result;
                //        });
                //        break;
                //    case "year":
                //        comp = new Comparison<Game>((a, b) => { 
                //            int result = a.Year.CompareTo(b.Year);
                //            if (order == DESC)
                //                result *= -1;
                //            return result;
                //        });
                //        break;
                //    case "id":
                //        comp = new Comparison<Game>((a, b) => { 
                //            int result = a.Id.CompareTo(b.Id);
                //            if (order == DESC)
                //                result *= -1;
                //            return result;
                //        });
                //        break;
                //    default:
                //        throw new ArgumentException("incorrect sort keyword parameter");
                //        break;
                //}
                //return comp;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
    }
}