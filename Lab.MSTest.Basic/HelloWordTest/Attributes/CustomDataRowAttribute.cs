using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HelloWordTest.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CustomDataRowAttribute : DataRowAttribute
    {
        public CustomDataRowAttribute(object data1) : base(data1)
        {
        }

        public CustomDataRowAttribute(object data1, params object[] moreData) : base(data1, moreData)
        {
        }
    }
}
