using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace UniversalConverter.Tests.ConverterImplementations
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TypeConverterConverterTests
    {
        [TestMethod]
        public void StringToGuid()
        {
            var converter = new TypeConverterConverter();

            Guid e = Guid.NewGuid();
            string s = e.ToString();

            var context = new ConverterContext(typeof(string), typeof(Guid), s, CultureInfo.CurrentCulture);

            var result = converter.Convert(context);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOfType(result.Result, typeof(Guid));
            Assert.AreEqual(e, result.Result);
            Assert.IsFalse(ReferenceEquals(e, result.Result));
        }

        [TestMethod]
        public void NullStringToGuid()
        {
            var converter = new TypeConverterConverter();
            var context = new ConverterContext(typeof(string), typeof(Guid), null, CultureInfo.CurrentCulture);

            var result = converter.Convert(context);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Result);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void NullStringToNullableGuid()
        {
            var converter = new TypeConverterConverter();
            var context = new ConverterContext(typeof(string), typeof(Guid?), null, CultureInfo.CurrentCulture);

            var result = converter.Convert(context);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
        }
    }
}
