using System.Security.Claims;

namespace Estudos.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private static  List<Character> characters = new List<Character>
      {
          new Character(),
          new Character{ Id = 1, Name = "Sam"}
      };

        private readonly IMapper _mapper;
        
        private readonly DataContext _context;
        
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _mapper = mapper;
            
            _context = context;
            
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.user = await _context.users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = 
                await _context.Characters
                    .Where(c => c.user!.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c))
                    .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacters(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {

                var character = 
                    await _context.Characters
                    .FirstOrDefaultAsync(x => x.Id == id && x.user!.Id == GetUserId());
                if (character == null)
                {
                    throw new Exception($"Personagem com Id'{id}' não foi encontrado.");

                }

                _context.Characters.Remove(character);



                serviceResponse.Data =
                    await _context.Characters
                    .Where(c => c.user!.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();


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
            var dbCharacters = await _context.Characters.Where(c => c.user!.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters
                .FirstOrDefaultAsync(x => x.Id == id && x.user!.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse; 
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatecharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                
                var character = 
                    await _context.Characters
                    .Include(x => x.user)
                    .FirstOrDefaultAsync(x => x.Id == updatecharacter.Id);
                if (character is null || character.user!.Id != GetUserId())
                {
                    throw new Exception($"Personagem com Id '{updatecharacter.Id}' não encontrado.");
                }

                _mapper.Map(updatecharacter, character);    

                character.Name = updatecharacter.Name;
                character.HitPoints = updatecharacter.HitPoints;
                character.Strength = updatecharacter.Strength;
                character.Defense = updatecharacter.Defense;
                character.Intelligence = updatecharacter.Intelligence;
                character.Class = updatecharacter.Class;

                await _context.SaveChangesAsync();
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
