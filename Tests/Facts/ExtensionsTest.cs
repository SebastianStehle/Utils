// ==========================================================================
// ExtensionsTest.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GP.Utils;
using Xunit;

// ReSharper disable InvokeAsExtensionMethod

namespace Tests.Facts
{
    public class ExtensionsTest
    {
        [Fact]
        public void Forget_DoesNothing()
        {
            Extensions.Forget(new Task(() => { }));
        }

        [Fact]
        public void AddAndReturn_AddedAndReturned()
        {
            IList<int> list = new List<int> { 1, 5, 7, 9 };

            Assert.Equal(11, list.AddAndReturn(11));
            Assert.Equal(11, list.Last());
        }

        [Fact]
        public void WriteFromStream_InputIsNull_ThrowsException()
        {
            MemoryStream target = new MemoryStream();

            Assert.Throws<ArgumentNullException>(() => target.WriteFrom(null));
        }

        [Fact]
        public void WriteFromStream_ValidInput_CopiesDataCorrectly()
        {
            byte[] buffer = { 1, 5, 6, 7 };

            MemoryStream source = new MemoryStream(buffer);
            MemoryStream target = new MemoryStream();

            Extensions.WriteFrom(target, source);

            Assert.Equal(buffer, target.ToArray());
        }

        [Fact]
        public async Task ToMemoryStreamAsync_CopiedCorrectly()
        {
            byte[] buffer = { 1, 5, 6, 7 };

            MemoryStream input = new MemoryStream(buffer);
            MemoryStream result = await Extensions.ToMemoryStreamAsync(input);

            Assert.Equal(buffer, result.ToArray());
            Assert.NotEqual(result, input);
        }

        [Fact]
        public void IsBase64Encoded_Valid_ReturnsTrue()
        {
            Assert.True(Extensions.IsBase64Encoded(Convert.ToBase64String(new byte[4])));
        }

        [Fact]
        public void IsBase64Encoded_NotValid_ReturnsFalse()
        {
            Assert.False(Extensions.IsBase64Encoded("%INVALID%"));
        }

        [Fact]
        public void GetOrCreateDefault_Found_ReturnsValue()
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int> { { 1, 5 } };

            Assert.Equal(5, dictionary.GetOrAddDefault(1));
        }

        [Fact]
        public void GetOrCreateDefault_NotFound_ReturnsDefault()
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();

