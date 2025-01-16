using Api.Domain.Entities;
using Api.Utils.Enums;
using Api.Utils.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Api.Utils.Jwt;
using Api.Domain.DTOs;
using Api.Domain.ModelsViews;

namespace Api.Controllers;

[ApiController]
[Route("/administrators")]
public class AdministratorsController : ControllerBase
{
    private readonly AdministratorsInterface _administratorServices;
    public AdministratorsController(AdministratorsInterface administratorServices)
    {
        _administratorServices = administratorServices;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try {
            var administrators = await _administratorServices.GetAll();
            return Ok(administrators);
        } catch (Exception error)
        {
            return StatusCode(500, $"A error ocurred while searching for administrators: {error}");
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult?> GetById(int Id)
    {
        try
        {
            var result = await _administratorServices.GetById(Id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao buscar um administrador. Tente novamente mais tarde. {error}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewAdministrator(AdministratorsDTO administratorsDTO)
    {
        try 
        {
            await _administratorServices.CreateNewAdministrator(administratorsDTO);
            return Ok("Novo administrador criado com sucesso!");
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao criar um novo administrador. Tente novamente mais tarde. {error}");
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            var result = await _administratorServices.DeleteOneAdministrator(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao tentar excluir o administrador. Tente novamente mais tarde. {error}");
        }
    }

    [Authorize]
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOneAdministrator(AdministratorsDTO administrator, int id)
    {
        try
        {
            var result = await _administratorServices.UpdateOneAdministrator(administrator, id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao realizar tentar atualizar este administrador. Tente novamente mais tarde. {error}");
        }
    }

    [HttpPost("/administrators/login")]
    public async Task<IActionResult> Login(AdministratorLoginDTO administratorLoginDTO)
    {
        try
        {
            var result = await _administratorServices.AdministratorLogin(administratorLoginDTO);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Já existe um administrador com este usuário cadastrado.");
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao realizar o login. Tente novamente mais tarde. {error}");
        }
    }
}