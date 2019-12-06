using ConsoleEF.Context.EntityMap;
using ConsoleEF.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEF.Context
{
    public class TestContext :DbContext
    {
        public TestContext() : base("name=FirstCodeFirstApp")
        {
            
        }
        public DbSet<Donator> Donator { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Company> Company { get; set; }

        //使用fluent API的一个重要决定因素是我们是否使用了外部的POCO类，即实体模型类是否来自一个类库。
        //我们无法修改类库中类的定义，所以不能通过数据注解来提供映射细节。这种情况，我们必须使用fluent API。
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //<1>
            //modelBuilder.Entity<Donator>().ToTable("Donator").HasKey(p => p.DonatorId);//映射到表Donators,DonatorId当作主键对待
            //modelBuilder.Entity<Donator>().Property(p => p.DonatorId).HasColumnName("ID");//映射到数据表中的主键名为Id而不是DonatorId
            //modelBuilder.Entity<Donator>().Property(m => m.Name)
            //   .IsRequired()//设置Name是必须的，即不为null,默认是可为null的
            //   .IsUnicode()//设置Name列为Unicode字符，实际上默认就是unicode,所以该方法可不写
            //   .HasMaxLength(10);//最大长度为10

            //<2>
            modelBuilder.Configurations.Add(new DonatorMap());
            modelBuilder.Configurations.Add(new PersonMap());

            
            base.OnModelCreating(modelBuilder);
        }
    }
}
