using System;

namespace MyTryHard.FiltersAttributes
{
    /// <summary>
    /// When applied on class; Every field gets the Allow value by default; 
    /// When applied on Property, overrides Parent's rule on this property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class
        | AttributeTargets.Struct
        | AttributeTargets.Property
        | AttributeTargets.Field)]
    public class AllowSqlAutoFill : System.Attribute
    {
        /// <summary>
        /// Tells if we can auto fill. 'true' by default.
        /// </summary>
        public bool AutoFill { get; set; }

        /// <summary>
        /// Corresponds to the DbColumnName; 
        /// Useful if there's any name conflict, you can just specify the column name.
        /// </summary>
        public string DbColumnName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allow"></param>
        public AllowSqlAutoFill(bool allow = true)
        {
            AutoFill = allow;
        }
    }
}
