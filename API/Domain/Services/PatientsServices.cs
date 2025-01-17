using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.ModelsViews;
using Api.Infrastructure.Database;
using Api.Utils.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Services;

public class PatientsServices : PatientsInterface
{
    private DatabaseContext _context;
    public PatientsServices(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<List<PatientsModelView>> GetAll()
    {
        try 
        {
            var result = await _context.Patients
                .Include(x => x.Appointments)
                .ToListAsync();
            List<PatientsModelView> patients = new List<PatientsModelView>();

            foreach (PatientsEntity item in result)
            {
                patients.Add(new PatientsModelView
                {
                    Id = item.Id,
                    Name = item.Name,
                    Gender = item.Gender.ToString(),
                    Age = item.Age,
                    Birth_Date = item.Birth_Date,
                    Appointments = item.Appointments,
                    Cpf = item.Cpf,
                    Observations = item.Observations
                });
            }
            return patients;
        } catch (Exception error)
        {
            throw new Exception($"Um erro ocorreu ao tentar buscar todos os pacientes: {error}");
        }
    }

    public async Task<PatientsModelView?> GetById(int Id)
    {
        var result = await _context.Patients
        .Include(x => x.Appointments)
        .Where(x => x.Id == Id).FirstOrDefaultAsync();
        if (result != null)
        {
            var patient = new PatientsModelView 
            {
                Id = result.Id,
                Name = result.Name,
                Gender = result.Gender.ToString(),
                Age = result.Age,
                Birth_Date = result.Birth_Date,
                Appointments = result.Appointments,
                Cpf = result.Cpf,
                Observations = result.Observations
            };

            return patient;
        }
        return null;
    }
    public async Task CreateNewPatient(PatientsDTO patientsDTO)
    {
        var newPatient = new PatientsEntity 
        {
            Name = patientsDTO.Name,
            Gender = patientsDTO.Gender,
            Age = patientsDTO.Age,
            Birth_Date = patientsDTO.Birth_Date,
            Appointments = patientsDTO.Appointments ?? new List<AppointmentsEntity?>(),
            Cpf = patientsDTO.Cpf,
            Observations = patientsDTO.Observations
        };

        await _context.Patients.AddAsync(newPatient);
        await _context.SaveChangesAsync();
    }

    public async Task<PatientsModelView?> DeleteOnePatient(int Id)
    {
        var patientToDelete = await _context.Patients.Where(x => x.Id == Id).FirstOrDefaultAsync();

        if (patientToDelete != null)
        {
            _context.Patients.Remove(patientToDelete);
            await _context.SaveChangesAsync();
            return new PatientsModelView 
            {
                Id = patientToDelete.Id,
                Name = patientToDelete.Name,
                Gender = patientToDelete.Gender.ToString(),
                Age = patientToDelete.Age,
                Birth_Date = patientToDelete.Birth_Date,
                Appointments = patientToDelete.Appointments,
                Cpf = patientToDelete.Cpf,
                Observations = patientToDelete.Observations
            };
        }
        return null;
    }

    public async Task<PatientsModelView?> UpdateOnePatient(PatientsDTO patientsDTO, int Id)
    {
        var patientToEdit = await _context.Patients
        .Include(x => x.Appointments)
        .Where(x => x.Id == Id).FirstOrDefaultAsync();

        if (patientToEdit != null)
        {
            patientToEdit.Name = patientsDTO.Name;
            patientToEdit.Gender = patientsDTO.Gender;
            patientToEdit.Age = patientsDTO.Age;
            patientToEdit.Birth_Date = patientsDTO.Birth_Date;
            patientToEdit.Appointments = patientsDTO.Appointments ?? new List<AppointmentsEntity?>();
            patientToEdit.Cpf = patientsDTO.Cpf;
            patientToEdit.Observations = patientsDTO.Observations;

            _context.Patients.Update(patientToEdit);

            var patientEdited = new PatientsModelView
            {
                Id = Id,
                Name = patientToEdit.Name,
                Gender = patientToEdit.Gender.ToString(),
                Age = patientToEdit.Age,
                Birth_Date = patientToEdit.Birth_Date,
                Appointments = patientToEdit.Appointments,
                Cpf = patientToEdit.Cpf,
                Observations = patientToEdit.Observations
            };

            await _context.SaveChangesAsync();
            return patientEdited;
        }
        return null;
    }
}