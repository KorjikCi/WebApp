using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated(); // создаем базу данных при первом обращении
    }

    // Нам надо инициализировать свойство типа DbSet - инициализировать свойство значением типа Set<T>
    public DbSet<Note> Notes => Set<Note>();
}