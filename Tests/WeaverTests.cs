﻿namespace Tests
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using Xunit;
    using Xunit.Abstractions;

    public class WeaverTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private readonly WeaverHelper _weaverHelper = WeaverHelper.Create();

        public WeaverTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void OutputWeaverErrors()
        {
            foreach (var message in _weaverHelper.Errors)
            {
                _testOutputHelper.WriteLine(message);
            }

            Assert.Equal(12, _weaverHelper.Errors.Count());
        }

        [Fact]
        public void OutputWeaverMessages()
        {
            foreach (var message in _weaverHelper.Messages)
            {
                _testOutputHelper.WriteLine(message);
            }
        }

        [Fact]
        public void ValidatePropertyChangedIsInjected()
        {
            var assembly = _weaverHelper.Assembly;
            var instance = assembly.GetInstance("ImplementsPropertyChanged");
            var calls = new List<string>();

            var inpc = (INotifyPropertyChanged)instance;
            inpc.PropertyChanged += (sender, args) => calls.Add(args.PropertyName);

            instance.Property1 = "Test";

            Assert.True(new[] { "Property1" }.SequenceEqual(calls));
        }
    }
}