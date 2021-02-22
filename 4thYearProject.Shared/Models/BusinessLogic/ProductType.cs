using System;
using System.Collections.Generic;
using System.Text;

namespace _4thYearProject.Shared.Models.BusinessLogic
{
   abstract class ProductType
    {

        public virtual double Price()
        {
            return 0;
        }

    }


    class License : ProductType
    {
        


    }

    class Print : ProductType
    {
        //SIZES


    }

    class Shirt : ProductType
    {
        //SIZES


    }


}
