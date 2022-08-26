using Microsoft.Data.SqlClient;

internal class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();

    SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder();
    connectionBuilder.DataSource = "<your_server.database.windows.net>"; 
    connectionBuilder.UserID = "<your_username>";            
    connectionBuilder.Password = "<your_password>";     
    connectionBuilder.InitialCatalog = "<your_database>";
    // OR 
    connectionBuilder.ConnectionString="<your_ado_net_connection_string>";

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