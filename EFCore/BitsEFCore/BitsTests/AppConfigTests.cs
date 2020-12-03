using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using BitsEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace BitsTests
{
    [TestFixture]
    public class AppConfigTests
    {

        BitsContext dbContext;
        AppConfig appC;
        List<AppConfig> appCList;

        // finish writing reset stored procedure for appConfig and appUsers 
        [SetUp]
        public void Setup()
        {
            dbContext = new BitsContext();
        }

        [Test]
        public void GetAllTest()
        {
            appCList = dbContext.AppConfig.OrderBy(appC => appC.BreweryName).ToList();
            Assert.AreEqual(1, appCList.Count);
            Assert.AreEqual("Manifest", appCList[0].BreweryName);
            PrintAll(appCList);
        }

        [Test]
        public void GetPrimaryKeyTest()
        {
            appC = dbContext.AppConfig.Find(1);
            Assert.IsNotNull(appC);
            Assert.AreEqual("Manifest", appC.BreweryName);
        }

        [Test]
        public void GetUsingWhere()
        {
            appCList = dbContext.AppConfig.Where(appC => appC.BreweryName.StartsWith("M")).ToList();
            Assert.AreEqual(1, appCList.Count);
            Assert.AreEqual("Manifest", appCList[0].BreweryName);
            PrintAll(appCList);
        }

        //delete
        [Test]
        public void DeleteTest()
        {
            appC = dbContext.AppConfig.Find(2);
            dbContext.AppConfig.Remove(appC);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.AppConfig.Find(2));
        }

        //create
        [Test]
        public void CreateTest()
        {
            appC = new AppConfig();
            appC.BreweryId = 2;
            appC.BreweryName = "Puppers";
            appC.Color1 = "b3a2ad";
            appC.Color2 = "2e2e2e";
            dbContext.AppConfig.Add(appC);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.AppConfig.Find(2));
        }
        //update
        [Test]
        public void UpdateTest()
        {
            appC = dbContext.AppConfig.Find(2);
            appC.BreweryName = "Xengis";
            dbContext.AppConfig.Update(appC);
            appC = dbContext.AppConfig.Find(2);
            Assert.AreEqual("Xengis", appC.BreweryName);
        }

        public void PrintAll(List<AppConfig> appCList)
        {
            foreach (AppConfig appC in appCList)
            {
                Console.WriteLine(appC);
            }
        }

    }
}
