namespace DataBase
{
    using Domain;
    using System;
    using System.Data.Entity;
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
            : base("name=HeroAndQuestions")
        {
         //   Database.SetInitializer<Model1>(null);
        }
        public virtual DbSet<Heroes> heroes { get; set; }
        public virtual DbSet<Questions> qestions { get; set; }
        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}