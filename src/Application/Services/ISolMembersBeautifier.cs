using Application.Models;

namespace Application.Services;
/// <summary>
/// Updates properties of the sol member that are not in correct format
/// For example: telephone number: 0612345678 -> 06-12345678
/// </summary>
public interface ISolMembersBeautifier
{
    void Beautify(IEnumerable<SolMember> members);
}
