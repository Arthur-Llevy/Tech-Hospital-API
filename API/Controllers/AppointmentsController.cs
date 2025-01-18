using Api.Utils.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.DTOs;

namespace Api.Controllers;

[ApiController]
[Route("/appointments")]
public class AppointmentsController : ControllerBase
{
    private readonly AppointmentsInterface _appointmentsServices;
    public AppointmentsController(AppointmentsInterface appointmentsServices)
    {
        _appointmentsServices = appointmentsServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try {
            var appointments = await _appointmentsServices.GetAll();
            return Ok(appointments);
        } catch (Exception error)
        {
            return StatusCode(500, $"A error ocurred while searching for patients: {error}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult?> GetById(int Id)
    {
        try
        {
            var result = await _appointmentsServices.GetById(Id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao buscar um exame. Tente novamente mais tarde. {error}");
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateNewAppointment(AppointmentsDTO appointmentsDTO)
    {
        try 
        {
            await _appointmentsServices.CreateNewAppointment(appointmentsDTO);
            return Ok("Novo exame criado com sucesso!");
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao criar um novo exame. Tente novamente mais tarde. {error}");
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            var result = await _appointmentsServices.DeleteOneAppointment(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao tentar excluir o exame. Tente novamente mais tarde. {error}");
        }
    }

    [Authorize]
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOneAppointment(AppointmentsDTO appointmentsDTO, int id)
    {
        try
        {
            var result = await _appointmentsServices.UpdateOneAppointment(appointmentsDTO, id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao realizar tentar atualizar este exame. Tente novamente mais tarde. {error}");
        }
    }
}