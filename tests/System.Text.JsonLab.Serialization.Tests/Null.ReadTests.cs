﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace System.Text.Json.Serialization.Tests
{
    public partial class NullTests
    {
        [Fact]
        public static void ClassWithNull()
        {
            TestClassWithNull obj = JsonSerializer.Parse<TestClassWithNull>(TestClassWithNull.s_json);
            obj.Verify();
        }

        [Fact]
        public static void DefaultReadValue()
        {
            TestClassWithNullButInitialized obj = JsonSerializer.Parse<TestClassWithNullButInitialized>(TestClassWithNullButInitialized.s_json);
            Assert.Equal(null, obj.MyString);
            Assert.Equal(null, obj.MyInt);
        }

        [Fact]
        public static void OverrideReadOnOption()
        {
            var options = new JsonSerializerOptions();
            options.IgnoreNullPropertyValueOnRead = true;

            TestClassWithNullButInitialized obj = JsonSerializer.Parse<TestClassWithNullButInitialized>(TestClassWithNullButInitialized.s_json, options);
            Assert.Equal("Hello", obj.MyString);
            Assert.Equal(1, obj.MyInt);
        }

        [Fact]
        public static void OverrideReadOnAttribute()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            JsonPropertyValueAttribute attr = new JsonPropertyValueAttribute();
            attr.IgnoreNullValueOnRead = true;
            options.AddAttribute(typeof(TestClassWithNullButInitialized), attr);

            TestClassWithNullButInitialized obj = JsonSerializer.Parse<TestClassWithNullButInitialized>(TestClassWithNullButInitialized.s_json, options);
            Assert.Equal("Hello", obj.MyString);
            Assert.Equal(1, obj.MyInt);
        }
    }
}
