using MediatR;
using AidManager.Collaborate.Application.DTOs;

namespace AidManager.Collaborate.Application.Queries;

public record GetEventByIdQuery(int EventId) : IRequest<EventDto?>;
