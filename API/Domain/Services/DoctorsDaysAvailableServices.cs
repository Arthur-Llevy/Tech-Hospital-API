using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.ModelsViews;
using Api.Infrastructure.Database;
using Api.Utils.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Services;

public class DoctorsDaysAvailableServices : DoctorsDaysAvailableInterface
{
    private DatabaseContext _context;
    public DoctorsDaysAvailableServices(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<List<DoctorsDaysAvailableModelView>> GetAll()
    {
        try 
        {
            var result = await _context.Doctors_Avaiable_Days
                .Include(x => x.Doctor)
                .ToListAsync();
            List<DoctorsDaysAvailableModelView> doctorsDaysAvailable = new List<DoctorsDaysAvailableModelView>();

            foreach (DoctorsDaysAvailableEntity item in result)
            {
                doctorsDaysAvailable.Add(new DoctorsDaysAvailableModelView
                {
                  Arrival_Time = item.Arrival_Time,
                  Departure_Time = item.Departure_Time,
                  Date = item.Date,
                  Doctor = item.Doctor,
                  Id = item.Id
                });
            }
            return doctorsDaysAvailable;
        } catch (Exception error)
        {
            throw new Exception($"Um erro ocorreu ao tentar buscar todos os pacientes: {error}");
        }
    }

    public async Task<DoctorsDaysAvailableModelView?> GetById(int Id)
    {
        var result = await _context.Doctors_Avaiable_Days
        .Include(x => x.Doctor)
        .Where(x => x.Id == Id).FirstOrDefaultAsync();
        if (result != null)
        {
            var doctorAvailableDay = new DoctorsDaysAvailableModelView 
            {
                Arrival_Time = result.Arrival_Time,
                Departure_Time = result.Departure_Time,
                Date = result.Date,
                Doctor = result.Doctor,
                Id = result.Id
            };

            return doctorAvailableDay;
        }
        return null;
    }
    public async Task CreateNewDoctorDayAvailable(DoctorsDaysAvailableDTO doctorsDaysAvailableDTO)
    {
        var newDoctorAvailableDay = new DoctorsDaysAvailableEntity 
        {
            Arrival_Time = doctorsDaysAvailableDTO.Arrival_Time,
            Departure_Time = doctorsDaysAvailableDTO.Departure_Time,
            Date = doctorsDaysAvailableDTO.Date,
            Doctor_Id = doctorsDaysAvailableDTO.Doctor_Id            
        };

        await _context.Doctors_Avaiable_Days.AddAsync(newDoctorAvailableDay);
        await _context.SaveChangesAsync();
    }

    public async Task<DoctorsDaysAvailableModelView?> DeleteOneDoctorDayAvailable(int Id)
    {
        var doctorAvailableDayToDelete = await _context.Doctors_Avaiable_Days.Where(x => x.Id == Id).FirstOrDefaultAsync();

        if (doctorAvailableDayToDelete != null)
        {
            _context.Doctors_Avaiable_Days.Remove(doctorAvailableDayToDelete);
            await _context.SaveChangesAsync();
            return new DoctorsDaysAvailableModelView 
            {
                Arrival_Time = doctorAvailableDayToDelete.Arrival_Time,
                Departure_Time = doctorAvailableDayToDelete.Departure_Time,
                Date = doctorAvailableDayToDelete.Date,
                Doctor = doctorAvailableDayToDelete.Doctor,
                Id = doctorAvailableDayToDelete.Id
            };
        }
        return null;
    }

    public async Task<DoctorsDaysAvailableModelView?> UpdateOneDoctorDayAvailable(DoctorsDaysAvailableDTO doctorsDaysAvailableDTO, int Id)
    {
        var doctorAvailableDayToUpdate = await _context.Doctors_Avaiable_Days
        .Include(x => x.Doctor)
        .Where(x => x.Id == Id).FirstOrDefaultAsync();

        if (doctorAvailableDayToUpdate != null)
        {
            doctorAvailableDayToUpdate.Arrival_Time = doctorsDaysAvailableDTO.Arrival_Time;
            doctorAvailableDayToUpdate.Departure_Time = doctorsDaysAvailableDTO.Departure_Time;
            doctorAvailableDayToUpdate.Date = doctorsDaysAvailableDTO.Date;
            doctorAvailableDayToUpdate.Doctor_Id = doctorsDaysAvailableDTO.Doctor_Id;

            _context.Doctors_Avaiable_Days.Update(doctorAvailableDayToUpdate);

            var doctorAvailableDayUpdated = new DoctorsDaysAvailableModelView
            {
                Arrival_Time = doctorsDaysAvailableDTO.Arrival_Time,
                Departure_Time = doctorsDaysAvailableDTO.Departure_Time,
                Date = doctorsDaysAvailableDTO.Date,
                Id = Id
            };

            await _context.SaveChangesAsync();
            return doctorAvailableDayUpdated;
        }
        return null;
    }
}