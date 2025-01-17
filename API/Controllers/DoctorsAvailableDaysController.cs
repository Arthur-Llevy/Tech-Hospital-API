using Api.Utils.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.DTOs;

namespace Api.Controllers;

[ApiController]
[Route("/doctors-available-days")]
public class DoctorsAvailableDaysController : ControllerBase
{
    private readonly DoctorsDaysAvailableInterface _doctorsAvailableDaysServices;
    public DoctorsAvailableDaysController(DoctorsDaysAvailableInterface doctorsAvailableDaysServices)
    {
        _doctorsAvailableDaysServices = doctorsAvailableDaysServices;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try {
            var appointments = await _doctorsAvailableDaysServices.GetAll();
            return Ok(appointments);
        } catch (Exception error)
        {
            return StatusCode(500, $"A error ocurred while searching for patients: {error}");
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult?> GetById(int Id)
    {
        try
        {
            var result = await _doctorsAvailableDaysServices.GetById(Id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao buscar um dia disponível. Tente novamente mais tarde. {error}");
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateNewDoctorAvailableDay(DoctorsDaysAvailableDTO doctorsDaysAvailableDTO)
    {
        try 
        {
            await _doctorsAvailableDaysServices.CreateNewDoctorDayAvailable(doctorsDaysAvailableDTO);
            return Ok("Novo dia disponível criado com sucesso!");
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao criar um novo dia disponível. Tente novamente mais tarde. {error}");
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            var result = await _doctorsAvailableDaysServices.DeleteOneDoctorDayAvailable(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao tentar excluir o dia disponível. Tente novamente mais tarde. {error}");
        }
    }

    [Authorize]
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOneDoctorAvailableDay(DoctorsDaysAvailableDTO doctorsDaysAvailableDTO, int id)
    {
        try
        {
            var result = await _doctorsAvailableDaysServices.UpdateOneDoctorDayAvailable(doctorsDaysAvailableDTO, id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao realizar tentar atualizar este dia disponível. Tente novamente mais tarde. {error}");
        }
    }
}