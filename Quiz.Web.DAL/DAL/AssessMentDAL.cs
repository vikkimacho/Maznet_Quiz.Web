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
                using (TestEngineEntities TestEngineDBContext = new TestEngineEntities())
                {
                    assesmentPageModal.LQuestionBankModal = TestEngineDBContext.Database.SqlQuery<QuestionBankModal>("exec Assesmentpagemodal").ToList();
                    assesmentPageModal.LstCandidateAssesmentDetailsForm = TestEngineDBContext.Database.SqlQuery<CustomCandidateAssesmentDetailsForm>("exec GetCandidateAssesmentDetailsForm").ToList();
                    assesmentPageModal.LstUserDetailMaster = TestEngineDBContext.Database.SqlQuery<CustomUserDetailMaster>("exec GetLstUserDetailMaster").ToList();
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
                using (TestEngineEntities TestEngineDBContext = new TestEngineEntities())
                {
                    List<EligibilityCriteriaDetail> lstEligibilityCriteriaDetail = new List<EligibilityCriteriaDetail>();
                    foreach (var item in lstpostAssessmentModal)
                    {
                        EligibilityCriteriaDetail eligibilityCriteriaDetail = new EligibilityCriteriaDetail();
                        eligibilityCriteriaDetail.ID = Guid.NewGuid();
                        eligibilityCriteriaDetail.MayConsider = item.MayConsider;
                        eligibilityCriteriaDetail.Name = item.Name;
                        eligibilityCriteriaDetail.NotConsider = item.NotConsider;
                        eligibilityCriteriaDetail.StrongConsider = item.StrongConsider;                        
                        eligibilityCriteriaDetail.QuestionBankID = item.QuestionBankID;
                        lstEligibilityCriteriaDetail.Add(eligibilityCriteriaDetail);
                    }
                    if(lstEligibilityCriteriaDetail.Any())
                    {
                        TestEngineDBContext.EligibilityCriteriaDetails.AddRange(lstEligibilityCriteriaDetail);
                        result = "Success";
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
            try
            {
                using (TestEngineEntities TestEngineDBContext = new TestEngineEntities())
                {
                    var assesmentDetailMaster = new AssessmentDetailMaster();
                    assesmentDetailMaster.AssessmentName = postAssessmentModal.AssessmentName;
                    assesmentDetailMaster.ID = AssessmentId;
                    assesmentDetailMaster.CreatedDate = DateTime.Now;
                    assesmentDetailMaster.IsBrowserLock = postAssessmentModal.IsBrowserLockEnabled;
                    assesmentDetailMaster.IsPrintScreenLock = postAssessmentModal.IsPrintScreenLockEnabled;
                    assesmentDetailMaster.IsDeleted = false;
                    assesmentDetailMaster.ModifiedDate = DateTime.Now;
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
                            objAssessmentQuestionBankDetail.CreatedDate = DateTime.Now;
                            objAssessmentQuestionBankDetail.ID = Guid.NewGuid();
                            objAssessmentQuestionBankDetail.ModifiedDate = DateTime.Now;
                            objAssessmentQuestionBankDetail.QuestionBankID = item;
                            objAssessmentQuestionBankDetail.QuestionBankName = questionBankInfo.QuestionBankName;
                            objAssessmentQuestionBankDetail.Duration = questionBankInfo.Duration;
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
                        candidateAssesmentDetailsForm.Createddate = DateTime.Now;
                        candidateAssesmentDetailsForm.DisplayFieldName = assesmentModalInfo.FirstOrDefault(x => x.id == item.FormId).DisplayFieldName;
                        candidateAssesmentDetailsForm.FieldName = assesmentModalInfo.FirstOrDefault(x => x.id == item.FormId).FieldName;
                        candidateAssesmentDetailsForm.FieldType = assesmentModalInfo.FirstOrDefault(x => x.id == item.FormId).FieldType;
                        candidateAssesmentDetailsForm.FormId = assesmentModalInfo.FirstOrDefault(x => x.id == item.FormId).FormId;
                        candidateAssesmentDetailsForm.id = Guid.NewGuid();
                        candidateAssesmentDetailsForm.IsEnabled = true;
                        candidateAssesmentDetailsForm.IsLocked = assesmentModalInfo.FirstOrDefault(x => x.id == item.FormId).IsLocked;
                        candidateAssesmentDetailsForm.IsMandatory = item.IsMandatory;
                        candidateAssesmentDetailsForm.ModificationHistory = string.Empty;
                        candidateAssesmentDetailsForm.ModifiedDate = DateTime.Now;
                        candidateAssesmentDetailsForm.Remarks = string.Empty;
                        candidateAssesmentDetailsForm.Values = assesmentModalInfo.FirstOrDefault(x => x.id == item.FormId).Values;
                        lstCandidateAssesmentDetailsForm.Add(candidateAssesmentDetailsForm);
                    }


                    //Schedule plan updation
                    TestEngineDBContext.AssessmentDetailMasters.Add(assesmentDetailMaster);

                    
                    if (postAssessmentModal.lstBulkScheduleIds.Any())
                    {
                        List<AssessmentUserDetail> lstAssesmentdetailMaster = new List<AssessmentUserDetail>();
                        foreach (var item in postAssessmentModal.lstBulkScheduleIds)
                        {
                            AssessmentUserDetail assessmentUserDetail = new AssessmentUserDetail();
                            assessmentUserDetail.AssessmentID = assesmentDetailMaster.ID;
                            assessmentUserDetail.CreatedDate = DateTime.Now;
                            assessmentUserDetail.ID = Guid.NewGuid();
                            assessmentUserDetail.IsDeleted = false;
                            assessmentUserDetail.ModifiedDate = DateTime.Now;
                            assessmentUserDetail.UserID = item;
                            lstAssesmentdetailMaster.Add(assessmentUserDetail);
                        }
                        TestEngineDBContext.AssessmentUserDetails.AddRange(lstAssesmentdetailMaster);
                    }

                    if (postAssessmentModal.SingleScheduleModal != null)
                    {
                        UserDetailMaster UserDetailMaster = new UserDetailMaster();
                        UserDetailMaster.CreatedDate = DateTime.Now;
                        UserDetailMaster.Id = Guid.NewGuid();
                        UserDetailMaster.IsDeleted = false;
                        UserDetailMaster.ModifiedDate = DateTime.Now;
                        UserDetailMaster.UserTitle = assesmentDetailMaster.AssessmentName + " - Assessment";
                        TestEngineDBContext.UserDetailMasters.Add(UserDetailMaster);


                        DefaultRegistation defaultRegistation = new DefaultRegistation();
                        defaultRegistation.UserDetailId = UserDetailMaster.Id;
                        defaultRegistation.ID = Guid.NewGuid();
                        defaultRegistation.Name = postAssessmentModal.SingleScheduleModal.FirstName + " " + postAssessmentModal.SingleScheduleModal.LastName;
                        defaultRegistation.Email = postAssessmentModal.SingleScheduleModal.Email;
                        defaultRegistation.Password = postAssessmentModal.SingleScheduleModal.Password;
                        defaultRegistation.MobileNumber = postAssessmentModal.SingleScheduleModal.Mobile;

                        TestEngineDBContext.DefaultRegistations.Add(defaultRegistation);

                        AssessmentUserDetail assessmentUserDetail = new AssessmentUserDetail();
                        assessmentUserDetail.AssessmentID = assesmentDetailMaster.ID;
                        assessmentUserDetail.CreatedDate = DateTime.Now;
                        assessmentUserDetail.ID = Guid.NewGuid();
                        assessmentUserDetail.IsDeleted = false;
                        assessmentUserDetail.ModifiedDate = DateTime.Now;
                        assessmentUserDetail.UserID = UserDetailMaster.Id;
                        TestEngineDBContext.AssessmentUserDetails.Add(assessmentUserDetail);

                    }


                    //Common Login Info
                    if (postAssessmentModal.CommonLoginModal != null)
                    {
                        UserDetailMaster UserDetailMaster = new UserDetailMaster();
                        UserDetailMaster.CreatedDate = DateTime.Now;
                        UserDetailMaster.Id = Guid.NewGuid();
                        UserDetailMaster.IsDeleted = false;
                        UserDetailMaster.ModifiedDate = DateTime.Now;
                        UserDetailMaster.UserTitle = assesmentDetailMaster.AssessmentName + " - Assessment";
                        TestEngineDBContext.UserDetailMasters.Add(UserDetailMaster);

                        List<DefaultRegistation> lstDefaultReg = new List<DefaultRegistation>();
                        foreach (var items in postAssessmentModal.CommonLoginModal.CLSendLoginDetailsto.Split(','))
                        {
                            DefaultRegistation defaultRegistation = new DefaultRegistation();
                            defaultRegistation.UserDetailId = UserDetailMaster.Id;
                            defaultRegistation.ID = Guid.NewGuid();
                            defaultRegistation.Name = postAssessmentModal.SingleScheduleModal.FirstName + " " + postAssessmentModal.SingleScheduleModal.LastName;
                            defaultRegistation.Email = postAssessmentModal.SingleScheduleModal.Email;
                            defaultRegistation.Password = postAssessmentModal.SingleScheduleModal.Password;
                            defaultRegistation.MobileNumber = postAssessmentModal.SingleScheduleModal.Mobile;
                            lstDefaultReg.Add(defaultRegistation);
                        }

                        AssessmentUserDetail assessmentUserDetail = new AssessmentUserDetail();
                        assessmentUserDetail.AssessmentID = assesmentDetailMaster.ID;
                        assessmentUserDetail.CreatedDate = DateTime.Now;
                        assessmentUserDetail.ID = Guid.NewGuid();
                        assessmentUserDetail.IsDeleted = false;
                        assessmentUserDetail.ModifiedDate = DateTime.Now;
                        assessmentUserDetail.UserID = UserDetailMaster.Id;
                        TestEngineDBContext.AssessmentUserDetails.Add(assessmentUserDetail);
                        TestEngineDBContext.DefaultRegistations.AddRange(lstDefaultReg);
                    }

                    //Alert information to Admin

                    if(!string.IsNullOrEmpty(postAssessmentModal.AssesmentAlertEmail))
                    {
                        List<AssessmentAdminEmailNotification> lstAssesmentEMailNotification = new List<AssessmentAdminEmailNotification>();
                        foreach(var items in postAssessmentModal.AssesmentAlertEmail.Split(','))
                        {
                            AssessmentAdminEmailNotification assessmentAdminEmailNotification = new AssessmentAdminEmailNotification();
                            assessmentAdminEmailNotification.AssessmentId = assesmentDetailMaster.ID;
                            assessmentAdminEmailNotification.EmailId = items;
                            assessmentAdminEmailNotification.id = Guid.NewGuid();
                            assessmentAdminEmailNotification.IsAdminEmailCompletionAlertEnabled = postAssessmentModal.IsAssessmentCompletionAlertEnabled;
                            lstAssesmentEMailNotification.Add(assessmentAdminEmailNotification);
                        }
                        if(lstAssesmentEMailNotification.Any())
                        {
                            TestEngineDBContext.AssessmentAdminEmailNotifications.AddRange(lstAssesmentEMailNotification);
                        }                      
                    }


                    //Alert Information to Students
                    if(postAssessmentModal.AssessmentStudentAlertModal != null)
                    {
                        AssessmentStudentNotification assessmentStudentNotification = new AssessmentStudentNotification();
                        assessmentStudentNotification.AssessmentId = assesmentDetailMaster.ID;
                        assessmentStudentNotification.BCC = postAssessmentModal.AssessmentStudentAlertModal.BCC;
                        assessmentStudentNotification.BodyofMessage = postAssessmentModal.AssessmentStudentAlertModal.BCC;
                        assessmentStudentNotification.CC = postAssessmentModal.AssessmentStudentAlertModal.CC;
                        assessmentStudentNotification.CommunicationType = postAssessmentModal.AssessmentStudentAlertModal.CommunicationType;
                        assessmentStudentNotification.CreatedDate = DateTime.Now;
                        assessmentStudentNotification.id = Guid.NewGuid();
                        assessmentStudentNotification.IsEnabled = postAssessmentModal.AssessmentStudentAlertModal.IsEnabled;
                        assessmentStudentNotification.ModHistory = string.Empty;
                        assessmentStudentNotification.ModifiedDate = DateTime.Now;
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

    }
}
