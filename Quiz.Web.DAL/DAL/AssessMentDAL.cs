using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL.DataAccess;
using Quiz.Web.DTO;
using Quiz.Web.DTO.Models;
using System.Data.SqlClient;

namespace Quiz.Web.DAL.Home
{
    public class AssessMentDAL
    {
        private DateTime dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
        public string CreateAssessMent()
        {
            return "";
        }


        /// <summary>
        /// This method will return the page modal from DB SP and also from single entity from table values but completely customized modals.
        /// </summary>
        /// <returns></returns>
        public AssesmentPageModal GetAssessmentPageModal()
        {
            try
            {
                AssesmentPageModal assesmentPageModal = new AssesmentPageModal();
                using (DBEntities TestEngineDBContext = new DBEntities())
                {
                    assesmentPageModal.LQuestionBankModal = TestEngineDBContext.Database.SqlQuery<QuestionBankModal>("exec Assesmentpagemodal").ToList();
                    assesmentPageModal.LstCandidateAssesmentDetailsForm = TestEngineDBContext.Database.SqlQuery<CustomCandidateAssesmentDetailsForm>("exec GetCandidateAssesmentDetailsForm").ToList();
                    assesmentPageModal.LstUserDetailMaster = TestEngineDBContext.Database.SqlQuery<CustomUserDetailMaster>("exec GetLstUserDetailMaster").ToList();
                    assesmentPageModal.ListEligibilityCriteria = TestEngineDBContext.Database.SqlQuery<EligibilityCriteriaList>("exec GetEligibilityCriteriaList").ToList();
                    assesmentPageModal.existingAssessmentDetails = TestEngineDBContext.Database.SqlQuery<ExistingAssessmentDetails>("exec GetExistingAssessmentDetails").ToList();


                    return assesmentPageModal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public string PostUpdateEligibleCriteria(List<PostEligibilityCriteria> lstpostAssessmentModal)
        {
            string result = "Failed";
            try
            {
                Guid AssesmentEligibilityId = Guid.NewGuid();
                using (DBEntities TestEngineDBContext = new DBEntities())
                {
                    List<EligibilityCriteriaDetail> lstEligibilityCriteriaDetail = new List<EligibilityCriteriaDetail>();
                    foreach (var item in lstpostAssessmentModal)
                    {
                        EligibilityCriteriaDetail eligibilityCriteriaDetail = new EligibilityCriteriaDetail();
                        eligibilityCriteriaDetail.ID = item.EligibilityGuid;
                        eligibilityCriteriaDetail.MayConsider = item.MayConsider;
                        eligibilityCriteriaDetail.Name = item.Name;
                        eligibilityCriteriaDetail.NotConsider = item.NotConsider;
                        eligibilityCriteriaDetail.StrongConsider = item.StrongConsider;
                        eligibilityCriteriaDetail.QuestionBankID = item.QuestionBankID;
                        eligibilityCriteriaDetail.EligibilityIdForAssessment = AssesmentEligibilityId;
                        lstEligibilityCriteriaDetail.Add(eligibilityCriteriaDetail);
                    }
                    if (lstEligibilityCriteriaDetail.Any())
                    {
                        var eligilibilityCriteriaName = lstEligibilityCriteriaDetail.FirstOrDefault().Name;
                        var eligibilityCritAvailability = TestEngineDBContext.EligibilityCriteriaDetails.FirstOrDefault(x => x.Name == eligilibilityCriteriaName);
                        if (eligibilityCritAvailability == null)
                        {
                            TestEngineDBContext.EligibilityCriteriaDetails.AddRange(lstEligibilityCriteriaDetail);
                            TestEngineDBContext.SaveChanges();
                            result = AssesmentEligibilityId.ToString();
                        }
                        else
                        {
                            result = "ALREADY AVAILABLE";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public string PostCreateAssessment(PostAssessmentModal postAssessmentModal, Guid AssessmentId)
        {
            string result = "Failed";
            dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            try
            {
                using (DBEntities TestEngineDBContext = new DBEntities())
                {
                    var assesmentDetailMaster = new DataAccess.AssessmentDetailMaster();
                    assesmentDetailMaster.AssessmentName = postAssessmentModal.AssessmentName;
                    assesmentDetailMaster.ID = AssessmentId;
                    assesmentDetailMaster.CreatedDate = dateTime;
                    assesmentDetailMaster.IsBrowserLock = postAssessmentModal.IsBrowserLockEnabled;
                    assesmentDetailMaster.IsPrintScreenLock = postAssessmentModal.IsPrintScreenLockEnabled;
                    assesmentDetailMaster.IsDeleted = false;
                    assesmentDetailMaster.ModifiedDate = dateTime;
                    assesmentDetailMaster.ScheduledEndDatetime = postAssessmentModal.ScheduleTo;
                    assesmentDetailMaster.ScheduledStartDatetime = postAssessmentModal.ScheduleFrom;
                    assesmentDetailMaster.EligibilityCriteriaId = postAssessmentModal.SelectedShortListCriteria;


                    //Question bank updation
                    List<AssessmentQuestionBankDetail> lstAssessmentQuestionBankDetail = new List<AssessmentQuestionBankDetail>();
                    foreach (var item in postAssessmentModal.LstQuestionBankSelected)
                    {
                        var questionBankInfo = TestEngineDBContext.QuestionBankMasters.FirstOrDefault(x => x.ID == item);
                        if (questionBankInfo != null)
                        {
                            AssessmentQuestionBankDetail objAssessmentQuestionBankDetail = new AssessmentQuestionBankDetail();
                            objAssessmentQuestionBankDetail.AssessmentID = assesmentDetailMaster.ID;
                            objAssessmentQuestionBankDetail.IsDeleted = false;
                            objAssessmentQuestionBankDetail.CreatedDate = dateTime;
                            objAssessmentQuestionBankDetail.ID = Guid.NewGuid();
                            objAssessmentQuestionBankDetail.ModifiedDate = dateTime;
                            objAssessmentQuestionBankDetail.QuestionBankID = item;
                            objAssessmentQuestionBankDetail.QuestionBankName = questionBankInfo.QuestionBankName;
                            objAssessmentQuestionBankDetail.Duration = questionBankInfo.Duration;
                            objAssessmentQuestionBankDetail.AssessmentDetailMaster = assesmentDetailMaster;
                            lstAssessmentQuestionBankDetail.Add(objAssessmentQuestionBankDetail);
                        }
                    }

                    //Candidate form selection

                    List<CandidateAssesmentDetailsForm> lstCandidateAssesmentDetailsForm = new List<CandidateAssesmentDetailsForm>();
                    foreach (var item in postAssessmentModal.LstCandidateFormSelectedFields)
                    {
                        var assesmentModalInfo = TestEngineDBContext.AssesmentMasterDetailsForms.ToList();
                        CandidateAssesmentDetailsForm candidateAssesmentDetailsForm = new CandidateAssesmentDetailsForm();
                        candidateAssesmentDetailsForm.AssessmentId = assesmentDetailMaster.ID;
                        candidateAssesmentDetailsForm.Createddate = dateTime;
                        candidateAssesmentDetailsForm.DisplayFieldName = assesmentModalInfo.FirstOrDefault(x => x.id == item.FormId).DisplayFieldName;
                        candidateAssesmentDetailsForm.FieldName = assesmentModalInfo.FirstOrDefault(x => x.id == item.FormId).FieldName;
                        candidateAssesmentDetailsForm.FieldType = assesmentModalInfo.FirstOrDefault(x => x.id == item.FormId).FieldType;
                        candidateAssesmentDetailsForm.FormId = assesmentModalInfo.FirstOrDefault(x => x.id == item.FormId).FormId;
                        candidateAssesmentDetailsForm.id = Guid.NewGuid();
                        candidateAssesmentDetailsForm.IsEnabled = true;
                        candidateAssesmentDetailsForm.IsLocked = assesmentModalInfo.FirstOrDefault(x => x.id == item.FormId).IsLocked;
                        candidateAssesmentDetailsForm.IsMandatory = item.IsMandatory;
                        candidateAssesmentDetailsForm.ModificationHistory = string.Empty;
                        candidateAssesmentDetailsForm.ModifiedDate = dateTime;
                        candidateAssesmentDetailsForm.Remarks = string.Empty;
                        candidateAssesmentDetailsForm.Values = assesmentModalInfo.FirstOrDefault(x => x.id == item.FormId).Values;
                        lstCandidateAssesmentDetailsForm.Add(candidateAssesmentDetailsForm);
                    }


                    //Schedule plan updation
                    TestEngineDBContext.AssessmentDetailMasters.Add(assesmentDetailMaster);

                    if (postAssessmentModal.lstBulkScheduleIds != null)
                    {
                        if (postAssessmentModal.lstBulkScheduleIds.Any())
                        {
                            List<AssessmentUserDetail> lstAssesmentdetailMaster = new List<AssessmentUserDetail>();
                            foreach (var item in postAssessmentModal.lstBulkScheduleIds)
                            {
                                AssessmentUserDetail assessmentUserDetail = new AssessmentUserDetail();
                                assessmentUserDetail.AssessmentID = assesmentDetailMaster.ID;
                                assessmentUserDetail.CreatedDate = dateTime;
                                assessmentUserDetail.ID = Guid.NewGuid();
                                assessmentUserDetail.IsDeleted = false;
                                assessmentUserDetail.ModifiedDate = dateTime;
                                assessmentUserDetail.UserID = item;
                                lstAssesmentdetailMaster.Add(assessmentUserDetail);
                            }
                            TestEngineDBContext.AssessmentUserDetails.AddRange(lstAssesmentdetailMaster);
                        }
                    }

                    if (postAssessmentModal.SingleScheduleModal != null)
                    {
                        UserDetailMaster UserDetailMaster = new UserDetailMaster();
                        UserDetailMaster.CreatedDate = dateTime;
                        UserDetailMaster.Id = Guid.NewGuid();
                        UserDetailMaster.IsDeleted = false;
                        UserDetailMaster.ModifiedDate = dateTime;
                        UserDetailMaster.UserTitle = assesmentDetailMaster.AssessmentName + " - Assessment";
                        TestEngineDBContext.UserDetailMasters.Add(UserDetailMaster);


                        ExaminerMaster examinerMaster = new ExaminerMaster();
                        examinerMaster.AssessmentId = assesmentDetailMaster.ID;
                        examinerMaster.UserDetailId = UserDetailMaster.Id;
                        TestEngineDBContext.ExaminerMasters.Add(examinerMaster);


                        DefaultRegistation defaultRegistation = new DefaultRegistation();
                        defaultRegistation.UserDetailId = UserDetailMaster.Id;
                        defaultRegistation.ID = Guid.NewGuid();
                        defaultRegistation.Name = postAssessmentModal.SingleScheduleModal.FirstName + " " + postAssessmentModal.SingleScheduleModal.LastName;
                        defaultRegistation.Email = postAssessmentModal.SingleScheduleModal.Email;
                        defaultRegistation.Password = postAssessmentModal.SingleScheduleModal.Password;
                        defaultRegistation.MobileNumber = postAssessmentModal.SingleScheduleModal.Mobile;
                        defaultRegistation.UserName = postAssessmentModal.SingleScheduleModal.UserName;
                        defaultRegistation.IsDeleted = false;
                        defaultRegistation.IsExamCompleted = false;
                        TestEngineDBContext.DefaultRegistations.Add(defaultRegistation);

                        AssessmentUserDetail assessmentUserDetail = new AssessmentUserDetail();
                        assessmentUserDetail.AssessmentID = assesmentDetailMaster.ID;
                        assessmentUserDetail.CreatedDate = dateTime;
                        assessmentUserDetail.ID = Guid.NewGuid();
                        assessmentUserDetail.IsDeleted = false;
                        assessmentUserDetail.ModifiedDate = dateTime;
                        assessmentUserDetail.UserID = UserDetailMaster.Id;
                        TestEngineDBContext.AssessmentUserDetails.Add(assessmentUserDetail);

                    }


                    //Common Login Info
                    if (postAssessmentModal.CommonLoginModal != null)
                    {
                        if (!string.IsNullOrEmpty(postAssessmentModal.CommonLoginModal.CommonLoginUserName) && !string.IsNullOrEmpty(postAssessmentModal.CommonLoginModal.CommonLoginPassword))
                        {

                            UserDetailMaster UserDetailMaster = new UserDetailMaster();
                            UserDetailMaster.CreatedDate = dateTime;
                            UserDetailMaster.Id = Guid.NewGuid();
                            UserDetailMaster.IsDeleted = false;
                            UserDetailMaster.ModifiedDate = dateTime;
                            UserDetailMaster.UserTitle = assesmentDetailMaster.AssessmentName + " - Assessment";
                            TestEngineDBContext.UserDetailMasters.Add(UserDetailMaster);


                            ExaminerMaster examinerMaster = new ExaminerMaster();
                            examinerMaster.AssessmentId = assesmentDetailMaster.ID;
                            examinerMaster.UserDetailId = UserDetailMaster.Id;
                            TestEngineDBContext.ExaminerMasters.Add(examinerMaster);

                            List<DefaultRegistation> lstDefaultReg = new List<DefaultRegistation>();
                            if (postAssessmentModal.CommonLoginModal.CLSendLoginDetailsto != null)
                            {
                                foreach (var items in postAssessmentModal.CommonLoginModal.CLSendLoginDetailsto.Split(','))
                                {
                                    DefaultRegistation defaultRegistation = new DefaultRegistation();
                                    defaultRegistation.UserDetailId = UserDetailMaster.Id;
                                    defaultRegistation.ID = Guid.NewGuid();
                                    defaultRegistation.Name = postAssessmentModal.SingleScheduleModal.FirstName + " " + postAssessmentModal.SingleScheduleModal.LastName;
                                    defaultRegistation.Email = postAssessmentModal.SingleScheduleModal.Email;
                                    defaultRegistation.Password = postAssessmentModal.SingleScheduleModal.Password;
                                    defaultRegistation.MobileNumber = postAssessmentModal.SingleScheduleModal.Mobile;
                                    defaultRegistation.IsDeleted = false;
                                    defaultRegistation.IsExamCompleted = false;
                                    lstDefaultReg.Add(defaultRegistation);
                                }
                            }
                            AssessmentUserDetail assessmentUserDetail = new AssessmentUserDetail();
                            assessmentUserDetail.AssessmentID = assesmentDetailMaster.ID;
                            assessmentUserDetail.CreatedDate = dateTime;
                            assessmentUserDetail.ID = Guid.NewGuid();
                            assessmentUserDetail.IsDeleted = false;
                            assessmentUserDetail.ModifiedDate = dateTime;
                            assessmentUserDetail.UserID = UserDetailMaster.Id;
                            TestEngineDBContext.AssessmentUserDetails.Add(assessmentUserDetail);
                            TestEngineDBContext.DefaultRegistations.AddRange(lstDefaultReg);
                        }
                    }

                    //Alert information to Admin

                    if (!string.IsNullOrEmpty(postAssessmentModal.AssesmentAlertEmail))
                    {
                        List<AssessmentAdminEmailNotification> lstAssesmentEMailNotification = new List<AssessmentAdminEmailNotification>();
                        foreach (var items in postAssessmentModal.AssesmentAlertEmail.Split(','))
                        {
                            AssessmentAdminEmailNotification assessmentAdminEmailNotification = new AssessmentAdminEmailNotification();
                            assessmentAdminEmailNotification.AssessmentId = assesmentDetailMaster.ID;
                            assessmentAdminEmailNotification.EmailId = items;
                            assessmentAdminEmailNotification.id = Guid.NewGuid();
                            assessmentAdminEmailNotification.IsAdminEmailCompletionAlertEnabled = postAssessmentModal.IsAssessmentCompletionAlertEnabled;
                            lstAssesmentEMailNotification.Add(assessmentAdminEmailNotification);
                        }
                        if (lstAssesmentEMailNotification.Any())
                        {
                            TestEngineDBContext.AssessmentAdminEmailNotifications.AddRange(lstAssesmentEMailNotification);
                        }
                    }


                    //Alert Information to Students
                    if (postAssessmentModal.AssessmentStudentAlertModal != null)
                    {
                        AssessmentStudentNotification assessmentStudentNotification = new AssessmentStudentNotification();
                        assessmentStudentNotification.AssessmentId = assesmentDetailMaster.ID;
                        assessmentStudentNotification.BCC = postAssessmentModal.AssessmentStudentAlertModal.BCC;
                        assessmentStudentNotification.BodyofMessage = postAssessmentModal.AssessmentStudentAlertModal.BodyofMessage;
                        assessmentStudentNotification.CC = postAssessmentModal.AssessmentStudentAlertModal.CC;
                        assessmentStudentNotification.CommunicationType = postAssessmentModal.AssessmentStudentAlertModal.CommunicationType;
                        assessmentStudentNotification.CreatedDate = dateTime;
                        assessmentStudentNotification.id = Guid.NewGuid();
                        assessmentStudentNotification.IsEnabled = postAssessmentModal.AssessmentStudentAlertModal.IsEnabled;
                        assessmentStudentNotification.ModHistory = string.Empty;
                        assessmentStudentNotification.ModifiedDate = dateTime;
                        assessmentStudentNotification.Remarks = string.Empty;
                        assessmentStudentNotification.Type = postAssessmentModal.AssessmentStudentAlertModal.Type;
                        TestEngineDBContext.AssessmentStudentNotifications.Add(assessmentStudentNotification);
                    }

                    TestEngineDBContext.AssessmentQuestionBankDetails.AddRange(lstAssessmentQuestionBankDetail);
                    TestEngineDBContext.CandidateAssesmentDetailsForms.AddRange(lstCandidateAssesmentDetailsForm);
                    TestEngineDBContext.SaveChanges();
                    result = "Success";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }



        public string ValidateAssesmentName(string assesmentName)
        {
            String result = "Failed";
            using (DBEntities TestEngineDBContext = new DBEntities())
            {
                var assesmentInfo = TestEngineDBContext.AssessmentDetailMasters.FirstOrDefault(x => x.AssessmentName.Trim().ToUpper() == assesmentName.Trim().ToUpper());
                if (assesmentInfo != null)
                {
                    return "AVAILABLE";
                }

            }
            return result;
        }

        public List<MyAssesmentModal> GetListMyAssesment()
        {
            try
            {
                using (DBEntities TestEngineDBContext = new DBEntities())
                {
                    var returnResult = TestEngineDBContext.Database.SqlQuery<MyAssesmentModal>("exec GetMyAssesments").ToList();
                    foreach (var item in returnResult)
                    {
                        string duration = string.Empty;
                        var assesmentQues = TestEngineDBContext.AssessmentQuestionBankDetails.Where(x => x.AssessmentID == item.AssesmentId).ToList();
                        TimeSpan totalDuarion = new TimeSpan();
                        foreach (var assesmentQuesItem in assesmentQues)
                        {
                            totalDuarion += assesmentQuesItem.Duration != null ? TimeSpan.Parse(Convert.ToString(assesmentQuesItem.Duration)) : new TimeSpan();
                        }
                        item.totaltimeDuration = Convert.ToString(totalDuarion);
                    }

                    return returnResult;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<QuestionBankModal> LQuestionBankModal()
        {
            try
            {
                using (DBEntities TestEngineDBContext = new DBEntities())
                {
                    var LQuestionBankModal = TestEngineDBContext.Database.SqlQuery<QuestionBankModal>("exec Assesmentpagemodal").ToList();
                    return LQuestionBankModal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExistingQuestionBankDetails> GetExistingQuestionBankDetails(Guid assessmentId)
        {
            try
            {
                using (DBEntities dbContext = new DBEntities())
                {
                    var existingQuestionBankDetails = dbContext.Database.SqlQuery<ExistingQuestionBankDetails>("exec GetExistingQuestionDetails @assessmentId", new SqlParameter("@assessmentID", assessmentId)).ToList();
                    return existingQuestionBankDetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<UsersDetailsModel> GetUploadedUserDetails(Guid userdetailId)
        {
            try
            {
                using (DBEntities dBContext = new DBEntities())
                {
                    var UploadedDetail = dBContext.Database.SqlQuery<UsersDetailsModel>("exec GetUploadedUserDetailsOnUserDetailId @userDetailId", new SqlParameter("@userDetailId", userdetailId)).ToList();
                    return UploadedDetail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ValidateAndDeleteAssesment(Guid AssesmentId)
        {
            string result = "Failed";
            try
            {

                using (DBEntities TestEngineDBContext = new DBEntities())
                {
                    var validationResult = TestEngineDBContext.Database.SqlQuery<string>("exec ValidateDeletionofAssesmentId @AssesmentId").FirstOrDefault();
                    if (validationResult == "Success")
                    {
                        result = TestEngineDBContext.Database.SqlQuery<string>("exec DeletionofAssesmentId @AssesmentId").FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<QuestionBankDetail> GetAssessmentQuestionDetails(Guid assessmentID)
        {
            List<QuestionBankDetail> questionBankDetailList = new List<QuestionBankDetail>();
            try
            {
                using (DBEntities TestEngineDBContext = new DBEntities())
                {
                    questionBankDetailList = TestEngineDBContext.Database.SqlQuery<QuestionBankDetail>("exec GetAssessmentQuestions @AssessmentID",
                        new SqlParameter("@AssessmentID", assessmentID)).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return questionBankDetailList;
        }

        public List<CandidateDetailsReport> GetCandidateDetails(Guid assessmentID)
        {
            List<CandidateDetailsReport> UsersDetailsModellist = new List<CandidateDetailsReport>();
            try
            {
                using (DBEntities dBEntities = new DBEntities())
                {
                    var assessmentUsermasterDetail = dBEntities.AssessmentUserDetails.FirstOrDefault(x => x.AssessmentID == assessmentID);
                    if (assessmentUsermasterDetail != null)
                    {
                        var userDetails = dBEntities.DefaultRegistations.Where(x => x.UserDetailId == assessmentUsermasterDetail.UserID).ToList();
                        if (userDetails.Count() > 0)
                        {
                            userDetails.ForEach(x =>
                            {
                                CandidateDetailsReport usersDetails = new CandidateDetailsReport();
                                usersDetails.AssessmentID = assessmentID;
                                usersDetails.Email = x.Email;
                                usersDetails.IsExamCompleted = x.IsExamCompleted ?? false;
                                usersDetails.MobileNumber = x.MobileNumber;
                                usersDetails.Name = x.Name;
                                usersDetails.UserID = x.ID;
                                usersDetails.TestDetails = GetTestDetails(assessmentID, x.ID);
                                UsersDetailsModellist.Add(usersDetails);
                            });
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return UsersDetailsModellist;
        }

        public List<TestDetails> GetTestDetails(Guid assessmentID, Guid userID)
        {
            List<TestDetails> testDetails = new List<TestDetails>();
            try
            {
                using (DBEntities dBEntities = new DBEntities())
                {
                    var examinerMaster = dBEntities.ExaminerMasters.FirstOrDefault(x => x.AssessmentId == assessmentID);
                    if (examinerMaster != null)
                    {
                        var examinerMasterDetail = dBEntities.ExaminerMasterDetails.FirstOrDefault(x => x.ExaminerMasterId == examinerMaster.ID);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return testDetails;
        }
    }
}
