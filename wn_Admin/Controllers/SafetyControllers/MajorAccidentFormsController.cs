using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.Safety;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using wn_Admin.Models.UtilityModels;

namespace wn_Admin.Controllers.SafetyControllers
{
    public class MajorAccidentFormsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: MajorAccidentForms
        public ActionResult Index()
        {
            return View(db.MajorAccidentForms.ToList());
        }

        // GET: MajorAccidentForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorAccidentForm majorAccidentForm = db.MajorAccidentForms.Find(id);
            if (majorAccidentForm == null)
            {
                return HttpNotFound();
            }
            return View(majorAccidentForm);
        }

        // GET: MajorAccidentForms/Create
        public ActionResult Create()
        {
            ViewBag.AcTypes = db.AccidentTypes.ToList();
            var atValues = db.AccidentTypeValues.ToList();
            ViewBag.ATValues = new MultiSelectList(atValues, "AccidentTypeValueName", "AccidentTypeValueName");
            ViewBag.ImmeCauses = new MultiSelectList(db.ImmeCauses, "ImmeCauseName", "ImmeCauseName");
            ViewBag.KeyStates = new MultiSelectList(db.KeyStates, "KeyStateName", "KeyStateName");
            ViewBag.YesNo = new SelectList(db.YesNoNA.Where(w => w.YesNoNAName != "N/A"), "YesNoNAName", "YesNoNAName");
            ViewBag.YesNoNA = new SelectList(db.YesNoNA, "YesNoNAName", "YesNoNAName");
            ViewBag.PPEOpts = new SelectList(db.AccidentPPEOptions, "AccidentPPEOptionName", "AccidentPPEOptionName");
            ViewBag.ReOpts = new SelectList(db.ReoccurOptions, "ReoccurOptionName", "ReoccurOptionName");
            ViewBag.AdvEffOpts = new MultiSelectList(db.AdvEffs, "AdvEffName", "AdvEffName");
            return View();
        }

        // POST: MajorAccidentForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string[] AccidentTypeValueID, string[] ImmeCauses, string[] KeyS, string[] AdvEffOpts, IEnumerable<HttpPostedFileBase> aphotos, [Bind(Include = "MajorAccidentFormID,Name,TypeInjury,BodyLeft,BodyRight,HospitalName,DoctorName,FirstAidAttendent,EquipmentInvolved,WCBFormCompSubm,OHSNotified,OHSNameContact,DescLoss,InjuriesDueToIncident,LostTime,EquipOut,OtherLosses,WitnessesOccur,DateOccur,TimeOccur,LocationOccur,AccidentPPEOption,ListPPE,WasPersonTrained,PotReoccur,LossPotEval,LocationSpill,SourceSpill,TypeSubstance,AmountSubstance,AdvEffOn,AccidentDesc,PhotoTaken,WitnessStatement,ImmediateCauses,KeyStates,SubstandardActions,SubstandardConditions,BasicCauses,PersonalFactors,JobFactors,RiskAssess,TempActionTaken,SuggestedCorrActions,PreparedBy,PrepareDate,ApprovedBy,ApprovedDate")] MajorAccidentForm majorAccidentForm)
        {
            if (ModelState.IsValid)
            {




                if (AdvEffOpts != null)
                {
                    string content = string.Join(", ", AdvEffOpts);
                    majorAccidentForm.AdvEffOn = content + " | Other: " + majorAccidentForm.AdvEffOn;
                }



                if (ImmeCauses != null)
                {

                    string content = string.Join(", ", ImmeCauses);

                    majorAccidentForm.ImmediateCauses = content + " | Other: " + majorAccidentForm.ImmediateCauses;
                }

                if (KeyS != null)
                {
                    string content = string.Join(", ", KeyS);

                    majorAccidentForm.KeyStates = content + " | Other: " + majorAccidentForm.KeyStates;
                }

                db.MajorAccidentForms.Add(majorAccidentForm);
                db.SaveChanges();

                if (AccidentTypeValueID != null)
                {
                    foreach (var item in AccidentTypeValueID)
                    {
                        AccidentType at = new AccidentType();
                        at.MajorAccidentFormID = majorAccidentForm.MajorAccidentFormID;
                        at.AccidentTypeName = item.ToString();
                        db.AccidentTypes.Add(at);
                        db.SaveChanges();
                    }
                }

                StorageController sc = new StorageController();
                sc.setContainer("accidentphotos");

                if (aphotos != null)
                {
                    foreach (var file in aphotos)
                    {
                        if (file.ContentLength > 0)
                        {

                            var path = sc.upload(file);

                            AccidentPhoto acPhoto = new AccidentPhoto();
                            acPhoto.MajorAccidentFormID = majorAccidentForm.MajorAccidentFormID;
                            acPhoto.Url = path;
                            db.AccidentPhotos.Add(acPhoto);
                            db.SaveChanges();
                        }
                    }
                }


                return RedirectToAction("Index");
            }

            return View(majorAccidentForm);
        }

        // GET: MajorAccidentForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorAccidentForm majorAccidentForm = db.MajorAccidentForms.Find(id);
            if (majorAccidentForm == null)
            {
                return HttpNotFound();
            }
            return View(majorAccidentForm);
        }

        // POST: MajorAccidentForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MajorAccidentFormID,Name,TypeInjury,BodyLeft,BodyRight,HospitalName,DoctorName,FirstAidAttendent,EquipmentInvolved,WCBFormCompSubm,OHSNotified,OHSNameContact,DescLoss,InjuriesDueToIncident,LostTime,EquipOut,OtherLosses,WitnessesOccur,DateOccur,TimeOccur,LocationOccur,AccidentPPEOption,ListPPE,WasPersonTrained,PotReoccur,LossPotEval,LocationSpill,SourceSpill,TypeSubstance,AmountSubstance,AdvEffOn,AccidentDesc,PhotoTaken,WitnessStatement,ImmediateCauses,KeyStates,SubstandardActions,SubstandardConditions,BasicCauses,PersonalFactors,JobFactors,RiskAssess,TempActionTaken,SuggestedCorrActions,PreparedBy,PrepareDate,ApprovedBy,ApprovedDate")] MajorAccidentForm majorAccidentForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(majorAccidentForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(majorAccidentForm);
        }

        // GET: MajorAccidentForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorAccidentForm majorAccidentForm = db.MajorAccidentForms.Find(id);
            if (majorAccidentForm == null)
            {
                return HttpNotFound();
            }
            return View(majorAccidentForm);
        }

        // POST: MajorAccidentForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MajorAccidentForm majorAccidentForm = db.MajorAccidentForms.Find(id);
            db.MajorAccidentForms.Remove(majorAccidentForm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
