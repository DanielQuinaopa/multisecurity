using multisecurity.Repositories;

namespace multisecurity.Data
{
    public class DbInitial
    {
        public static void Initialize(Contexto context)
        {
            context.Database.EnsureCreated();

            if (context.Usuario.Any())
            {
                return;
            }

            var rols = new Models.Rol[]
            {
                 new Models.Rol
                 {
                     Id= 1,
                     Description="Administrator",
                     Status = "A",
                     Add = DateTime.Now
                 },
                 new Models.Rol
                 {
                     Id= 2,
                     Description="Student",
                     Status = "A",
                     Add = DateTime.Now
                 }
            };

            foreach (var item in rols)
            {
                context.Rol.Add(item);
            }

            context.SaveChanges();

            var users = new Models.User[]
            {
                 new Models.User{
                     Id=1,
                     Name="Daniel",
                     LastName="Quinaopa",
                     Email="dq@gmail.com",
                     Password="123456",
                     Phone="2222222",
                     Estado="A",
                     Add=DateTime.Now,
                     RolId=1 },

                 new Models.User{
                     Id=2,
                     Name="Juan",
                     LastName="Perez",
                     Email="jperez@gmail.com",
                     Password="123456",
                     Phone="2222222",
                     Estado="A",
                     Add=DateTime.Now,
                     RolId=2 },

            };

            foreach (var item in users)
            {
                context.Usuario.Add(item);
            }

            context.SaveChanges();

        }

    }

}
