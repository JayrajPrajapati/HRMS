using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProspectServices
    {
        /// <summary>
        /// Gets the prospect list.
        /// </summary>
        /// <returns></returns>
        List<ProspectModel> GetProspectList();

        /// <summary>
        /// Gets the skill models.
        /// </summary>
        /// <returns></returns>
        List<SkillModel> GetSkillModelList();

        /// <summary>
        /// Inserts the prospect list.
        /// </summary>
        /// <param name="prospectModel">The prospect model.</param>
        /// <returns></returns>
        ProspectModel InsertProspectList(ProspectModel prospectModel);

        /// <summary>
        /// Gets the propects by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        ProspectModel GetPropectsById(int Id);

        /// <summary>
        /// Updates the prospect.
        /// </summary>
        /// <param name="prospectModel">The prospect model.</param>
        void UpdateProspect(ProspectModel prospectModel);

        /// <summary>
        /// Deletes the prospect by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        void DeleteProspectById(int Id);

        /// <summary>
        /// Logins the using usernameandpaswword.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
        UserModel LoginUsingUsernameandpaswword(UserModel userModel);

        /// <summary>
        /// Gets the user model list.
        /// </summary>
        /// <returns></returns>
        List<UserModel> GetUserModelList();

        /// <summary>
        /// Inserts the company response.
        /// </summary>
        /// <param name="companyResponseModel">The company response model.</param>
        /// <returns></returns>
        CompanyResponseModel InsertCompanyResponse(CompanyResponseModel companyResponseModel);

        /// <summary>
        /// Gets the interview list.
        /// </summary>
        /// <returns></returns>
        List<ProspectModel> GetInterviewList();
    }
}
