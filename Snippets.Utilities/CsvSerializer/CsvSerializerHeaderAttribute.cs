using System;

namespace Snippets.Utilities.CsvSerializer
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CsvSerializerHeaderAttribute : Attribute
    {
        private const string DoubleArgumentErrorMessage = "Must only provide a HeaderName or ColumnNumber, not both.";

        private string _headerName;
        private int _columnNumber;

        public string HeaderName
        {
            get => _headerName;
            set
            {
                if (ColumnNumber > 0)
                {
                    throw new Exception(DoubleArgumentErrorMessage);
                }

                _headerName = value;
            }
        }

        public int ColumnNumber
        {
            get => _columnNumber;
            set
            {
                if (!String.IsNullOrEmpty(HeaderName))
                {
                    throw new Exception("DoubleArgumentErrorMessage");
                }

                _columnNumber = value;
            }
        }

        public string DateTimeFormat { get; set; } = "";
    }
}
