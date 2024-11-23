using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPalaceApp2
{
    public class Client1
    {
        public int Id { get; set; }
        public string Full_name { get; set; }
        public string Age { get; set; }
        public string gender { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public DateTime registration_date { get; set; }
    }
    public class Conseltation
    {
        public int Id { get; set; }
        public int service_id { get; set; }
        public int specialist_id { get; set; }
        public int client_id { get; set; }
        public int field_id { get; set; }
        public DateTime consultation_time { get; set; }
        public DateTime consultation_date { get; set; }
        public string notes { get; set; }
    }
    public class Status1
    {
        public int Id { get; set; }
        public int consultation_id { get; set; }
        public string status_description { get; set; }
    }
    public class Specialist1
    {
        public int Id { get; set; }
        public string Full_name { get; set; }
        public string Age { get; set; }
        public string gender { get; set; }
        public string field { get; set; }
        public int field_id { get; set; }

    }
    public class Field1
    {
        public int Id { get; set; }
        public string Field_name { get; set; }

    }
    public class Serviece
    {
        public int Id { get; set; }
        public string servies_name { get; set; }
        public string servies_field { get; set; }
        public string price { get; set; }

    }
    public class ServieceList
    {
        public int Id { get; set; }
        public string servies_count { get; set; }
        public int client_id { get; set; }
        public int serviec_id { get; set; }

    }

}
