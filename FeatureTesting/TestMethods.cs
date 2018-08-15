using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureTesting
{
    using OmIntLib.DataModels;
    using OmIntLib.Systems;
    using OmIntLib.Data;
    using System.IO;

    static class TestMethods
    {
        public static Ometria ometria;
        public static string merretConnString;

        static Contact contact = TestContact;
        static Contact contact2 = TestContact2;
        static Product product = TestProduct;
        static Order order = TestOrder;
        
        static string singleOutputPath = @"J:\IT\Tim\Ometria\TestOutput\SingleOutput.json";
        static string collectionOutputPath = @"J:\IT\Tim\Ometria\TestOutput\CollectionOutput.json";

        public static void MainMethod()
        {
            OrderLineItem ol1 = new OrderLineItem() { product_id = "00709998801", variant_id = "1123", quantity = 1, unit_price = 79, quantity_refunded = 1  };
            ol1.variant_options.Add(new OmertiraAttribute() { type = "size", id = "24", label = "24" });
            ((List<OrderLineItem>)order.lineitems).Add(ol1);

            ol1 = new OrderLineItem() { product_id = "00709998801", variant_id = "1124", quantity = -1, unit_price = 79, quantity_refunded = 1 };
            ol1.variant_options.Add(new OmertiraAttribute() { type = "size", id = "26", label = "26" });
            ((List<OrderLineItem>)order.lineitems).Add(ol1);


            contact.properties.Add("whistles_wednesday", "N");
            //contact.properties.Add("menswear", "Y");
            //OrderLineItem ol2 = new OrderLineItem() { product_id = "00702761801", variant_id = "139457", quantity = 1, unit_price = 79 };
            //ol2.properties.Add("size", "26");
            //((List<OrderLineItem>)order.lineitems).Add(ol2);

            //OrderLineItem ol3 = new OrderLineItem() { product_id = "00702715512", variant_id = "136832", quantity = 1, unit_price = 75, discount = 20 };
            //ol3.properties.Add("size", "26");
            //((List<OrderLineItem>)order.lineitems).Add(ol3);

            //List<Product> products = (List<Product>)DataParser.DataTableToObject<Product>(@"SELECT * FROM ""V66LWHIU#"".OMTR_PROD WHERE ""id"" IN ('00702767528')", merretConnString, DataParser.GetProductProperties);
            //List<Product> products = (List<Product>)DataParser.DataTableToObject<Product>(@"SELECT * FROM ""V66LWHIU#"".OMTR_PROD WHERE ""id"" LIKE '007027%'", merretConnString, DataParser.GetProductProperties);

            //List<Contact> contacts = (List<Contact>)DataParser.DataTableToObject<Contact>(@"SELECT * FROM ""V66LWHIU#"".OMTR_CONTACT WHERE ""id"" = 309104", merretConnString, DataParser.GetContactProperties);

            //Contact Oc = contacts.First();
            //Oc.email = "tonyBurridg@mannyman.com";
            //Oc.add_to_lists = new int[] { 308 };

            //ometria.CreateContactListing(contact.collection, DataParser.ObjectToJSon(contact), contact.id);

            //File.WriteAllText(singleOutputPath, ometria.GetContactListing("cat", "400555"));

            File.WriteAllText(singleOutputPath, ometria.GetOrder("116"));
            //ometria.CreateOrder(DataParser.ObjectToJSon(order), order.id);
        }

        static Contact TestContact => new Contact()
        {
            id = "400555",
            email = "blowmeaway@icloud.com",
            firstname = "Tony tttttttt",
            //lastname = "Dante ttt",
            collection = "secondCat",
            marketing_optin = marketing_optin_options.EXPLICITLY_OPTEDIN,
            add_to_lists = new int[] { 635 },
            
            merge = true

        };

        static Contact TestContact2 => new Contact()
        {
            id = "32199",
            email = "tbunson@gmail.com",
            firstname = "Timothy",
            lastname = "Bunson",
            collection = "TheCollection",
            phone_number = "+447788799999", // new ContactPhoneNumber() { number = "+447788799999", country_code = "GB" },
            //marketing_optin = marketing_optin_options.EXPLICITLY_OPTEDIN,
            add_to_lists = new int[] { 207 },
            merge = true
            //timestamp_unsubscribed = new DateTime(2018,8,29,14,41,21,DateTimeKind.Local)
        };

        static Product TestProduct => new Product()
        {
            id = "007123456789",
            title = "My Sexy Product",
            //url = @"http://www.whistles.com/dw/image/v2/AAQB_PRD/on/demandware.static/-/Sites-whistles-master-catalog/default/dwdb23dca0/images/00102877042/whistles-animal-faux-fur-cocoon-coat-leopard-print_medium_06.jpg?sw=137&sh=142&sm=fit&cx=493&cy=0&cw=1494&ch=1494",
            //image_url = @"http://www.whistles.com/dw/image/v2/AAQB_PRD/on/demandware.static/-/Sites-whistles-master-catalog/default/dwdb23dca0/images/00102877042/whistles-animal-faux-fur-cocoon-coat-leopard-print_medium_06.jpg?sw=137&sh=142&sm=fit&cx=493&cy=0&cw=1494&ch=1494"
            price = 900,
            //is_active = true
        };

        static Order TestOrder2 => new Order()
        {
            id = "112",
            shipping = 500
        };

        static Order TestOrder => new Order()
        {
            id = "118",
            timestamp = DateTime.Now,
       
            currency = "USD",     
            store = "0501",
            customer = new OrderCustomer() { customer_id = "400555", email = "blowmeaway@icloud.com" },
            //is_valid = false
            
        };

    }
}
