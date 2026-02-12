using Asrfly.Core;
using Asrfly.Data;
using Asrfly.Data.SqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asrfly.Tests
{
    [TestClass]
    public class CategoriesEntityTests
    {
        IDataHelper<Categories> dataHelper;
        public CategoriesEntityTests()
        {
            dataHelper = new CategoriesEntity();
        }

        [TestMethod]
        public void AddTest()
        {
            Categories categories = new Categories
            {
                Name = "تحليل المشروع",
                Details = "الصنف الخاص في عملية تحليل المشروع",
                Type = "صرف",
                Balance = 1000,
                AddedDate = DateTime.Now,
            };

            int act = dataHelper.Add(categories);
            int expt = 1;
            Assert.AreEqual(expt, act);
        }

        [TestMethod]
        public void EditTest()
        {
            Categories categories = new Categories
            {
                Id = 1,
                Name = "تصميم المشروع",
                Details = "الصنف الخاص في عملية تصميم المشروع",
                Type = "صرف",
                Balance = 2000,
                AddedDate = DateTime.Now,
            };

            int act = dataHelper.Edit(categories);
            int expt = 1;
            Assert.AreEqual(expt, act);
        }

        [TestMethod]
        public void GetAllDataTest()
        {


            var act = dataHelper.GetAllData();
            Assert.IsNotNull(act);
        }
        [TestMethod]
        public void SearchTest()
        {
            var searchitem = "صرف";

            var act = dataHelper.Search(searchitem);
            Assert.IsNotNull(act);
        }

        [TestMethod]

        public void FindTest()
        {
            var Id = 1;

            var act = dataHelper.Find(1);
            Assert.IsNotNull(act);
        }
        [TestMethod]

        public void DeleteTest()
        {
            var Id = 1;

            var act = dataHelper.Delete(1);
            Assert.AreEqual(1, act);
        }
    }
}
