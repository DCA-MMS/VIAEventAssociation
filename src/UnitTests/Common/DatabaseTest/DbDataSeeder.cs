﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Infrastructure.EfcQueries;

namespace Tests.Common.DBContext;

public class DbDataSeeder
{
    [Test]
    public async Task Test()
    {
        await using var context = SetUpReadContext().Seed();
        Assert.IsNotEmpty(context.Users);
    }

    public static VeadatabaseContext SetUpReadContext()
    {
        var testDbName = "Test.db";
        DbContextOptionsBuilder<VeadatabaseContext> optionsBuilder = new();
        optionsBuilder.UseSqlite($"Data Source = {testDbName}");
        VeadatabaseContext context = new(optionsBuilder.Options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }
}