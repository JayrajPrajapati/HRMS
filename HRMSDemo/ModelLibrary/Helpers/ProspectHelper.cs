using DBHelper;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class ProspectHelper
    {

        /// <summary>
        /// Converts the prospect to prospect model.
        /// </summary>
        /// <param name="prospect">The prospect.</param>
        /// <returns></returns>
        #region Public Method
        public ProspectModel ConvertProspectToProspectModel(Prospect prospect)
        {
            try
            {
                ProspectModel prospectModel = new ProspectModel();
                prospectModel.ProspectID = prospect.ProspectID;
                prospectModel.FirstName = prospect.FirstName;
                prospectModel.MiddleName = prospect.MiddleName;
                prospectModel.LastName = prospect.LastName;
                prospectModel.DOB = prospect.DOB;
                prospectModel.Email = prospect.Email;
                prospectModel.Mobile = prospect.Mobile;
                prospectModel.Gender = prospect.Gender;
                prospectModel.Address = prospect.Address;
                prospectModel.FileName = prospect.FileName;
                prospectModel.Status = prospect.Status;
                return prospectModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Converts the skill to skill model.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <returns></returns>
        public SkillModel ConvertSkillToSkillModel(Skill skill)
        {
            try
            {
                SkillModel skillModel = new SkillModel();
                skillModel.SkillID = skill.SkillID;
                skillModel.Name = skill.Name;
                return skillModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Converts the skill list to skill model list.
        /// </summary>
        /// <param name="skills">The skills.</param>
        /// <returns></returns>
        public List<SkillModel> ConvertSkillListToSkillModelList(List<Skill> skills)
        {
            try
            {
                List<SkillModel> skillModels = new List<SkillModel>();
                if (skills != null && skills.Count > 0)
                {
                    foreach (Skill skill in skills)
                    {
                        SkillModel skillModel = new SkillModel();
                        skillModel = ConvertSkillToSkillModel(skill);
                        skillModels.Add(skillModel);
                    }
                }
                return skillModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Converts the prospect list to prospect model.
        /// </summary>
        /// <param name="prospects">The prospects.</param>
        /// <returns></returns>
        public List<ProspectModel> ConvertProspectListToProspectModel(List<Prospect> prospects)
        {
            try
            {
                List<ProspectModel> prospectModels = new List<ProspectModel>();
                if (prospects != null && prospects.Count > 0)
                {
                    foreach (Prospect prospect in prospects)
                    {
                        ProspectModel prospectModel = new ProspectModel();
                        prospectModel = ConvertProspectToProspectModel(prospect);
                        prospectModels.Add(prospectModel);
                    }
                }
                return prospectModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Converts the prospect model to prospect.
        /// </summary>
        /// <param name="prospectModel">The prospect model.</param>
        /// <returns></returns>
        public Prospect ConvertProspectModelToProspect(ProspectModel prospectModel)
        {
            try
            {
                Prospect prospect = new Prospect();
                prospect.ProspectID = prospectModel.ProspectID;
                prospect.FirstName = prospectModel.FirstName;
                prospect.MiddleName = prospectModel.MiddleName;
                prospect.LastName = prospectModel.LastName;
                prospect.DOB = prospectModel.DOB;
                prospect.Email = prospectModel.Email;
                prospect.Mobile = prospectModel.Mobile;
                prospect.Gender = prospectModel.Gender;
                prospect.Address = prospectModel.Address;
                prospect.FileName = prospectModel.FileName;
                prospect.Status = prospectModel.Status;
                return prospect;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Converts the user to user model.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public UserModel ConvertUserToUserModel(User user)
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.UserID = user.UserID;
                userModel.UserName = user.UserName;
                userModel.Email = user.Email;
                userModel.Password = user.Password;
                userModel.RoleType = (byte)user.RoleType;
                userModel.Active = (bool)user.Active;
                return userModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CompanyResponse ConvertCompanyResponseModelToCompanyResponse(CompanyResponseModel companyResponseModel)
        {
            try
            {
                CompanyResponse companyResponse = new CompanyResponse();
                companyResponse.ProspectID = companyResponseModel.ProspectID;
                companyResponse.Status = companyResponseModel.Status;
                companyResponse.ScheduleDate = companyResponseModel.ScheduleDate;
                companyResponse.UserID = companyResponseModel.UserID;
                companyResponse.Comments = companyResponseModel.Comments;
                companyResponse.CreatedDate = companyResponseModel.CreatedDate;
                companyResponse.CreatedBy = companyResponseModel.CreatedBy;
                return companyResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Converts the user list to user model list.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <returns></returns>
        public List<UserModel> ConvertUserListToUserModelList(List<User> users)
        {
            try
            {
                List<UserModel> userModels = new List<UserModel>();
                if (users != null && users.Count > 0)
                {
                    foreach (User user in users)
                    {
                        UserModel userModel = new UserModel();
                        userModel = ConvertUserToUserModel(user);
                        userModels.Add(userModel);
                    }
                }
                return userModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
