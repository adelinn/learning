using dotNetLearn.Services;
using dotNetLearn.Models;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public class RandTest
{
    [TestMethod]
    public void TestMethod1()
    {
        DBConnect.connect(new DbContextOptionsBuilder());
        RandDBContext db = new RandDBContext(DBConnect.opts);
        RandService testObj = new RandService(db);
        //testObj.Add();
        //testObj.Add();
        List<RandRecord> toTest = testObj.GetAll();
        Assert.AreNotEqual(toTest.Last().Numbers[0], toTest.Last().Numbers[1], "Error - Generated numbers are not random within the array.");
        Assert.AreNotEqual(toTest.Last().Numbers[0], toTest[toTest.Count-2].Numbers[0], "Error - Generated numbers are not random across runs.");
    }
}