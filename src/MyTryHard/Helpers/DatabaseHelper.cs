using MyTryHard.FiltersAttributes;
using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace MyTryHard.Helpers
{
    /// <summary>
    /// A simple class (not) that auto fills data returned by the datareader.
    /// </summary>
    public class DatabaseHelper
    {
        /// <summary>
        /// Fills model from dbreader.
        /// </summary>
        public static void FillModelFromReader<T>(T model, DbDataReader reader)
            where T : new()
        {
            if (reader.FieldCount <= 0)
                return;

            // We'll look if it got any attributes.
            Type tModel = typeof(T);
            // old line: Attribute.GetCustomAttributes(tModel, typeof(AllowSqlAutoFill));
            Attribute attributeOnModel = tModel.GetTypeInfo().GetCustomAttribute(typeof(AllowSqlAutoFill));

            // Get the properties and the fields infos.
            PropertyInfo[] propInfos = tModel.GetProperties();
            FieldInfo[] fieldInfos = tModel.GetFields();

            // Get the returned field by the reader.
            string[] lstFields = GetFieldsFromReader(reader);

            // Scan all properties.
            foreach (PropertyInfo pi in propInfos)
            {
                Attribute piAttr = pi.GetCustomAttribute(typeof(AllowSqlAutoFill), false);
                bool isForced = false; // Determines if we throw (only if DbColumnName is set)

                // If we don't have the attribute at all, skip this prop.
                if (piAttr == null && attributeOnModel == null)
                    continue;

                string dbColumnName = pi.Name;

                // If DbColumnName is specified then we need to use it. Otherwise we fall back 
                // on the property name, hoping the mapping is right
                if (piAttr != null)
                {
                    string specified = piAttr.GetType().GetProperty("DbColumnName").GetValue(piAttr) as string;

                    // If we have a specified DbColumnName, we need to throw.
                    if (specified != null)
                        isForced = true;

                    dbColumnName = specified ?? pi.Name;
                }

                bool hasProperty = lstFields.Contains(dbColumnName);
                // If the property ain't even in reader, no matter what is specified.
                // Throw an informative exception.
                if (!hasProperty && attributeOnModel != null && isForced)
                    throw new Exception("No column named '" + dbColumnName + "' found in reader for model '" + model.GetType().FullName + "'.");

                // Set if property.
                if (hasProperty)
                {
                    // If value is null, just skip.
                    if (reader.IsDBNull(reader.GetOrdinal(dbColumnName)))
                        continue;

                    if (pi.PropertyType == typeof(Guid) || pi.PropertyType == typeof(Guid?))
                        pi.SetValue(model, new Guid(reader[dbColumnName].ToString()));
                    else
                        pi.SetValue(model, Convert.ChangeType(reader[dbColumnName], pi.PropertyType));
                }
            }

            // Each fields.
            foreach (FieldInfo fi in fieldInfos)
            {
                Attribute fiAttr = fi.GetCustomAttribute(typeof(AllowSqlAutoFill), false);
                bool isForced = false;

                // Don't bother to continue if attr is null and attribute on model is too.
                if (fiAttr == null && attributeOnModel == null)
                    continue;

                // By default we'll use the field name.
                string dbColumnName = fi.Name;

                // Get DbColumnName if set.
                if (fiAttr != null)
                {
                    string specified = fiAttr.GetType().GetProperty("DbColumnName").GetValue(fiAttr) as string;

                    // If we have a specified DbColumnName, we need to throw.
                    if (specified != null)
                        isForced = true;

                    dbColumnName = specified ?? fi.Name;
                }

                bool hasField = lstFields.Contains(dbColumnName);

                // Look if it contains the column name we're looking for.
                if (!hasField && attributeOnModel != null && isForced)
                    throw new Exception("No column named '" + dbColumnName + "' found in reader for model '" + model.GetType().FullName + "'.");

                // Set if has field 
                if (hasField)
                {
                    if (fi.FieldType == typeof(Guid) || fi.FieldType == typeof(Guid?))
                        fi.SetValue(model, new Guid(reader[dbColumnName].ToString()));
                    else
                        fi.SetValue(model, Convert.ChangeType(reader[dbColumnName], fi.FieldType.GetType()));
                }
            }
        }

        /// <summary>
        /// Gets the fields from the reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static string[] GetFieldsFromReader(DbDataReader reader)
        {
            string[] lstFields = new string[reader.FieldCount];

            // Get the fields names.
            for (int i = 0; i < reader.FieldCount; ++i)
                lstFields[i] = reader.GetName(i);

            return lstFields;
        }
    }
}
