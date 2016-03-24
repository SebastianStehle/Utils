// ==========================================================================
// ValueChangeEventArgsTest.cs
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
    public class ValueChangeEventArgsTest
    {
        [Fact]
        public void Constructor_OwnerIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new ValueChangedEventArgs<object, int>(null, 1, 2));
        }

        [Fact]
        public void Constructor_ValidArguments_PassedToProperties()
        {
            int oldValue = 123;
            int newValue = 456;
            object owner = string.Empty;

            ValueChangedEventArgs<object, int> args = new ValueChangedEventArgs<object, int>(owner, oldValue, newValue);

            Assert.Equal(owner, args.Owner);
            Assert.Equal(oldValue, args.OldValue);
            Assert.Equal(newValue, args.NewValue);
        }
    }
}
