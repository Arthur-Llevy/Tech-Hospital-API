using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.ModelsViews;
using Api.Infrastructure.Database;
using Api.Utils.HashGenerator;
using Api.Utils.Interfaces;
using Api.Utils.Jwt;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Services;

public class AppointmentsServices : AppointmentsInterface
{
    private DatabaseContext _context;
    public AppointmentsServices(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<List<AppointmentsModelView>> GetAll()
    {
        try 
        {
            var result = await _context.Appointments
            .Include(x => x.Doctor)
            .Include(x => x.Patient)
            .Include(x => x.Date)
            .ToListAsync();
            List<AppointmentsModelView> appointments = new List<AppointmentsModelView>();

            foreach (AppointmentsEntity item in result)
            {
                appointments.Add(new AppointmentsModelView
                {
                    Id = item.Id,
                    Status = item.Status.ToString(),
                    Type = item.Type.ToString(),
                    Date = item.Date,
                    Doctor = item.Doctor,
                    Patient = item.Patient
                });
            }
            return appointments;
        } catch (Exception error)
        {
            throw new Exception($"Um erro ocorreu ao tentar buscar todos os exames: {error}");
        }
    }

    public async Task<AppointmentsModelView?> GetById(int Id)
    {
        var result = await _context.Appointments
        .Include(x => x.Doctor)
        .Include(x => x.Patient)
        .Include(x => x.Date)
        .Where(x => x.Id == Id).FirstOrDefaultAsync();

        if (result != null)
        {
            var appointment = new AppointmentsModelView 
            {
                Id = result.Id,
                Status = result.Status.ToString(),
                Type = result.Type.ToString(),
                Date = result.Date,
                Doctor = result.Doctor,
                Patient = result.Patient
            };

            return appointment;
        }
        return null;
    }
    public async Task CreateNewAppointment(AppointmentsDTO appointmentsDTO)
    {
        var newAppointment = new AppointmentsEntity 
        {
           Doctor_Id = appointmentsDTO.Doctor_Id,
           Patient_Id = appointmentsDTO.Patient_Id,
           Doctors_Days_Available_Id = appointmentsDTO.Doctors_Days_Available_Id,
           Status = appointmentsDTO.Status,
           Type = appointmentsDTO.Type,
        };

        await _context.Appointments.AddAsync(newAppointment);
        await _context.SaveChangesAsync();
    }

    public async Task<AppointmentsModelView?> DeleteOneAppointment(int Id)
    {
        var appointmentToDelete = await _context.Appointments
        .Include(x => x.Doctor)
        .Include(x => x.Patient)
        .Include(x => x.Date)
        .Where(x => x.Id == Id).FirstOrDefaultAsync();

        if (appointmentToDelete != null)
        {
            _context.Appointments.Remove(appointmentToDelete);
            await _context.SaveChangesAsync();
            return new AppointmentsModelView 
            {
                Id = appointmentToDelete.Id,
                Status = appointmentToDelete.Status.ToString(),
                Type = appointmentToDelete.Type.ToString(), 
            };
        }
        return null;
    }

    public async Task<AppointmentsModelView?> UpdateOneAppointment(AppointmentsDTO appointmentsDTO, int Id)
    {
        var appointmentToUpdate = await _context.Appointments.Where(x => x.Id == Id).FirstOrDefaultAsync();

        if (appointmentToUpdate != null)
        {
            appointmentToUpdate.Doctor_Id = appointmentsDTO.Doctor_Id;
            appointmentToUpdate.Patient_Id = appointmentsDTO.Patient_Id;
            appointmentToUpdate.Doctors_Days_Available_Id = appointmentsDTO.Doctors_Days_Available_Id;
            appointmentToUpdate.Status = appointmentsDTO.Status;
            appointmentToUpdate.Type = appointmentsDTO.Type;

            _context.Appointments.Update(appointmentToUpdate);

            var appointmentEdited = new AppointmentsModelView
            {
                Id = appointmentToUpdate.Id,
                Status = appointmentToUpdate.Status.ToString(),
                Type = appointmentToUpdate.Type.ToString(), 
            };

            await _context.SaveChangesAsync();
            return appointmentEdited;
        }
        return null;
    }
}