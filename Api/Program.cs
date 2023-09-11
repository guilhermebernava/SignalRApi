using Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//adiciona o signalR
builder.Services.AddSignalR();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Vai mapear o HUB para um "endpoint" 
app.MapHub<MyHub>("/chat");
app.UseHttpsRedirection();
app.Run();