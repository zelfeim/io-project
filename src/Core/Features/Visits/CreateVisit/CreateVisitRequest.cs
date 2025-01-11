using System.Runtime.InteropServices.JavaScript;
using Application.Features.Visit.Enums;
using Application.Features.Visits.Enums;
using MediatR;

namespace Application.Features.Visits.CreateVisit;

public class CreateVisitRequest : IRequest<int>
{
    public int FkAnimal;
    public int FkEmployee;
    public VisitType VisitType;
    public DateTime Date;
    public VisitStatus VisitInformation;
    public uint VisitLength;
}