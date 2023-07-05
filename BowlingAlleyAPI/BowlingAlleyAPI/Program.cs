using BowlingAlleyAPI.Data;
using BowlingAlleyAPI.DTO;
using BowlingAlleyAPI.Models;
using BowlingAlleyAPI.Services.Alleys;
using BowlingAlleyAPI.Services.Reservations;
using BowlingAlleyAPI.Services.Users;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader()
                            .AllowAnyMethod(); ;
                      });
});



builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IAlleyService, AlleyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/alleys/{startDate}/{endDate}", async (IAlleyService alleyService, DateTime startDate, DateTime endDate) => await alleyService.GetAvailableAlleys(startDate, endDate));

app.MapGet("/alleys/", async (IAlleyService alleyService) => await alleyService.GetAllAlleys());

app.MapGet("/reservations", async (IReservationService reservationService) => await reservationService.GetAllReservations());

app.MapPost("/reservation/", async (IReservationService reservationService, Reservation reservation) => await reservationService.AddReservation(reservation));

app.MapPost("/login", async (IUserService userService, UserDTO user) => await userService.Login(user.Email, user.Password));

app.MapGet("/reservations/{userId}", async (IReservationService reservationService, Guid userId) => await reservationService.GetReservationsByUserId(userId));

app.Run();