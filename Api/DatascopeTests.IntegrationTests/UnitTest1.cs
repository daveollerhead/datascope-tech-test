using System;
using System.Transactions;
using DatascopeTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace DatascopeTests.IntegrationTests
{
    public static class DbContextBuilder
    {
        public static AppDbContext Build()
        {
            var config = Configuration.Get();
            var connectionString = config.GetConnectionString("DatascopeTestIntegrationTests");

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new AppDbContext(options);
        }
    }

    //[SetUpFixture]
    //public class GlobalSetup
    //{
    //    [OneTimeSetUp]
    //    public void Setup()
    //    {
    //        var context = DbContextBuilder.Build();
    //        context.Database.Migrate();
    //    }
    //}

    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;
        public void BeforeTest(ITest test)
        {
            _transactionScope = new TransactionScope();
        }

        public void AfterTest(ITest test)
        {
            _transactionScope.Dispose();
        }

        public ActionTargets Targets => ActionTargets.Test;
    }
}