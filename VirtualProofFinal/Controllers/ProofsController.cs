﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using VirtualProofFinal.Models;

namespace VirtualProofFinal.Controllers
{
    public class ProofsController : Controller
    {
        private ProofDbContext db = new ProofDbContext();

        // GET: Proofs
        public ActionResult Index()
        {
            return View(db.Proofs.ToList());
        }

        // GET: Proofs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proof proof = db.Proofs.Find(id);
            if (proof == null)
            {
                return HttpNotFound();
            }
            return View(proof);
        }

        // GET: Proofs/Create
        [HttpGet]
        public ActionResult Create()
        {
            var paperSizes = GetAllPaperSizes();
            var orientations = GetAllOrientations();
            var model = new Proof();

            model.PaperSizes = GetSelectListItems(paperSizes);
            model.Orientations = GetSelectListItems(orientations);

            return View(model);
        }

        // POST: Proofs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProofName,PaperSize,Orientation")] Proof proof, HttpPostedFileBase file)
        {

            var paperSizes = GetAllPaperSizes();
            var orientations = GetAllOrientations();

            proof.PaperSizes = GetSelectListItems(paperSizes);
            proof.Orientations = GetSelectListItems(orientations);

            if (ModelState.IsValid)
            {
                //if (file != null)
                //{
                //    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                //                + file.FileName);
                //    proof.ImagePath = file.FileName;
                //}
                var filePaths = new List<string>();

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    string savePath = "~/Images/" + @User.Identity.GetUserId() + "/" + @proof.ProofName + "/";
                    DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath(savePath));
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    file = Request.Files[i];
                    file.SaveAs(HttpContext.Server.MapPath(savePath)
                                + file.FileName);
                    proof.ImagePath = proof.ProofName + file.FileName;

                    //ViewData["ProofName"] = proof.ProofName;
                    //ViewData["ImagePath"] = proof.ImagePath;

                    filePaths.Add();

                }
                db.Proofs.Add(proof);
                db.SaveChanges();
                return RedirectToAction("GeneratePdf");
            }

            return View(proof);
        }

        // GET: Proofs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proof proof = db.Proofs.Find(id);
            if (proof == null)
            {
                return HttpNotFound();
            }
            return View(proof);
        }

        // POST: Proofs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProofName,PaperSize,Orientation")] Proof proof)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proof).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proof);
        }

        // GET: Proofs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proof proof = db.Proofs.Find(id);
            if (proof == null)
            {
                return HttpNotFound();
            }
            return View(proof);
        }

        // POST: Proofs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proof proof = db.Proofs.Find(id);
            db.Proofs.Remove(proof);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GeneratePdf(Proof proof)
        {
            ViewData["ProofName"] = proof.ProofName;

            return View(db.Proofs.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private IEnumerable<string> GetAllPaperSizes()
        {
            return new List<string>
            {
                "A6",
                "A5",
                "A4",
                "A3",
                "A2",
                "A1"
            };
        }

        private IEnumerable<string> GetAllOrientations()
        {
            return new List<string>
            {
                "Landscape",
                "Portrait",
            };
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            var selectList = new List<SelectListItem>();

            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }

    }
}
