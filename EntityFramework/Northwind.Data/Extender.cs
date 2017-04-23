using System;

namespace Northwind.Data
{
    public partial class Customer
    {
        public override string ToString()
        {
            return string.Format("Customer ID: {0}\nContact name: {1}\nCompany Name: {2}\nCity: {3}",
                this.CustomerID, this.ContactName, this.CompanyName, this.City);
        }
    }
}