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

        //using virtual keyword to support lazy loading by entity framework
        public virtual ICollection<Image> Images { get; set; }
        public PaperSize Size { get; set; }
        public Orientation Orientation { get; set; }
    }

    public class ProofDbContext : DbContext
    {
        public DbSet<Proof> Proofs { get; set; }
    }


    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
    }

    //since they are constants use an enumeration
    public enum PaperSize
    {
        A6,A5,A4,A3,A2,A1
    }

    public enum Orientation
    {
        Landscape,Portrait
    }

}