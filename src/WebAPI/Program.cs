using Application.Extensions;
using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Domain.Repositories;
using VIAEventAssociation.Core.QueryContracts.Contract;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper.Implementations;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using VIAEventAssociation.Infrastructure.EfcDmPersistence;
using VIAEventAssociation.Infrastructure.EfcDmPersistence.Repositories;
using VIAEventAssociation.Infrastructure.EfcQueries.Queries;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;
using ViaEventAssociation.Presentation.WebAPI.MappingConfigurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.RegisterCommandHandlers();
builder.Services.RegisterCommandDispatcher();

//TODO: Find out where to move this logic
builder.Services.AddDbContext<DbContext, EfcDbContext>(optionsBuilder =>
    optionsBuilder.UseSqlite(@"Data Source = ../Infrastructure/EfcDmPersistence/VEADatabase.db"));
builder.Services.AddScoped<IUnitOfWork, EfcUnitOfWork>();
builder.Services.AddScoped<IEventRepository, EfcEventRepository>();
builder.Services.AddScoped<IUserRepository, EfcUserRepository>();

//TODO: Find out where to move this logic
builder.Services.AddScoped<IMapper, ObjectMapper>();
builder.Services.AddScoped<IMappingConfig<EventEditingOverview.Answer, EventEditingOverviewResponse>, EventEditingOverviewResponseMapper>();
builder.Services.AddScoped<IMappingConfig<ProfilePageRequest, UserProfilePage.Query>, ProfilePageQueryMapper>();
builder.Services.AddScoped<IMappingConfig<UserProfilePage.Answer, ProfilePageResponse>, ProfilePageResponseMapper>();
builder.Services.AddScoped<IMappingConfig<UpcomingEventsPageRequest, UpcomingEventsPage.Query>, UpcomingEventsPageQueryMapper>();
builder.Services.AddScoped<IMappingConfig<UpcomingEventsPage.Answer, UpcomingEventsPageResponse>, UpcomingEventsPageResponseMapper>();
builder.Services.AddScoped<IMappingConfig<ViewSingleEventRequest, ViewSingleEvent.Query>, ViewSingleEventQueryMapper>();
builder.Services.AddScoped<IMappingConfig<ViewSingleEvent.Answer, ViewSingleEventResponse>, ViewSingleEventResponseMapper>();

//TODO: Find out where to move this logic
builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
builder.Services
    .AddScoped<IQueryHandler<EventEditingOverview.Query, EventEditingOverview.Answer>,
        EventEditingOverviewQueryHandler>();
builder.Services.AddScoped<IQueryHandler<UserProfilePage.Query, UserProfilePage.Answer>, ProfilePageQueryHandler>();
builder.Services
    .AddScoped<IQueryHandler<UpcomingEventsPage.Query, UpcomingEventsPage.Answer>, UpcomingEventsPageQueryHandler>();
builder.Services.AddScoped<IQueryHandler<ViewSingleEvent.Query, ViewSingleEvent.Answer>, ViewSingleEventQueryHandler>();




var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
