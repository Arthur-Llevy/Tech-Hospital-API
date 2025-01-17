using Api.Utils.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.DTOs;

namespace Api.Controllers;

[ApiController]
[Route("/patients")]
public class PatientsController : ControllerBase
{
    private readonly PatientsInterface _patientsServices;
    public PatientsController(PatientsInterface patientsServices)
    {
        _patientsServices = patientsServices;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try {
            var patients = await _patientsServices.GetAll();
            return Ok(patients);
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
            var result = await _patientsServices.GetById(Id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao buscar um paciente. Tente novamente mais tarde. {error}");
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateNewPatient(PatientsDTO patientsDTO)
    {
        try 
        {
            await _patientsServices.CreateNewPatient(patientsDTO);
            return Ok("Novo paciente criado com sucesso!");
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao criar um novo paciente. Tente novamente mais tarde. {error}");
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            var result = await _patientsServices.DeleteOnePatient(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao tentar excluir o paciente. Tente novamente mais tarde. {error}");
        }
    }

    [Authorize]
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOnePatient(PatientsDTO patientsDTO, int id)
    {
        try
        {
            var result = await _patientsServices.UpdateOnePatient(patientsDTO, id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        } catch (Exception error)
        {
            throw new Exception($"Algo deu errado ao realizar tentar atualizar este paciente. Tente novamente mais tarde. {error}");
        }
    }
}