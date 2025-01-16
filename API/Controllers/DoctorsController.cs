using Api.Utils.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.DTOs;

namespace Api.Controllers;

[ApiController]
[Route("/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly DoctorsInterface _doctorsServices;
    public DoctorsController(DoctorsInterface doctorsServices)
    {
        _doctorsServices = doctorsServices;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try {
            var administrators = await _doctorsServices.GetAll();
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
            var result = await _doctorsServices.GetById(Id);
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
    public async Task<IActionResult> CreateNewDoctor(DoctorsDTO doctorsDTo)
    {
        try 
        {
            await _doctorsServices.CreateNewDoctor(doctorsDTo);
            return Ok("Novo doutor criado com sucesso!");
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao criar um novo doutor. Tente novamente mais tarde. {error}");
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            var result = await _doctorsServices.DeleteOneDoctor(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao tentar excluir o doutor. Tente novamente mais tarde. {error}");
        }
    }

    [Authorize]
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOneDoctor(DoctorsDTO doctorsDTO, int id)
    {
        try
        {
            var result = await _doctorsServices.UpdateOneDOctor(doctorsDTO, id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao realizar tentar atualizar este doutor. Tente novamente mais tarde. {error}");
        }
    }

    [HttpPost("/doctors/login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        try
        {
            var result = await _doctorsServices.DoctorLogin(loginDTO);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao realizar o login. Tente novamente mais tarde. {error}");
        }
    }
}