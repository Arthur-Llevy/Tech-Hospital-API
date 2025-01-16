using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.ModelsViews;
using Api.Infrastructure.Database;
using Api.Utils.HashGenerator;
using Api.Utils.Interfaces;
using Api.Utils.Jwt;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Services;

public class AdministratorsServices : AdministratorsInterface
{
    private DatabaseContext _context;
    public AdministratorsServices(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<List<AdministratorsModelView>> GetAll()
    {
        try 
        {
            var result = await _context.Administrators.ToListAsync();
            List<AdministratorsModelView> administrators = new List<AdministratorsModelView>();

            foreach (AdministratorsEntity item in result)
            {
                administrators.Add(new AdministratorsModelView
                {
                    Id = item.Id,
                    User = item.User
                });
            }
            return administrators;
        } catch (Exception error)
        {
            throw new Exception($"A error ocurred while searching for administrators: {error}");
        }
    }

    public async Task<Dictionary<string, string>?> AdministratorLogin(AdministratorLoginDTO administratorLoginDto)
    {
        var hasAdministratorWithThisCredentials = await _context.Administrators
        .Where(x => x.User == administratorLoginDto.User && x.Password == administratorLoginDto.Password.GenerateHash())
        .FirstOrDefaultAsync();

        if (hasAdministratorWithThisCredentials != null)
        {
            return new Dictionary<string, string>{ { "Token", TokenService.Generate(hasAdministratorWithThisCredentials) }};
        }

        return null;
    }

    public async Task<AdministratorsModelView?> GetById(int Id)
    {
        var result = await _context.Administrators.Where(x => x.Id == Id).FirstOrDefaultAsync();
        if (result != null)
        {
            var administrator = new AdministratorsModelView 
            {
                User = result.User,
                Id = result.Id
            };

            return administrator;
        }
        return null;
    }
    public async Task CreateNewAdministrator(AdministratorsDTO adminitratorDTO)
    {
        var newAdministrator = new AdministratorsEntity 
        {
            Password = adminitratorDTO.Password.GenerateHash(),
            User = adminitratorDTO.User
        };

        await _context.Administrators.AddAsync(newAdministrator);
        await _context.SaveChangesAsync();
    }

    public async Task<AdministratorsModelView?> DeleteOneAdministrator(int Id)
    {
        var administratorToDelete = await _context.Administrators.Where(x => x.Id == Id).FirstOrDefaultAsync();

        if (administratorToDelete != null)
        {
            _context.Administrators.Remove(administratorToDelete);
            await _context.SaveChangesAsync();
            return new AdministratorsModelView 
            {
                Id = administratorToDelete.Id,
                User = administratorToDelete.User
            };
        }
        return null;
    }

    public async Task<AdministratorsModelView?> UpdateOneAdministrator(AdministratorsDTO administrator, int Id)
    {
        var administratorToEdit = await _context.Administrators.Where(x => x.Id == Id).FirstOrDefaultAsync();

        if (administratorToEdit != null)
        {
            administratorToEdit.User = administrator.User;
            administratorToEdit.Password = administrator.Password.GenerateHash();

            _context.Administrators.Update(administratorToEdit);

            var administratorEdited = new AdministratorsModelView
            {
                Id = administratorToEdit.Id,
                User = administratorToEdit.User
            };

            await _context.SaveChangesAsync();
            return administratorEdited;
        }
        return null;
    }
}