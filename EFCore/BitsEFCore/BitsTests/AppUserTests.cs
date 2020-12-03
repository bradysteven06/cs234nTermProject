using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using BitsEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace BitsTests
{
    public class AppUserTests
    {

        BitsContext dbContext;
        AppUser appU;
        List<AppUser> appUList;

        // finish writing reset stored procedure for appConfig and appUsers 
        [SetUp]
        public void Setup()
        {
            dbContext = new BitsContext();
            
        }

        [Test]
        public void GetAllTest()
        {
            appUList = dbContext.AppUser.OrderBy(appU => appU.AppUserId).ToList();
            Assert.AreEqual(2, appUList.Count);
            Assert.AreEqual("Chuck", appUList[0].Name);
            PrintAll(appUList);
        }

        [Test]
        public void GetPrimaryKeyTest()
        {
            appU = dbContext.AppUser.Find(1);
            Assert.IsNotNull(appU);
            Assert.AreEqual("Chuck", appU.Name);
        }

        [Test]
        public void GetUsingWhere()
        {
            appUList = dbContext.AppUser.Where(appU => appU.Name.StartsWith("C")).OrderBy(appU => appU.Name).ToList();
            Assert.AreEqual(1, appUList.Count);
            Assert.AreEqual("Chuck", appUList[0].Name);
            PrintAll(appUList);
        }

        //create
        [Test]
        public void CreateTest()
        {
            appU = new AppUser();
            appU.AppUserId = 3;
            appU.Name = "Gabe";
            dbContext.AppUser.Add(appU);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.AppUser.Find(3));

        }
        //update
        [Test]
        public void UpdateTest()
        {
            appU = dbContext.AppUser.Find(3);
            appU.Name = "Gabriel";
            dbContext.AppUser.Update(appU);
            dbContext.SaveChanges();
            appU = dbContext.AppUser.Find(3);
            Assert.AreEqual("Gabriel", appU.Name);
        }

        //delete
        [Test]
        public void DeleteTest()
        {
            appU = dbContext.AppUser.Find(3);
            dbContext.AppUser.Remove(appU);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.AppUser.Find(3));
        }

        public void PrintAll(List<AppUser> appUList)
        {
            foreach (AppUser appU in appUList)
            {
                Console.WriteLine(appU);
            }
        }
    }
}
