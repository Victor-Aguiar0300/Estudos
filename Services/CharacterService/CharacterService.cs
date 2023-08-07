global using AutoMapper;
using Estudos.Models;

namespace Estudos.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> characters = new List<Character>
      {
          new Character(),
          new Character{ Id = 1, Name = "Sam"}
      };

        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper) 
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(x => x.Id) + 1;
            characters.Add(character);
            characters.Add(_mapper.Map<Character>(newCharacter));
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacters(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {

                var character = characters.FirstOrDefault(x => x.Id == id);
                if (character == null)
                {
                    throw new Exception($"Character with Id '{id}' not found.");

                }

                characters.Remove(character);

                serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();


            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse; 
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatecharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                
                var character = characters.FirstOrDefault(x => x.Id == updatecharacter.Id);
                if (character == null)
                {
                    throw new Exception($"Character with Id '{updatecharacter.Id}' not found.");
                }

                _mapper.Map(updatecharacter, character);    

                character.Name = updatecharacter.Name;
                character.HitPoints = updatecharacter.HitPoints;
                character.Strength = updatecharacter.Strength;
                character.Defense = updatecharacter.Defense;
                character.Intelligence = updatecharacter.Intelligence;
                character.Class = updatecharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);

                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }
    }
}
