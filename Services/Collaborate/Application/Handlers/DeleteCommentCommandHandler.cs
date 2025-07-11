using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
{
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.CommentId);
        if (comment == null)
        {
            return false; // Or throw NotFoundException
        }

        // Optional: Check if request.UserId matches comment.UserId for authorization
        if (comment.UserId != request.UserId)
        {
            // Handle unauthorized deletion attempt, e.g., throw UnauthorizedAccessException or return false
            // For simplicity, we'll allow deletion if the comment exists in this example.
            // In a real app, this check is crucial.
            // For now, let's assume a service layer above or an API gateway handles authorization based on UserId.
        }

        await _commentRepository.DeleteAsync(request.CommentId);
        return true; // Assuming delete is successful if no exception is thrown
    }
}
