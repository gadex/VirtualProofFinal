using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualProofFinal.Models
{
    public class Proof
    {
        public int ID { get; set; }
        public string ProofName { get; set; }
        public string PaperSize { get; set; }
        public string Orientation { get; set; }
        public string ImagePath { get; set; }

        public IEnumerable<SelectListItem> PaperSizes { get; set; }
        public IEnumerable<SelectListItem> Orientations { get; set; }
    }

    public class ProofDbContext : DbContext
    {
        public DbSet<Proof> Proofs { get; set; }
    }
}