using Api.Domain.DTOs;
using Api.Domain.ModelsViews;

namespace Api.Utils.Interfaces;

public interface DoctorsDaysAvailableInterface
{
    Task<List<DoctorsDaysAvailableModelView>> GetAll();
    Task<DoctorsDaysAvailableModelView?> GetById(int Id);
    Task CreateNewDoctorDayAvailable(DoctorsDaysAvailableDTO doctorsDaysAvailableDTO);
    Task<DoctorsDaysAvailableModelView?> DeleteOneDoctorDayAvailable(int Id);
    Task<DoctorsDaysAvailableModelView?> UpdateOneDoctorDayAvailable(DoctorsDaysAvailableDTO doctorsDaysAvailableDTO, int Id);
}