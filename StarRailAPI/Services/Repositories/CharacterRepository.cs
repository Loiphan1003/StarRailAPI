using Microsoft.EntityFrameworkCore;
using StarRailAPI.Common;
using StarRailAPI.Data;
using StarRailAPI.Helpers;
using StarRailAPI.Models.Class;
using Supabase;

namespace StarRailAPI.Service.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly StarRailContext _context;
        private readonly MyHelpers _myHelpers;
        private readonly Client _client;
        private string folderImage = "character-images";

        public CharacterRepository(StarRailContext context, MyHelpers myHelpers, Supabase.Client client)
        {
            _context = context;
            _myHelpers = myHelpers;
            _client = client;
        }

        public async Task<Result> Add(CharacterAdd model)
        {
            var characterExisting = await _context.Characters.SingleOrDefaultAsync(c => c.Name.Equals(model.Name));

            if (characterExisting is not null)
            {
                return Result.Failure(Errors.AlreadyName(model.Name));
            }

            var destiny = await _context.Destinies.SingleOrDefaultAsync(d => d.Name.Equals(model.DestinyName));

            var system = await _context.Systems.SingleOrDefaultAsync(s => s.Name.Equals(model.SystemName));

            if (destiny is null)
            {
                return Result.Failure(Errors.NotFound(model.DestinyName, "Destiny"));
            }

            if (system is null)
            {
                return Result.Failure(Errors.NotFound(model.SystemName, "System"));
            }

            Character character = new Character
            {
                Name = model.Name,
                Avatar = string.Empty,
                DestinyId = destiny.Id,
                SystemDataId = system.Id,
            };

            if (model.Image is not null)
            {
                var uploadFile = await _myHelpers.UploadToSupabaseStorage(model.Image, _client, folderImage);
                if (!uploadFile.Status)
                {
                    return Result.Failure(Errors.UploadFileFailed(uploadFile.Value!));
                }

                character.Avatar = uploadFile.Value!;
            }

            _context.Characters.Add(character);
            _context.SaveChanges();

            return Result.Success("Add character successful");
        }

        public List<Character> Get()
        {
            var characters = _context.Characters.ToList();

            return characters;
        }

        public async Task<Result> Remove(string name)
        {
            var character = await _context.Characters.SingleOrDefaultAsync(c => c.Name.Equals(name));

            if (character is null)
            {
                return Result.Failure(Errors.NotFound(name, "character"));
            }

            await _myHelpers.DeleteFileInSupabaseStorage(_client, character.Avatar, folderImage);

            _context.Remove(character);

            return Result.Success("Remove successful");
        }

        public async Task<Result> Update(CharacterUpdate model)
        {
            var character = await _context.Characters.SingleOrDefaultAsync(c => c.Id == model.Id);

            if (character is null)
            {
                return Result.Failure(Errors.NotFound(model.Name, "character"));
            }

            if (model.Name is not null)
            {
                character.Name = model.Name;
            }

            if (model.Image is not null)
            {
                var uploadFile = await _myHelpers.UploadToSupabaseStorage(model.Image, _client, folderImage);
                if (!uploadFile.Status)
                {
                    return Result.Failure(Errors.UploadFileFailed(uploadFile.Value!));
                }

                character.Avatar = uploadFile.Value!;
            }

            if (model.DestinyName is not null)
            {
                var destiny = await _context.Destinies.FirstOrDefaultAsync(d => d.Name.Equals(model.DestinyName));

                if (destiny is null)
                {
                    return Result.Failure(Errors.NotFound(model.DestinyName, "destiny"));
                }

                character.DestinyId = destiny.Id;
            }

            if (model.SystemName is not null)
            {
                var system = await _context.Systems.FirstOrDefaultAsync(s => s.Name.Equals(model.SystemName));

                if (system is null)
                {
                    return Result.Failure(Errors.NotFound(model.SystemName, "system"));
                }

                character.SystemDataId = system.Id;
            }

            _context.Update(character);
            _context.SaveChanges();

            return Result.Success("Update successful");
        }
    }
}