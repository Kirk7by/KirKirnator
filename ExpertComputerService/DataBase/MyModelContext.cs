namespace DataBase
{
    using Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    public class MyModelContext : DbContext
    {
        Type _Hack = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        
        // Контекст настроен для использования строки подключения "Model1" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "DataBase.Model1" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "Model1" 
        // в файле конфигурации приложения.
        public MyModelContext()
            : base("data source = "+Configurate.ExpConfig.Default.ConnectionString)
        {
         //   Database.SetInitializer<Model1>(null);
        }
        public virtual DbSet<Heroes> heroes { get; set; }
        public virtual DbSet<Questions> qestions { get; set; }
        /*
        public virtual int HR_UPDHEROES(string OldHeroName1, string NewHeroName1)
        {
            var OldHeroName = new ObjectParameter("OldHeroName", OldHeroName1);
            var NewHeroName = new ObjectParameter("NewHeroName", NewHeroName1);

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("HR_UPDHEROES", OldHeroName, NewHeroName);
        }
        */ //то что не пригодилось
    }
}