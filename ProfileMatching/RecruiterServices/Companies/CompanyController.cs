﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileMatching.Models.DTOs;

namespace ProfileMatching.RecruiterServices.Companies

{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    public class CompanyController : Controller
    {
        private ICompany contract;
        public CompanyController(ICompany contract)
        {
            this.contract = contract;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            return Ok(await contract.GetCompanies());
        }
/*        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            return Ok(await contract.GetCompanyById(id));
        }*/
        [HttpPost]
        [Authorize(Roles = "Adminitrator")]
        public async Task<IActionResult> AddCompany(CompanyDTO company)
        {

            return Ok(await contract.AddCompany(company));
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Adminitrator")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                return Ok(await contract.DeleteCompany(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Adminitrator,Recruiter")]
        public void UpdateCompany(CompanyDTO company)
        {
            contract.UpdateCompany(company);

        }
    }
}
