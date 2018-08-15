using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Odbc;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OmIntLib.DataModels;
using System.Collections;
using OmIntLib.Systems;

namespace OmIntLib.Data
{
    public static class DataParser
    {
        private static DataTable GetDataTable(string SQL, string connString)
        {
            DataTable table = new DataTable();

            using (OdbcConnection conn = new OdbcConnection(connString))
            using (OdbcCommand command = new OdbcCommand(SQL, conn))
            {
                command.CommandTimeout = 1000;
                conn.Open();
                table.Load(command.ExecuteReader());
            }

            return table;
        }

        public static IEnumerable<T> DataTableToObject<T>(string sql, string connString, Func<T, DataRow, T> func)
            => DataTableToObject(GetDataTable(sql, connString), func);

        public static IEnumerable<T> DataTableToObject<T>(DataTable table, Func<T, DataRow, T> func)
        {
            List<T> col = new List<T>();
            foreach (DataRow row in table.Rows) col.Add(DataRowToObject(row, func));
            return col;
        }

        public static IEnumerable<T> DataTableToObject<T>(string sql, string connString)
            => DataTableToObject<T>(GetDataTable(sql, connString));
   
        public static IEnumerable<T> DataTableToObject<T>(DataTable table)
        {
            List<T> col = new List<T>();
            foreach (DataRow row in table.Rows) col.Add(DataRowToObject<T>(row));
            return col;
        }

        public static T DataRowToObject<T>(DataRow row, Func<T, DataRow, T> func) => func(DataRowToObject<T>(row), row);

        public static T DataRowToObject<T>(DataRow row)
        {
            T obj = (T)Activator.CreateInstance(typeof(T));
            foreach (PropertyInfo p in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)) SetPropertyValueFromDataRow(obj, p, row);
            return obj;
        }

        private static void SetPropertyValueFromDataRow(object obj, PropertyInfo p, DataRow row)
        {
            if (!row.Table.Columns.Contains(p.Name)) return;
            Type type = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
            if (type.IsEnum) try { p.SetValue(obj, Convert.ChangeType(Enum.Parse(type, (string)row[p.Name], true), type)); } catch { }
            else p.SetValue(obj, Convert.ChangeType(row[p.Name], type));
        }

        public static string ObjectToJSon(object o)
            => JsonConvert.SerializeObject(o, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });

        public static T JSonToObject<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        private static void AddPropertiesFromRow(Dictionary<string, string> properties, DataRow row)
        {
            DataTable dataTable = row.Table;
            string colName;

            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                colName = dataTable.Columns[j].ColumnName;
                if (colName.IndexOf("properties.") == 0) try { properties.Add(colName.Split('.')[1], (string)row[colName]); } catch { }
            }
        }

        private static void AddAttriutesFromRow(List<OmertiraAttribute> attributes, DataRow row)
        {
            DataTable dataTable = row.Table;
            string colName;

            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                colName = dataTable.Columns[j].ColumnName;
                if (colName.IndexOf("attributes.") == 0)
                    try
                    {
                        attributes.Add(new OmertiraAttribute()
                        {
                            type = colName.Split('.')[1],
                            id = ((string)row[colName]).Split(new[] { "||" }, StringSplitOptions.None)[0],
                            label = ((string)row[colName]).Split(new[] { "||" }, StringSplitOptions.None)[1]
                        });
                    }
                    catch { }
            }
        }

        public static class CustomReadMethods
        {
            public static IEnumerable<Order> GetOrders(string sql, string connString)
            {
                DataTable table = GetDataTable(sql, connString);
                string currentId = string.Empty;
                Order order = null;
      
                foreach (DataRow row in table.Rows)
                {
                    if ((string)row["id"] != currentId && currentId != string.Empty) yield return order;
                    if ((string)row["id"] != currentId)
                    {
                        currentId = (string)row["id"];
                        order = DataRowToObject<Order>(row);
                        order.customer = DataRowToObject<OrderCustomer>(row);
                        if ((string)row["store"] == "0501") order.shipping_address = DataRowToObject<OrderAdress>(row);
                    }
                    OrderLineItem lineItem = DataRowToObject<OrderLineItem>(row);

                    AddAttriutesFromRow(lineItem.variant_options, row);

                    order.lineitems.Add(lineItem);
                }

                yield return order;
            }
            public static Product GetProductProperties(Product product, DataRow row)
            {
                AddAttriutesFromRow(product.attributes, row);
      
                AddPropertiesFromRow(product.properties, row);
                return product;
            }

            public static Contact GetContactProperties(Contact contact, DataRow row)
            {
                AddPropertiesFromRow(contact.properties, row);
                contact.merge = true;
                contact.force_optin = true;
                return contact;
            }

            public static Order GetTransactionProperties(Order order, DataRow row)
            {
                AddPropertiesFromRow(order.properties, row);
                return order;
            }
        }
    }

    class LongDateConverter : IsoDateTimeConverter { public LongDateConverter() => DateTimeFormat = "yyyy-MM-dd"; }

    class JsonConverterObjectToString : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(JTokenType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Object) return token.ToString();
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => serializer.Serialize(writer, value);
    }
}
