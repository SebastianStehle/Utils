// ==========================================================================
// FileExtensionTests.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using GP.Utils;
using Xunit;

// ReSharper disable ConvertToConstant.Local

namespace Tests.Facts
{
    public class FileExtensionTests
    {
        [Theory]
        [InlineData(null, "MIME")]
        [InlineData("EXT", null)]
        public void Constructor_ExtensionOrMimeTypeIsNull_ThrowsException(string extension, string mimeType)
        {
            Assert.Throws<ArgumentNullException>(() => new FileExtension(extension, mimeType));
        }

        [Theory]
        [InlineData("", "MIME")]
        [InlineData(" ", "MIME")]
        [InlineData("EXT", "")]
        [InlineData("EXT", " ")]
        public void Constructor_ExtensionOrMimeTypeIsEmpty_ThrowsException(string extension, string mimeType)
        {
            Assert.Throws<ArgumentException>(() => new FileExtension(extension, mimeType));
        }

        [Fact]
        public void Constructor_ValidParameters_PassedToProperties()
        {
            string extension = "EXT";
            string mimeType = "MIME";

            FileExtension fileExtension = new FileExtension(extension, mimeType);

            Assert.Equal(extension, fileExtension.Extension);
            Assert.Equal(mimeType, fileExtension.MimeType);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(".ext2")]
        public void Matching_InvalidInput_ReturnFalse(string fileName)
        {
            string extension = ".ext";
            string mimeType = "invalid/mime";

            FileExtension fileExtension = new FileExtension(extension, mimeType);

            Assert.False(fileExtension.MatchsFileName(fileName));
        }

        [Fact]
        public void Matching_SameExtension_ReturnsTrue()
        {
            string extension = ".ext";
            string mimeType = "invalid/mime";

            FileExtension fileExtension = new FileExtension(extension, mimeType);

            Assert.True(fileExtension.MatchsFileName("MyFile.ext"));
            Assert.True(fileExtension.MatchsFileName("MyFile.EXT"));
        }
    }
}
