using System;
using System.Data.SqlClient;

namespace Simbahan.Transformers
{
    public abstract class Transformer<T>
    {
        /// <summary>
        /// Transforms an object to an actual model implementation.
        /// </summary>
        /// <param name="reader">SqlDataReader that executes the query</param>
        /// <returns></returns>
        public T Transform(SqlDataReader reader)
        {
            MapData(reader);

            return Parse();
        }

        protected abstract T Parse();

        protected void MapData(SqlDataReader reader)
        {
            var properties = GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            char[] lessThanSeparator = { '<' };
            char[] greaterThanSeparator = { '>' };

            foreach (var property in properties)
            {
                var propertyName = property.Name;

                if (property.Name.Contains("<") || property.Name.Contains(">"))
                    propertyName = property.Name.Split(lessThanSeparator)[1].Split(greaterThanSeparator)[0];

                property.SetValue(this, reader[propertyName]);
            }
        }

        #region Conversion Helpers

        protected int ToInt(object value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        protected double ToDouble(object value)
        {
            return Convert.ToDouble(value);
        }

        protected DateTime ToDateTime(object value)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch (Exception)
            {
                // Ignored
            }
            return DateTime.Now;
        }

        protected bool ToBoolean(object value)
        {
            return Convert.ToBoolean(value);
        }

        #endregion
    }
}