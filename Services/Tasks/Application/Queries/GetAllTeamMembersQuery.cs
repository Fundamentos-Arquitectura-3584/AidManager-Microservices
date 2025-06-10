using MediatR;
using System.Collections.Generic;
using Tasks.Application.DTOs;

namespace Tasks.Application.Queries
{
    // Changed name from GetAllTeamMembers to GetAllTeamMembersByProjectIdQuery for clarity
    public record GetAllTeamMembersByProjectIdQuery(int ProjectId) : IRequest<IEnumerable<TeamMemberDto>>;
}
