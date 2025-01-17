using Api.Domain.DTOs;
using Api.Domain.ModelsViews;

namespace Api.Utils.Interfaces;

public interface PatientsInterface 
{
    Task<List<PatientsModelView>> GetAll();
    Task<PatientsModelView?> GetById(int Id);
    Task CreateNewPatient(PatientsDTO patientsDTO);
    Task<PatientsModelView?> DeleteOnePatient(int Id);
    Task<PatientsModelView?> UpdateOnePatient(PatientsDTO patientsDTo, int Id);
}