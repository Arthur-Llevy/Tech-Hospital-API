using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.ModelsViews;

namespace Api.Utils.Interfaces;

public interface AdministratorsInterface
{
    Task<List<AdministratorsModelView>> GetAll();
    Task<AdministratorsModelView?> GetById(int Id);
    Task CreateNewAdministrator(AdministratorsDTO AdminitratorDTO);
    Task<AdministratorsModelView?> DeleteOneAdministrator(int Id);
    Task<AdministratorsModelView?> UpdateOneAdministrator(AdministratorsDTO administrator, int Id);
    Task<Dictionary<string, string>?> AdministratorLogin(LoginDTO administratorLoginDto);    
}