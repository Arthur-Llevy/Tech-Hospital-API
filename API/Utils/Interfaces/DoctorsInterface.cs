using Api.Domain.DTOs;
using Api.Domain.ModelsViews;

namespace Api.Utils.Interfaces;

public interface DoctorsInterface 
{
    Task<List<DoctorsModelView>> GetAll();
    Task<DoctorsModelView?> GetById(int Id);
    Task CreateNewDoctor(DoctorsDTO doctorsDTO);
    Task<DoctorsModelView?> DeleteOneDoctor(int Id);
    Task<DoctorsModelView?> UpdateOneDOctor(DoctorsDTO doctorDTO, int Id);
    Task<Dictionary<string, string>?> DoctorLogin(LoginDTO loginDTO);    
}