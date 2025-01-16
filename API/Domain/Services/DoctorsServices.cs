using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.ModelsViews;
using Api.Infrastructure.Database;
using Api.Utils.HashGenerator;
using Api.Utils.Interfaces;
using Api.Utils.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;

namespace Api.Domain.Services;

public class DoctorsServices : DoctorsInterface
{
    private DatabaseContext _context;
    public DoctorsServices(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<List<DoctorsModelView>> GetAll()
    {
        try 
        {
            var result = await _context.Doctors.ToListAsync();
            List<DoctorsModelView> doctors = new List<DoctorsModelView>();

            foreach (DoctorsEntity item in result)
            {
                doctors.Add(new DoctorsModelView
                {
                    Id = item.Id,
                    User = item.User,
                    Name = item.Name,
                    Specialty = item.Specialty.ToString(),
                });
            }
            return doctors;
        } catch (Exception error)
        {
            throw new Exception($"Um erro ocorreu ao tentar buscar todos os médicos: {error}");
        }
    }

    public async Task<Dictionary<string, string>?> DoctorLogin(LoginDTO loginDTO)
    {
        var hasDoctorCretendials = await _context.Doctors
        .Where(x => x.User == loginDTO.User && x.Password == loginDTO.Password.GenerateHash())
        .FirstOrDefaultAsync();

        if (hasDoctorCretendials != null)
        {
            return new Dictionary<string, string>{ { "Token", TokenService.GenerateDoctorToken(hasDoctorCretendials) }};
        }

        return null;
    }

    public async Task<DoctorsModelView?> GetById(int Id)
    {
        var result = await _context.Doctors.Where(x => x.Id == Id).FirstOrDefaultAsync();
        if (result != null)
        {
            var doctor = new DoctorsModelView 
            {
                User = result.User,
                Id = result.Id,
                Name = result.Name,
                Specialty = result.Specialty
            };

            return doctor;
        }
        return null;
    }
    public async Task CreateNewDoctor(DoctorsDTO doctorsDTO)
    {
        var newDoctor = new DoctorsEntity 
        {
            Password = doctorsDTO.Password.GenerateHash(),
            User = doctorsDTO.User,
            Name = doctorsDTO.Name,
            Specialty = doctorsDTO.Specialty.ToString()
        };

        await _context.Doctors.AddAsync(newDoctor);
        await _context.SaveChangesAsync();
    }

    public async Task<DoctorsModelView?> DeleteOneDoctor(int Id)
    {
        var doctorToDelete = await _context.Doctors.Where(x => x.Id == Id).FirstOrDefaultAsync();

        if (doctorToDelete != null)
        {
            _context.Doctors.Remove(doctorToDelete);
            await _context.SaveChangesAsync();
            return new DoctorsModelView 
            {
                Id = doctorToDelete.Id,
                User = doctorToDelete.User,
                Specialty = doctorToDelete.Specialty,
                Name = doctorToDelete.Name
            };
        }
        return null;
    }

    public async Task<DoctorsModelView?> UpdateOneDOctor(DoctorsDTO doctorsDTO, int Id)
    {
        var doctorToEdit = await _context.Doctors.Where(x => x.Id == Id).FirstOrDefaultAsync();

        if (doctorToEdit != null)
        {
            doctorToEdit.User = doctorsDTO.User;
            doctorToEdit.Password = doctorsDTO.Password.GenerateHash();
            doctorToEdit.Name = doctorsDTO.Name;
            doctorToEdit.Specialty = doctorsDTO.Specialty.ToString();

            _context.Doctors.Update(doctorToEdit);

            var doctorEdited = new DoctorsModelView
            {
                Id = Id,
                User = doctorsDTO.User,
                Specialty = doctorsDTO.Specialty.ToString(),
                Name = doctorsDTO.Name
            };

            await _context.SaveChangesAsync();
            return doctorEdited;
        }
        return null;
    }
}