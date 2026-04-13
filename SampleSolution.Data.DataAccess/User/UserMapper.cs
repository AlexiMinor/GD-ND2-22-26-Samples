using Riok.Mapperly.Abstractions;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.User.Commands;

namespace SampleSolution.Data.DataAccess.User;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target,
    AllowNullPropertyAssignment = true,
    EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class UserMapper
{
    [MapProperty(nameof(InsertNewUserCommand.Salt), nameof(Db.Entities.User.PasswordSalt))]
    [MapperIgnoreTarget(nameof(Db.Entities.User.Id))]
    [MapperIgnoreTarget(nameof(Db.Entities.User.Role))]
    [MapperIgnoreTarget(nameof(Db.Entities.User.Comments))]
    public static partial Db.Entities.User InsertUserCommandToUser(InsertNewUserCommand userCommand);

    public static partial UserCheckPasswordDto UserEntityToUserCheckPasswordDto(Db.Entities.User userEntity);
}