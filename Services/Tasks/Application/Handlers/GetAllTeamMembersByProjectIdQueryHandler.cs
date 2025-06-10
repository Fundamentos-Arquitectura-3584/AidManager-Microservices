using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Queries;
using Tasks.Application.DTOs;
using Tasks.Application.Interfaces;
// Assuming IUserService or similar to fetch actual user details based on IDs

namespace Tasks.Application.Handlers
{
    public class GetAllTeamMembersByProjectIdQueryHandler : IRequestHandler<GetAllTeamMembersByProjectIdQuery, IEnumerable<TeamMemberDto>>
    {
        private readonly IProjectRepository _projectRepository;
        // private readonly IUserService _userService; // To get user details

        public GetAllTeamMembersByProjectIdQueryHandler(IProjectRepository projectRepository /*, IUserService userService */)
        {
            _projectRepository = projectRepository;
            // _userService = userService;
        }

        public async Task<IEnumerable<TeamMemberDto>> Handle(GetAllTeamMembersByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var userIds = await _projectRepository.GetTeamMemberUserIdsAsync(request.ProjectId, cancellationToken);

            // Placeholder: In a real app, fetch user details from userIds
            // var teamMembers = await _userService.GetUsersByIdsAsync(userIds);
            // return teamMembers.Select(u => new TeamMemberDto(u.Id, u.Name, u.Email, u.Phone, u.ImageUrl));

            // Simplified placeholder response
            return userIds.Select(id => new TeamMemberDto(id, "User "+id, "user"+id+"@example.com", "123-456-7890", "url/to/image.png")).ToList();
        }
    }
}
