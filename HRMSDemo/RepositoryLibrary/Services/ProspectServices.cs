using DBHelper;
using ModelLibrary.Helpers;
using ModelLibrary.Models;
using RepositoryLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryLibrary.Services
{
    /// <summary>
    /// ProspectServices
    /// </summary>
    /// <seealso cref="RepositoryLibrary.Interface.IProspectServices" />
    public class ProspectServices : IProspectServices
    {
        /// <summary>
        /// Gets the prospect list.
        /// </summary>
        /// <returns></returns>
        #region Public Method
        public List<ProspectModel> GetProspectList()
        {
            try
            {
                List<ProspectModel> prospectModels = new List<ProspectModel>();
                using (var entity = new Entities())
                {
                    List<Prospect> prospects = entity.Prospects.ToList();
                    ProspectHelper prospectHelper = new ProspectHelper();
                    prospectModels = prospectHelper.ConvertProspectListToProspectModel(prospects);
                }
                return prospectModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProspectModel> GetInterviewList()
        {
            try
            {
                
                List<ProspectModel> prospectModels = new List<ProspectModel>();
                using (var entity = new Entities())
                {
                    //List<Prospect> prospects = new List<Prospect>();
                    var Date = DateTime.Now.Date;
                     var prospects = (from a in entity.Prospects
                                     join b in entity.CompanyResponses on a.ProspectID equals b.ProspectID
                                     join c in entity.Users on b.UserID equals c.UserID
                                     where /* b.ScheduleDate == Date &&*/ b.Status == 1
                                     select new 
                                     {
                                         UserID = c.UserID,
                                         ProspectID = a.ProspectID,
                                         FirstName = a.FirstName,
                                         MiddleName = a.MiddleName,
                                         LastName = a.LastName,
                                         DOB = a.DOB,
                                         Email = a.Email,
                                         Mobile = a.Mobile,
                                         Gender = a.Gender,
                                         Address = a.Address,
                                         FileName = a.FileName,
                                         SchduleDate = b.ScheduleDate
                                     }).ToList();
                    //List<Prospect> prospects1 = new List<Prospect>();
                    //prospects1 = prospects.ToList();
                    //ProspectHelper prospectHelper = new ProspectHelper();
                    //prospectModels = prospectHelper.ConvertProspectListToProspectModel(prospects1);

                }
                return prospectModels;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Gets the skill models.
        /// </summary>
        /// <returns></returns>
        public List<SkillModel> GetSkillModelList()
        {
            try
            {
                List<SkillModel> skillModels = new List<SkillModel>();
                using (var entity = new Entities())
                {
                    List<Skill> skills = entity.Skills.ToList();
                    ProspectHelper prospectHelper = new ProspectHelper();
                    skillModels = prospectHelper.ConvertSkillListToSkillModelList(skills);
                }
                return skillModels;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Gets the user model list.
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetUserModelList()
        {
            try
            {
                List<UserModel> userModels = new List<UserModel>();
                using (var entity = new Entities())
                {
                    List<User> users = entity.Users.Where(S => S.RoleType == 2).ToList();
                    ProspectHelper prospectHelper = new ProspectHelper();
                    userModels = prospectHelper.ConvertUserListToUserModelList(users);
                }
                return userModels;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Inserts the prospect list.
        /// </summary>
        /// <param name="prospectModel">The prospect model.</param>
        /// <returns></returns>
        public ProspectModel InsertProspectList(ProspectModel prospectModel)
        {
            try
            {
                using (var entity = new Entities())
                {
                    ProspectHelper prospectHelper = new ProspectHelper();
                    Prospect prospect = prospectHelper.ConvertProspectModelToProspect(prospectModel);
                    entity.Prospects.Add(prospect);
                    entity.SaveChanges();
                    int id = prospect.ProspectID;
                    for (int i = 0; i < prospectModel.Skill.Length; i++)
                    {
                        ProspectSkillMaping prospectSkill = new ProspectSkillMaping();
                        prospectSkill.ProspectID = id;
                        prospectSkill.SkillID = prospectModel.Skill[i];
                        entity.ProspectSkillMapings.Add(prospectSkill);
                        entity.SaveChanges();
                    }
                    prospectModel.ProspectID = id;
                }
                return prospectModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the propects by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public ProspectModel GetPropectsById(int Id)
        {
            try
            {
                ProspectModel prospectModel = new ProspectModel();
                using (var entity = new Entities())
                {
                    ProspectHelper prospectHelper = new ProspectHelper();
                    Prospect prospect = entity.Prospects.Find(Id);
                    prospectModel = prospectHelper.ConvertProspectToProspectModel(prospect);

                    int[] prospectSkills = entity.ProspectSkillMapings
                                                .Where(S => S.ProspectID == Id)
                                                .Select(S => S.SkillID)
                                                .ToArray();

                    if (prospectSkills.Length <= 0)
                        prospectSkills = null;

                    prospectModel.Skill = prospectSkills;
                }
                return prospectModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the prospect.
        /// </summary>
        /// <param name="prospectModel">The prospect model.</param>
        public void UpdateProspect(ProspectModel prospectModel)
        {
            try
            {
                using (var entity = new Entities())
                {
                    ProspectHelper prospectHelper = new ProspectHelper();
                    Prospect prospect = prospectHelper.ConvertProspectModelToProspect(prospectModel);
                    entity.Entry(prospect).State = EntityState.Modified;
                    entity.SaveChanges();

                    entity.ProspectSkillMapings.RemoveRange(entity.ProspectSkillMapings.Where(S => S.ProspectID == prospectModel.ProspectID));
                    entity.SaveChanges();

                    for (int i = 0; i < prospectModel.Skill.Length; i++)
                    {
                        ProspectSkillMaping prospectSkill = new ProspectSkillMaping();
                        prospectSkill.ProspectID = prospectModel.ProspectID;
                        prospectSkill.SkillID = prospectModel.Skill[i];
                        entity.ProspectSkillMapings.Add(prospectSkill);
                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes the prospect by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        public void DeleteProspectById(int Id)
        {
            try
            {
                using (var entity = new Entities())
                {
                    Prospect prospect = entity.Prospects.FirstOrDefault(s => s.ProspectID == Id);
                    entity.Entry(prospect).State = EntityState.Deleted;
                    entity.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Logins the using usernameandpaswword.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
        public UserModel LoginUsingUsernameandpaswword(UserModel userModel)
        {
            try
            {
                using (var entity = new Entities())
                {
                    User user = entity.Users.Where(a => (a.UserName == userModel.UserName || a.Email == userModel.UserName) && a.Password == userModel.Password).FirstOrDefault();
                    if (user != null)
                    {
                        ProspectHelper prospectHelper = new ProspectHelper();
                        userModel = prospectHelper.ConvertUserToUserModel(user);
                    }
                    return userModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserts the company response.
        /// </summary>
        /// <param name="companyResponseModel">The company response model.</param>
        /// <returns></returns>
        public CompanyResponseModel InsertCompanyResponse(CompanyResponseModel companyResponseModel)
        {
            try
            {
                using (var entity = new Entities())
                {
                    ProspectHelper prospectHelper = new ProspectHelper();
                    CompanyResponse companyResponse = prospectHelper.ConvertCompanyResponseModelToCompanyResponse(companyResponseModel);
                    entity.CompanyResponses.Add(companyResponse);
                    entity.SaveChanges();
                }
                return companyResponseModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
