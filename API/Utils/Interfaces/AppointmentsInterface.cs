using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.ModelsViews;

namespace Api.Utils.Interfaces;

public interface AppointmentsInterface
{
    Task<List<AppointmentsModelView>> GetAll();
    Task<AppointmentsModelView?> GetById(int Id);
    Task CreateNewAppointment(AppointmentsDTO appointmentsDTO);
    Task<AppointmentsModelView?> UpdateOneAppointment(AppointmentsDTO appointmentsDTO, int Id);
    Task<AppointmentsModelView?> DeleteOneAppointment(int Id);
}