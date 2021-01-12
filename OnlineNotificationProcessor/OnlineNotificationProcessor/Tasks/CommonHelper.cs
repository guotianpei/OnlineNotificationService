using System;
using System.Collections.Generic;
using ONP.Domain.Models;
using System.Text;
using ONP.BackendProcessor.Models;
using System.Reflection;

namespace ONP.BackendProcessor.Tasks
{
    public class CommonHelper
    {
        public List<PlaceHolder> AddPlaceHolder(EntityProfile entProf)
        {
            List<PlaceHolder> lstPH = new List<PlaceHolder>();
            PlaceHolder _pholder;
            foreach (PropertyInfo prop in entProf.GetType().GetProperties())
            {
                _pholder = new PlaceHolder(string.Format("${0}$", prop.Name), prop.GetValue(entProf).ToString());
                lstPH.Add(_pholder);
            }
            return lstPH;
        }


    }
}
