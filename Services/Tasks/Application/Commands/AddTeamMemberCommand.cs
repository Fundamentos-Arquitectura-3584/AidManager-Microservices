using MediatR;

namespace Tasks.Application.Commands
{
    // Assuming this command returns boolean or some status. For now, Unit (void).
    public record AddTeamMemberCommand(int ProjectId, int UserId) : IRequest<bool>;
}
