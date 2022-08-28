using Microsoft.Data.SqlClient;

internal class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    try 
    { 
      SqlConnectionStringBuilder dbBuilder = new SqlConnectionStringBuilder();
      dbBuilder.DataSource = builder.Configuration["DB:DataSource"];  
      dbBuilder.UserID = builder.Configuration["DB:UserId"];         
      dbBuilder.Password = builder.Configuration["DB:PW"];
      dbBuilder.InitialCatalog = builder.Configuration["DB:InitialCatalog"];

      using (SqlConnection connection = new SqlConnection(dbBuilder.ConnectionString))
      {
        String sql = "SELECT name, collation_name FROM sys.databases";

        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          connection.Open();
          using (SqlDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
                Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
            }
          }
        }                    
      }
    }
    catch (SqlException e)
    {
        Console.WriteLine(e.ToString());
    }
    Console.ReadLine();

    // Add services to the container.

    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    // app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
  }
}