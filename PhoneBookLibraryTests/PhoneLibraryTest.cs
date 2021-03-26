using NUnit.Framework;
using PhoneBookLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookLibraryTests
{
    class PhoneLibraryTest
    {
        [SetUp]
        public void Setup()
        {
        }



        [Test]
        public void EnsureLoadFile()
        {
            var library = new PhoneLibrary();
            Assert.IsNotNull(library.Entries);
        }


        [Test]
        public void SaveFileTest()
        {
            var library = new PhoneLibrary();
            var result = library.SaveChanges();
            Assert.AreEqual(result, "Successful");
        }


        [Test]
        public void AddEntryTest()
        {
            var library = new PhoneLibrary();
            var entry = new Entry { FirstName = "Armend", LastName = "Imeri", Number = "070123456", Type = "Cellphone" };
            var result = library.AddEntry(entry);
            library.DeleteEntry(entry.ID);
            Assert.AreEqual(result, "Successful");
        }

        [Test]
        public void DeleteEntryTest()
        {
            var library = new PhoneLibrary();
            var entry = new Entry { FirstName = "Armend", LastName = "Imeri", Number = "070123456", Type = "Cellphone" };
            library.AddEntry(entry);
            var result = library.DeleteEntry(entry.ID);
            Assert.AreEqual(result, "Successful");
        }


        public void EditEntryTest()
        {
            var library = new PhoneLibrary();
            var entry = new Entry { FirstName = "Armend", LastName = "Imeri", Number = "070123456", Type = "Cellphone" };
            library.AddEntry(entry);
            entry.FirstName = "Imeri";
            entry.LastName = "Armend";
            var result = library.Edit(entry);
            Assert.AreEqual(result, "Entry edited");
        }


    }
}
