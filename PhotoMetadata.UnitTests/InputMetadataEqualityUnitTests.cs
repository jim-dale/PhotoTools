namespace PhotoMetadata.UnitTests;

using System.Linq;
using AutoBogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMetadata.Models;

[TestClass]
public class InputMetadataEqualityUnitTests
{
    [TestMethod]
    public void Equals_SamePropertyValues_ReturnsTrue()
    {
        var expected = AutoFaker.Generate<InputMetadata>();
        var actual = new InputMetadata(expected);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Equals_DifferentPropertyValues_ReturnsFalse()
    {
        var expected = AutoFaker.Generate<InputMetadata>();
        expected.Comments = "actual";

        var actual = new InputMetadata(expected)
        {
            Comments = "expected"
        };

        Assert.AreNotEqual(expected, actual);
    }

    [TestMethod]
    public void Equals_SameArray_ReturnsTrue()
    {
        var expected = AutoFaker.Generate<InputMetadata>(3);
        var actual = expected.Select(i => new InputMetadata(i)).ToList();

        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LeftOuterJoin_WithChangedProperty_ReturnsDifferences()
    {
        var previous = AutoFaker.Generate<InputMetadata>(3);
        var expected = previous.Select(i =>
        {
            // Make the returned object (hopefully) different to the source object
            return new InputMetadata(i)
            {
                Comments = i.Comments.ToUpperInvariant()
            };
        }).ToList();

        var actual = Helpers.GetNewAndUpdatedMetadata(expected, previous);

        // Every item should be different so the 
        Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
    }

    [TestMethod]
    public void LeftOuterJoin_WithAdditions_ReturnsDifferences()
    {
        var previous = AutoFaker.Generate<InputMetadata>(3);
        var current = previous.Select(i => new InputMetadata(i)).ToList();
        var expected = AutoFaker.Generate<InputMetadata>(2);
        current.AddRange(expected);

        var actual = Helpers.GetNewAndUpdatedMetadata(current, previous);

        Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
    }
}
