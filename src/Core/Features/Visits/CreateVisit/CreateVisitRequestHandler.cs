using MediatR;

namespace Application.Features.Visits.CreateVisit;

public class CreateVisitRequestHandler : IRequestHandler<CreateVisitRequest, int>
{
    public Task<int> Handle(CreateVisitRequest request, CancellationToken cancellationToken)
    {
        
        
        throw new NotImplementedException();
    }
}