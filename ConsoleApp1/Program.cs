using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SouthernTreasuresDAL;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DALContext ctx = new DALContext())
            {
                ctx.ConnectionString = @"Data Source=GDC-BC-13\SQLEXPRESS;Initial Catalog=SouthernTreasures;Integrated Security=True";
                ctx.ReadSpecificOffer(8);
                //var x = ctx.ReadAllUsers();
                //foreach (var user in x)
                //{
                //   Console.WriteLine(user);
                //}
            }
        }

    }
}
