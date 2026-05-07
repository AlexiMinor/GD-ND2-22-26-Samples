using Riok.Mapperly.Abstractions;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.User.Commands;

namespace SampleSolution.UserService;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target,
    AllowNullPropertyAssignment = true,
    EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial  class UserMapper
{
    [MapProperty(nameof(RegisterUserDto.UserEmail), nameof(InsertNewUserCommand.Email))]
    public static partial InsertNewUserCommand RegisterUserDtoToInsertCommand(RegisterUserDto dto, string passwordHash, string salt);
}