using Microsoft.AspNetCore.Mvc;
using dotNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Nodes;
using System.Diagnostics;
using static DotNetMentorship.TestAPI.Responses.UkrainianBadResponse;
using static DotNetMentorship.TestAPI.Responses.UkrainianSuccessResponse;
using Npgsql;
using Microsoft.AspNetCore.Http.Features;
using DotNetMentorship.TestAPI.Responses;
using System.ComponentModel;

namespace DotNetMentorship.TestAPI
{
    [Route("api/ukrainians")]
    [ApiController]
    public class UkrainianController : ControllerBase
    {
        private readonly UkrainianDbContext _dbContext;
        private UnitOfWork _unitOfWork;

        public UkrainianController(UkrainianDbContext dbContext, UnitOfWork unitOfWork) {
            _dbContext = dbContext;
           _unitOfWork = new UnitOfWork(_dbContext);
        }

        // GET: api/ukrainians

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _unitOfWork.UkrainianRepository.GetAllAsync();
            
            return Ok(users);
        }

        //GET api/ukrainians/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var user = await _unitOfWork.UkrainianRepository.GetByIDAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(418);
            }
        }

        // POST /api/ukrainians
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UkrainianDTO UserDTO)
        {
            try
            {
                if (UserDTO != null && UserDTO.Name != null && UserDTO.City != null && UserDTO.IsCalm != null)
                {
                    var user = new Ukrainian()
                    {
                        Name = UserDTO.Name,
                        City = UserDTO.City,
                        IsCalm = UserDTO.IsCalm
                    };
                     _unitOfWork.UkrainianRepository.InsertAsync(user);
                    _unitOfWork.Save();

                    return Ok(new UkrainianObjectCreatedResponse($"User was successfully created!"));
                }
                else
                {
                    throw new Exception("Incorrect data");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return BadRequest(new UkrainianValidationError(ex.Message));
            }
            }

        // PUT api/ukrainians/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UkrainianDTO UserDTO)
        {
            try
            {
                if (UserDTO != null && UserDTO.Name != null && UserDTO.City != null && UserDTO.IsCalm != null)
                {
                    var user = new Ukrainian()
                    {
                        Id = id,
                        Name = UserDTO.Name,
                        City = UserDTO.City,
                        IsCalm = UserDTO.IsCalm
                    };

                    Ukrainian userToUpdate = await _unitOfWork.UkrainianRepository.GetByIDAsync(id);
                    _unitOfWork.UkrainianRepository.Update(id, user);
                    _unitOfWork.Save();
                    Ukrainian updatedUser = await _unitOfWork.UkrainianRepository.GetByIDAsync(id);

                    return Ok(new UkrainianObjectCreatedResponse($"User {updatedUser.Name} was successfully updated!", updatedUser.Id));

                }
                else
                {
                    throw new Exception("Incorrect data");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return BadRequest(new UkrainianValidationError(ex.Message));
            }
        }
    }
}
