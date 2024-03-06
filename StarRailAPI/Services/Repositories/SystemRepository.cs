using Microsoft.EntityFrameworkCore;
using StarRailAPI.Common;
using StarRailAPI.Data;
using StarRailAPI.Helpers;
using StarRailAPI.Models;
using Supabase;

namespace StarRailAPI.Service.Repositories
{
    public class SystemRepository : ISystemRepository
    {
        private readonly StarRailContext _context;
        private readonly Client _client;
        private readonly MyHelpers _myHelpers;

        public SystemRepository(StarRailContext context, Supabase.Client client, MyHelpers myHelpers)
        {
            _context = context;
            _client = client;
            _myHelpers = myHelpers;
        }

        public async Task<Result> Add(SystemAdd model)
        {
            var systemExist = await _context.Systems.SingleOrDefaultAsync(s => s.Name.Equals(model.Name));

            if (systemExist is not null)
            {
                return Result.Failure(Errors.AlreadyName(model.Name));
            }

            var system = new SystemData { Name = model.Name };

            if (model.Image != null)
            {
                var linkUpLoad = await _myHelpers.UploadToSupabaseStorage(model.Image, _client, "system-images");

                if (linkUpLoad.Status)
                {
                    system.Logo = linkUpLoad.Value;
                }
            }

            _context.Systems.Add(system);
            _context.SaveChanges();

            return Result.Success($"Add {system.Name} successful");
        }

        public List<SystemData> Get()
        {
            var systems = _context.Systems.ToList();

            return systems;
        }

        public async Task<Result> Remove(int id)
        {
            var system = await _context.Systems.SingleOrDefaultAsync(s => s.Id == id);

            if (system == null)
            {
                return Result.Failure(Errors.NotFound(id.ToString(), "system"));
            }

            if (system.Logo != null)
            {
                await _myHelpers.DeleteFileInSupabaseStorage(_client, system.Logo, "system-images");
            }

            _context.Remove(system);
            _context.SaveChanges();

            return Result.Success("Remove system successful");
        }

        public async Task<Result> Update(SystemUpdate model)
        {
            var system = await _context.Systems.SingleOrDefaultAsync(s => s.Id == model.Id);

            if (system == null)
            {
                return Result.Failure(Errors.NotFound(model.Name, "system"));
            }

            system.Name = model.Name;

            if (model.Image != null)
            {
                await _myHelpers.DeleteFileInSupabaseStorage(_client, system.Logo!, "system-images");

                var uploadImage = await _myHelpers.UploadToSupabaseStorage(model.Image, _client, "system-images");
                system.Logo = uploadImage.Value;
            }

            _context.Update(system);
            _context.SaveChanges();

            return Result.Success("Update Successful");
        }
    }
}