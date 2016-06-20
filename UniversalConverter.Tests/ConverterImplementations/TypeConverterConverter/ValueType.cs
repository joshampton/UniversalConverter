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

            object result = null;
            bool success = converter.TryConvert(context, out result);

            Assert.IsTrue(success);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Guid));
            Assert.AreEqual(e, result);
        }

        [TestMethod]
        public void NullStringToGuid()
        {
            var converter = new TypeConverterConverter();
            var context = new ConverterContext(typeof(string), typeof(Guid), null, CultureInfo.CurrentCulture);

            object result = null;
            bool success = converter.TryConvert(context, out result);

            Assert.IsFalse(success);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void NullStringToNullableGuid()
        {
            var converter = new TypeConverterConverter();
            var context = new ConverterContext(typeof(string), typeof(Guid?), null, CultureInfo.CurrentCulture);

            int comp = 1;
            object result = (object)comp;
            bool success = converter.TryConvert(context, out result);

            Assert.IsTrue(success);
            Assert.AreNotEqual(comp, result);
            Assert.IsNull(result);
        }
    }
}
