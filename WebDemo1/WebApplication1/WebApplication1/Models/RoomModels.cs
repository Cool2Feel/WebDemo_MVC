using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RoomModels
    {
        public int ID
        {
            get; set;
        }
        [Required]
        [StringLength(50)]
        public string Name
        {
            get; set;
        }
        public string Peopel
        {
            get; set;
        }
        public string Coustomer
        {
            get; set;
        }    
        public DateTime StartTime
        {
            get; set;
        }
        public DateTime EndTime
        {
            get; set;
        }
        public string Tips
        {
            get; set;
        }
        public bool Status
        {
            get;set;
        }
        public string Page
        {
            get; set;
        }
        public string Person
        { get; set; }
        public string Number { set; get; }
        public string Postion { set; get; }
    }

    public class HistoryModels
    {
        public int ID
        {
            get; set;
        }
        public int RoomId
        {
            get; set;
        }
        public string RoomName
        {
            get; set;
        }

        public string Peopel
        {
            get; set;
        }

        public string Coustomer
        {
            get; set;
        }


        public DateTime StartTime
        {
            get; set;
        }

        public DateTime EndTime
        {
            get; set;
        }

        public string Tips
        {
            get; set;
        }

        public string Person
        {
            get; set;
        }

    }


    public class RoomDBContext : DbContext
    {
        public DbSet<RoomModels> RoomModels
        {
            get; set;
        }

        public DbSet<HistoryModels> HistoryModels
        {
            get;set;
        }
    }


}