            Assert.Equal(0, dictionary.GetOrAddDefault(1));
        }

        [Fact]
        public void IndexOf_ReturnsIndex()
        {
            IReadOnlyList<int> list = new List<int> { 1, 5, 7, 9 };

            Assert.Equal(2, list.IndexOf(7));
        }

        [Fact]
        public void IndexOf_NotFound_ReturnsMinusOne()
        {
            IReadOnlyList<int> list = new List<int> { 1, 5, 7, 9 };

            Assert.Equal(-1, list.IndexOf(17));
        }

        [Fact]
        public void AddRange_Added()
        {
            var itemsToAdd = new[] { 1, 3, 5 };

            ObservableCollection<int> list = new ObservableCollection<int>();

            list.AddRange(itemsToAdd);

            Assert.Equal(itemsToAdd, list);
        }

        [Fact]
        public void Foreach_Called()
        {
            var items = new[] { 1, 3, 5 };

            List<int> itemsHandled = new List<int>();

            items.Foreach(x => itemsHandled.Add(x));

            Assert.Equal(itemsHandled, items);
        }

        [Fact]
        public void IsBetween_DifferentCases()
        {
            Assert.True(Extensions.IsBetween("B", "A", "C"));
            Assert.True(Extensions.IsBetween(2, 1, 3));

            Assert.False(Extensions.IsBetween("A", "B", "C"));
            Assert.False(Extensions.IsBetween(1, 2, 3));
        }

        [Fact]
        public void IsEnumValue_DifferentCases()
        {
            Assert.True(DateTimeKind.Local.IsEnumValue());
            Assert.False(((DateTimeKind)13).IsEnumValue());
            Assert.False(123.IsEnumValue());
        }

        [Fact]
        public void SeparateByUpperLetters_Null_ReturnsNull()
        {
            Assert.Null(Extensions.SeparateByUpperLetters(null));
        }

        [Fact]
        public void SeparateByUpperLetters_Empty_ReturnsEmpty()
        {
            Assert.Equal(string.Empty, Extensions.SeparateByUpperLetters(string.Empty));
        }

        [Fact]
        public void SeparateByUpperLetters_SimpleStrings()
        {
            Assert.Equal("Hello", Extensions.SeparateByUpperLetters("Hello"));
            Assert.Equal("Hello World", Extensions.SeparateByUpperLetters("HelloWorld"));
            Assert.Equal("Hello My World", Extensions.SeparateByUpperLetters("HelloMyWorld"));
        }

        [Fact]
        public void SeparateByUpperLetters_ComplexStrings()
        {
            Assert.Equal("GPS", Extensions.SeparateByUpperLetters("GPS"));
            Assert.Equal("GPS Solution", Extensions.SeparateByUpperLetters("GPSSolution"));
            Assert.Equal("GPS Solution V2", Extensions.SeparateByUpperLetters("GPSSolutionV2"));
            Assert.Equal("GPS Solution EXT", Extensions.SeparateByUpperLetters("GPSSolutionEXT"));
        }

        [Fact]
        public void IsValidFileName_VariousCases()
        {
            Assert.True(Extensions.IsValidFileName("File"));
            Assert.True(Extensions.IsValidFileName("File_With.Extension"));
            Assert.False(Extensions.IsValidFileName("Path\\Not\\Valid"));
        }

        [Fact]
        public void GetOrAddDefault_FunctionIsNull_ThrowsException()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            Assert.Throws<ArgumentNullException>(() => dictionary.GetOrAddDefault("Key", null));
        }

        [Fact]
        public void GetOrDefault_FunctionIsNull_ThrowsException()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            Assert.Throws<ArgumentNullException>(() => dictionary.GetOrDefault("Key", null));
        }

        [Fact]
        public void GetOrAddDefault_VariouseCases()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int> { { "Key1", 1 } };

            Assert.Equal(1, dictionary.GetOrAddDefault("Key1"));
            Assert.Equal(1, dictionary["Key1"]);

            Assert.Equal(0, dictionary.GetOrAddDefault("Key2"));
            Assert.Equal(0, dictionary["Key2"]);

            Assert.Equal(3, dictionary.GetOrAddDefault("Key3", x => 3));
            Assert.Equal(3, dictionary["Key3"]);
        }

        [Fact]
        public void GetOrDefault_VariouseCases()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int> { { "Key1", 1 } };

            Assert.Equal(1, dictionary.GetOrDefault("Key1"));

            Assert.Equal(0, dictionary.GetOrDefault("Key2"));
            Assert.False(dictionary.ContainsKey("Key2"));

            Assert.Equal(3, dictionary.GetOrDefault("Key3", x => 3));
            Assert.False(dictionary.ContainsKey("Key3"));
        }

        [Fact]
        public void TryGetRemoveValue_KeyNotExists_ReturnFalse()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            int temp;

            Assert.False(dictionary.TryGetRemoveValue("KEY", out temp));
            Assert.Equal(0, temp);
        }

        [Fact]
        public void TryGetRemoveValue_KeyExists_ReturnTrueAndValue()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int> { { "KEY", 123 } };

            int temp;

            Assert.True(dictionary.TryGetRemoveValue("KEY", out temp));
            Assert.Equal(123, temp);
        }

        [Fact]
        public void SingleOrNull_Nullables()
        {
            Assert.Null(new int?[] { 4, 2 }.SingleOrNull());
            Assert.Null(new int?[] { null, 1, 3 }.SingleOrNull());
            Assert.Null(new int?[] { null }.SingleOrNull());

            Assert.Equal(4, new int?[] { 4 }.SingleOrNull());
        }

        [Fact]
        public void SingleOrNull_Classes()
        {
            Assert.Null(new string[] { null }.SingleOrNull());

            Assert.Null(new[] { "A", "B" }.SingleOrNull());

            Assert.Equal("A", new[] { "A" }.SingleOrNull());
        }
    }
}
