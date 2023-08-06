using DBHelper;
using ModelLibrary.Models;
using RepositoryLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMSDemo.Controllers
{
    /// <summary>
    /// ProspectController
    /// </summary>
    /// <seealso cref="HRMSDemo.Controllers.BaseController" />
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ProspectController : BaseController
    {
        /// <summary>
        /// The iprospect services
        /// </summary>
        private readonly IProspectServices IprospectServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProspectController" /> class.
        /// </summary>
        /// <param name="prospectServices">The prospect services.</param>
        #region Public Method
        public ProspectController(IProspectServices prospectServices)
        {
            IprospectServices = prospectServices;
        }

        /// <summary>
        /// Indexes the specified prospect model.
        /// </summary>
        /// <param name="prospectModel">The prospect model.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(ProspectModel prospectModel)
        {
            try
            {
                List<ProspectModel> prospectModels = IprospectServices.GetProspectList();
                return View(prospectModels);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult IndexInterview(ProspectModel prospectModel)
        {
            try
            {
                List<ProspectModel> prospectModels = IprospectServices.GetInterviewList();
                return View(prospectModels);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                ViewBag.Skill = IprospectServices.GetSkillModelList();
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <param name="prospectModel">The prospect model.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create( ProspectModel prospectModel, HttpPostedFileBase file)
        {
            try
            {
                prospectModel.FileName = "";
                string _path = "";
                if (file != null)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    prospectModel.FileName = "~/UploadedFiles/" + _FileName;
                }
                prospectModel = IprospectServices.InsertProspectList(prospectModel);

                if (prospectModel.ProspectID > 0)
                {
                    if (!string.IsNullOrEmpty(prospectModel.FileName))
                        file.SaveAs(_path);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Details the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(int Id)
        {
            try
            {
                ViewBag.Skill = IprospectServices.GetSkillModelList();
                ViewBag.User = new SelectList(IprospectServices.GetUserModelList(),"UserID","UserName");
                var Prospect = IprospectServices.GetPropectsById(Id);
                return View(Prospect);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Detailses the specified company response model.
        /// </summary>
        /// <param name="companyResponseModel">The company response model.</param>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Details(CompanyResponseModel companyResponseModel , UserModel userModel)
        {
            try
            {
                userModel.UserID = Convert.ToInt32(Session["UserID"]);
                companyResponseModel.CreatedBy = userModel.UserID;
                companyResponseModel.CreatedDate = DateTime.Now;
                companyResponseModel = IprospectServices.InsertCompanyResponse(companyResponseModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            try
            {
                ViewBag.Skill = IprospectServices.GetSkillModelList();
                var Prospect = IprospectServices.GetPropectsById(Id);
                return View(Prospect);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="prospectModel">The prospect model.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(ProspectModel prospectModel, HttpPostedFileBase file)
        {
            try
            {
                
                if (file != null)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    prospectModel.FileName = "~/UploadedFiles/" + _FileName;
                    file.SaveAs(_path);
                }
                IprospectServices.UpdateProspect(prospectModel);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            try
            {
                ViewBag.Skill = IprospectServices.GetSkillModelList();
                var Prospect = IprospectServices.GetPropectsById(Id);
                return View(Prospect);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="prospectModel">The prospect model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int Id, ProspectModel prospectModel)
        {
            try
            {
                IprospectServices.DeleteProspectById(Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}