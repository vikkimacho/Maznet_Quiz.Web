﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Quiz.Web.BLL.Exam;
using Quiz.Web.DTO.Models;

namespace Quiz.Web.API.Controllers
{
    public class ExamController : ApiController
    {
        APIResponse result = new APIResponse();
        ExamBLL examBLL = new ExamBLL();
        public string GetPortalLogin(Guid assessmentID)
        {

            var result = examBLL.GetExamPortal(assessmentID);
            return result;
        }

        [HttpGet]
        public APIResponse ValidateExaminer(string username, string password, string assessmentID)
        {
            try
            {

                result = examBLL.ValidateExaminer(username, password, assessmentID);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        [HttpGet]
        public List<CustomRegistration> GetRegistration( string assessmentID)
        {
            List<CustomRegistration> result = new List<CustomRegistration>();
            try
            {

                result = examBLL.GetRegistration(assessmentID);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        [HttpPost]
        public string SaveExamAnswers(Guid assesmentID, Guid userID, Guid qusID, string answer)
        {
            string result = "FAILED";
            try
            {
                result = examBLL.SaveExamAnswers(assesmentID, userID, qusID, answer);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        [HttpGet]
        public List<Questions> GetAssesmentQuestions(Guid assesmentID)
        {
            List<Questions> questions = new List<Questions>();
            try
            {
                questions = examBLL.GetAssesmentQuestions(assesmentID);
            }
            catch (Exception ex)
            {

            }
            return questions;
        }
    }
}