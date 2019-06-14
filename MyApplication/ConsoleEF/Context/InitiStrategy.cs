using ConsoleEF.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEF.Context
{
    public class InitiStrategy : DropCreateDatabaseAlways<TestContext>  //DropCreateDatabaseAlways<TestContext> //DropCreateDatabaseIfModelChanges<TestContext>
    {
        protected override void Seed(TestContext context)
        {
            var list = new List<Donator>()
                {
                    new Donator{ Name="one", Amount =100, DonateDate = DateTime.Now},
                    new Donator{ Name ="second", Amount = 100,DonateDate = DateTime.Now}
                };
            context.Donator.AddRange(list);
            base.Seed(context);
        }
    }
}
